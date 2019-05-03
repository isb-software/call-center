using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Installer
{
    public class UpdateAgent
    {
        private readonly string sourceDirectoryPathKey = "SourceFolder";
        private readonly string targetDirectoryPathKey = "TargetFolder";
        private readonly string targetDirectoryPath;
        private readonly string sourceDirectoryPath;

        private List<FileInfo> targetDirectoryFiles;
        private List<FileInfo> sourceDirectoryFiles;

        public UpdateAgent()
        {
            this.targetDirectoryPath = ConfigurationManager.AppSettings[this.targetDirectoryPathKey];
            this.sourceDirectoryPath = ConfigurationManager.AppSettings[this.sourceDirectoryPathKey];
        }

        public void Update()
        {
            if (this.IsUpdateAvailable())
            {
                this.CopyUpdateFiles();
            }
        }

        private List<FileInfo> GetFilesForUpdate(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> directoryFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();

            return directoryFiles;
        }

        private bool IsUpdateAvailable()
        {
            this.targetDirectoryFiles = this.GetFilesForUpdate(this.targetDirectoryPath);
            this.sourceDirectoryFiles = this.GetFilesForUpdate(this.sourceDirectoryPath);

            FileCompare myFileCompare = new FileCompare();

            return !this.targetDirectoryFiles.SequenceEqual(this.sourceDirectoryFiles, myFileCompare);
        }

        private void CopyUpdateFiles()
        {
            foreach (FileInfo fileInfo in this.sourceDirectoryFiles)
            {
                string updateFileLocation = fileInfo.FullName.Replace(this.sourceDirectoryPath, string.Empty);
                string location = Path.Combine(this.targetDirectoryPath, updateFileLocation);

                new FileInfo(location).Directory.Create();

                File.Copy(fileInfo.FullName, location, true);
            }
        }
    }
}
