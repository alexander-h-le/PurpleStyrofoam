using System;
using System.Collections;
using System.IO;

namespace PurpleStyrofoam.Utility
{
    static class FileReader
    {
        private static StreamReader sr;

        public static string[] ReadShaderFile(string path)
        {
            using (sr = new StreamReader(path))
            {
                ArrayList strings = new ArrayList();
                while (sr.Peek() >= 0)
                {
                    strings.Add(sr.ReadLine());
                }
                string[] arrayStrings = new string[strings.Count];
                for (int i = 0; i < strings.Count; i++)
                {
                    arrayStrings[i] = (string) strings[i];
                }
                return arrayStrings;
            }
        }
    }
}