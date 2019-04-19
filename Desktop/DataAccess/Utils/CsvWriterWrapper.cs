using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using CsvHelper;
using Entities.Models;

namespace DataAccess.Utils
{
    public static class CsvWriterWrapper
    {
        public static void WriteCall(Call call)
        {
            var csvFolderLocation = ConfigurationManager.AppSettings["CsvFolder"];
            using (var writer = new StreamWriter("csvFolderLocation"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(call);
            }
        }
    }
}
