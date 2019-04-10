using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Common.Utils;

namespace UpdateAgent
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
            currentDirectoryPath = Directory.GetCurrentDirectory();
            updateDirectoryPath = ConfigurationManager.AppSettings[updateDirectoryPathKey];
        }

        public void Update()
        {
            if (IsUpdateAvailable())
            {
                CopyUpdateFiles();
                MigrateDatabase();
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
            currentDirectoryFiles = GetFilesForUpdate(currentDirectoryPath);
            updateDirectoryFiles = GetFilesForUpdate(updateDirectoryPath);

            FileCompare myFileCompare = new FileCompare();

            return !currentDirectoryFiles.SequenceEqual(updateDirectoryFiles, myFileCompare);
        }

        private void CopyUpdateFiles()
        {
            foreach (FileInfo fileInfo in updateDirectoryFiles)
            {
                string updateFileLocation = fileInfo.FullName.Replace(updateDirectoryPath, string.Empty);
                string location = Path.Combine(currentDirectoryPath, updateFileLocation);

                new FileInfo(location).Directory.Create();

                File.Copy(fileInfo.FullName, location, true);
            }
        }
    }
}
