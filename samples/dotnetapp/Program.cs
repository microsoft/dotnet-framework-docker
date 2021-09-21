using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Console;

public static class Program
{
    public static void Main(string[] args) 
    {
        // Variant of https://github.com/dotnet/core/tree/main/samples/dotnet-runtimeinfo
        // Ascii text: https://ascii.co.uk/text (Univers font)

        string nl = Environment.NewLine;

        WriteLine(
        $"                                 ad88{nl}" +
        $"                        ,d      d8\"{nl}" +
        $"                        88      88{nl}" +
        $"8b,dPPYba,   ,adPPYba, MM88MMM MM88MMM 8b,     ,d8{nl}" +
        $"88P'   `\"8a a8P_____88   88      88     `Y8, ,8P'{nl}" +
        $"88       88 8PP\"\"\"\"\"\"\"   88      88       )888({nl}" +
        $"88       88 \"8b,   ,aa   88,     88     ,d8\" \"8b,{nl}" +
        $"88       88  `\"Ybbd8\"'   \"Y888   88    8P'     `Y8{nl}");

        // .NET information
        WriteLine(RuntimeInformation.FrameworkDescription);
        WriteLine(RuntimeInformation.OSDescription);

        WriteLine();

        // Environment information
        WriteLine($"{nameof(RuntimeInformation.OSArchitecture)}: {RuntimeInformation.OSArchitecture}");
        WriteLine($"{nameof(Environment.ProcessorCount)}: {Environment.ProcessorCount}");
    }
}
