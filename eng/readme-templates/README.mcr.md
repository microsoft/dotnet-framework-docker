{{
  set commonArgs to [ "top-header": "##", "readme-host": "mar" ]
}}{{InsertTemplate("About.md", commonArgs)}}

{{InsertTemplate("FeaturedTags.md", commonArgs)}}

{{InsertTemplate("ReposProvider.md", union([ "template": "RelatedRepos.md" ], commonArgs))}}

{{InsertTemplate("Use.md", commonArgs)}}

{{InsertTemplate("Support.md", commonArgs)}}
