using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo
{

    public class DocumentService 
    {
        private DocumentService() { }

        #region synthronize call
        public static void Create(string filePath, string content)
        {
            using (FileStream fs = File.Create(filePath))
            {
                var bytes = new UTF8Encoding().GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
                return;
            }
            throw new Exception($"Failed to create a file {filePath}");
        }
        
        public static string Read(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                var sb = new StringBuilder();
                var output = "";
                while ( (output = sr.ReadLine()) != null)
                { 
                    sb.Append(output);
                }
                return sb.ToString();
            }
            throw new Exception($"Failed to Read file {filePath}");
        }

        public static void Delete(string filePath)
        {

        }
        #endregion

        #region
        public static async Task CreateAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }

        public static async Task<string> ReadAsync(string filePath)
        {
            var content = await File.ReadAllTextAsync(filePath);
            return content;
        }

        #endregion

    }
}
