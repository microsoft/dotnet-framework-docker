﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

public class Program
{
    public static void Main(string[] args)
    {
        bool getProcessorCount = args.Length > 0 && args[0] == "getProcessorCount";

        if (getProcessorCount)
        {
            Console.WriteLine(Environment.ProcessorCount);
        }
        else
        {
            Console.WriteLine("Hello Docker World from .NET 4.8.1!");
        }
    }
}
