using System;
using System.IO;

namespace GoogleHashCode
{
    public class FileHelper
    {
        public static string[] ReadInput(string path = "input.in")
        {
            return File.ReadAllLines(path);
        }

        public static void WriteOutput(string output, string path="output.out")
        {
            File.WriteAllText(path, output);
        }
    }
}
