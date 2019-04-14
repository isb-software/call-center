using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace AgentClient
{
    public class UpdateAgent
    {
        private readonly string updateDirectoryPathKey = "UpdateFolder";
        private readonly string currentDirectoryPath;
        private readonly string updateDirectoryPath;

        private List<FileInfo> currentDirectoryFiles;
        private List<FileInfo> updateDirectoryFiles;

        public UpdateAgent()
        {
            this.currentDirectoryPath = Directory.GetCurrentDirectory();
            this.updateDirectoryPath = ConfigurationManager.AppSettings[this.updateDirectoryPathKey];
        }

        public void Update()
        {
            if (this.IsUpdateAvailable())
            {
                this.CopyUpdateFiles();
                this.MigrateDatabase();
            }
        }

        private void MigrateDatabase()
        {
            var migrationConfiguration = new Database.Migrations.Configuration
                                             {
                                                 TargetDatabase = new DbConnectionInfo("DefaultConnection")
                                             };
            var migrator = new DbMigrator(migrationConfiguration);
            migrator.Update();
        }

        private List<FileInfo> GetFilesForUpdate(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> directoryFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).ToList();

            // Ignore files that run the console app, TBD what happens when we want to update the console app
            string consoleApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            directoryFiles = directoryFiles.Where(x => !x.Name.Contains(consoleApplicationName)).ToList();

            return directoryFiles;
        }

        private bool IsUpdateAvailable()
        {
            this.currentDirectoryFiles = this.GetFilesForUpdate(this.currentDirectoryPath);
            this.updateDirectoryFiles = this.GetFilesForUpdate(this.updateDirectoryPath);

            FileCompare myFileCompare = new FileCompare();

            return !this.currentDirectoryFiles.SequenceEqual(this.updateDirectoryFiles, myFileCompare);
        }

        private void CopyUpdateFiles()
        {
            foreach (FileInfo fileInfo in this.updateDirectoryFiles)
            {
                string updateFileLocation = fileInfo.FullName.Replace(this.updateDirectoryPath, string.Empty);
                string location = Path.Combine(this.currentDirectoryPath, updateFileLocation);

                new FileInfo(location).Directory.Create();

                File.Copy(fileInfo.FullName, location, true);
            }
        }
    }
}
