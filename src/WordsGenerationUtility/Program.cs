using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordsGenerationUtility
{
    public static class Program
    {
        public static void Main()
        {
            //FromNamespaceFile("FromSystemNamespace.txt");
            //FromNamespaceFile("FromSystemThreadingNamespace.txt");
            FromNamespaceFile("FromSystemCollectionsGenericNamespace.txt");
        }

        private static bool StringContainsDigit(string s)
        {
            return
                s.Contains('1')
                ||
                s.Contains('2')
                ||
                s.Contains('3')
                ||
                s.Contains('4')
                ||
                s.Contains('5')
                ||
                s.Contains('6')
                ||
                s.Contains('7')
                ||
                s.Contains('8')
                ||
                s.Contains('9')
                ||
                s.Contains('0');

        }

        private static void FromNamespaceFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename)
                .Where(p => !String.IsNullOrWhiteSpace(p) && !StringContainsDigit(p))
                .Select(p => p.Trim())
                .Where(p => p.Length > 2 && !p.Contains(' ') && !p.Contains('<') && !p.Contains('>') && !p.Contains('_'))
                .Distinct()
                .ToArray();

            Debug.WriteLine($"lines count: {lines.Length}");
            Debug.WriteLine("**********************");
            foreach (string line in lines)
            {
                string[] parts = Regex.Split(line, @"(?<!^)(?=[A-Z])")
                     .Where(p => p.Length > 2)
                     .Distinct()
                     .ToArray();

                Debug.WriteLine(String.Join(" ", parts));
            }
        }
    }
}