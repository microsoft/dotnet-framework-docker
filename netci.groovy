import jobs.generation.Utilities

def project = GithubProject
def branch = GithubBranchName
def isPR = true
def platformList = [
        'WindowsServerCore-ltsc2016',
        'WindowsServerCore-1709',
        'WindowsServerCore-1803',
        'WindowsServerCore-ltsc2019'
    ]
def versionList = ['3.5', '4.']

platformList.each { platform ->
    versionList.each { version ->
        def newJobName = Utilities.getFullJobName(project, "${version}_${platform}", isPR)
        def versionFilter = "${version}*"

        def newJob = job(newJobName) {
            steps { 
                batchFile("powershell -NoProfile -Command .\\build-and-test.ps1 -VersionFilter \"${versionFilter}\" -OSFilter \"${platform}\" -CleanupDocker")
            }
        }

        if (platform == 'WindowsServerCore-ltsc2019') {
            newJob.with {label('windows.10.amd64.serverrs5.open')}
        }
        else if (platform == 'WindowsServerCore-1803') {
            newJob.with {label('windows.10.amd64.serverrs4.open')}
        }
        else if (platform == 'WindowsServerCore-1709') {
            newJob.with {label('windows.10.amd64.serverrs3.open')}
        }
        else {
            Utilities.setMachineAffinity(newJob, 'Windows_2016', 'latest-docker')
        }
        Utilities.standardJobSetup(newJob, project, isPR, "*/${branch}")
        Utilities.setJobTimeout(newJob, 210)
        Utilities.addGithubPRTriggerForBranch(newJob, branch, "${platform} - ${version} Dockerfiles")
    }
}
