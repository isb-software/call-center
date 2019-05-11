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
        private static readonly string sourceDirectoryPathKey = "SourceFolder";
        private static readonly string targetDirectoryPathKey = "TargetFolder";

        private static readonly string targetDirectoryPath = ConfigurationManager.AppSettings[targetDirectoryPathKey];
        private static readonly string sourceDirectoryPath = ConfigurationManager.AppSettings[sourceDirectoryPathKey];

        public static void Update()
        {
            if(String.IsNullOrWhiteSpace(sourceDirectoryPath))
            {
                throw new Exception("Source directory path cannot be null or white space.");
            }

            if (String.IsNullOrWhiteSpace(targetDirectoryPath))
            {
                throw new Exception("TArget directory path cannot be null or white space.");
            }

            if (!Directory.Exists(sourceDirectoryPath))
            {
                throw new Exception(
                    String.Format(
                        "Source directory {0} does not exist.",
                        sourceDirectoryPath));
            }

            if (String.Equals(
                sourceDirectoryPath,
                targetDirectoryPath,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Cannot copy source directory over itself.");
            }

            Directory.CreateDirectory(targetDirectoryPath);

            foreach(var sourceFile in Directory.EnumerateFiles(sourceDirectoryPath, "*.*", SearchOption.AllDirectories))
            {
                //var sourceFileSubdirectory = Path.GetFullPath
                //TODO: Fix issue. Subfolders in source path are not carried over in target path.
                var targetFile = Path.Combine(targetDirectoryPath, Path.GetFileName(sourceFile));

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
