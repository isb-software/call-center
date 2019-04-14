using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgentClient
{
    // This implementation defines a very simple comparison  
    // between two FileInfo objects. It only compares the name  
    // of the files being compared and their length in bytes.  
    public class FileCompare : IEqualityComparer<FileInfo>
    {
        public FileCompare()
        {
        }

        public bool Equals(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            string fileInfo1MD5 = this.CalculateMD5(fileInfo1.FullName);
            string fileInfo2MD5 = this.CalculateMD5(fileInfo2.FullName);

            return fileInfo1MD5 == fileInfo2MD5;
        }

        private string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        public int GetHashCode(FileInfo fileInfo)
        {
            string s = $"{fileInfo.Name}{fileInfo.Length}";
            return s.GetHashCode();
        }
    }
}
