using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Installer
{
    public static class UpdateAgent
    {
        internal static readonly string SourceDirectoryPathKey = "SourceFolder";
        internal static readonly string TargetDirectoryPathKey = "TargetFolder";

        internal static readonly string TargetDirectoryPath = ConfigurationManager.AppSettings[TargetDirectoryPathKey];
        internal static readonly string SourceDirectoryPath = ConfigurationManager.AppSettings[SourceDirectoryPathKey];

        public static void Update()
        {
            if(String.IsNullOrWhiteSpace(SourceDirectoryPath))
            {
                throw new Exception("Source directory path cannot be null or white space.");
            }

            if (String.IsNullOrWhiteSpace(TargetDirectoryPath))
            {
                throw new Exception("Target directory path cannot be null or white space.");
            }

            if (!Directory.Exists(SourceDirectoryPath))
            {
                throw new Exception(
                    String.Format(
                        "Source directory {0} does not exist.",
                        SourceDirectoryPath));
            }

            if (String.Equals(
                SourceDirectoryPath,
                TargetDirectoryPath,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Cannot copy source directory over itself.");
            }

            Directory.CreateDirectory(TargetDirectoryPath);

            foreach(var sourceFile in Directory.EnumerateFiles(SourceDirectoryPath, "*.*", SearchOption.AllDirectories))
            {
                var targetFile = Path.GetFullPath(sourceFile)
                    .Replace(SourceDirectoryPath, TargetDirectoryPath);

                Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
                                
                if(!File.Exists(targetFile))
                {
                    File.Copy(sourceFile, targetFile);
                    continue;
                }
                               
                var sourceFileHash = CalculateMD5(sourceFile);
                var targetFileHash = CalculateMD5(targetFile);

                if(!sourceFileHash.SequenceEqual(targetFileHash))
                {
                    File.Copy(sourceFile, targetFile, true);
                }
            }
        }

        private static byte[] CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
