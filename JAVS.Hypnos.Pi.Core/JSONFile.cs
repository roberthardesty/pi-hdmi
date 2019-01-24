using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.Core
{
    public class JSONFile
    {
        public static async Task<T> LoadAsync<T>(string path)
        {
            if (!File.Exists(path))
                await SaveAsync<T>((T)Activator.CreateInstance(typeof(T)), path);
            using(StreamReader streamReader = new StreamReader(path))
            {
                string contentString = await streamReader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(contentString);
            }
        }

        public static async Task<bool> SaveAsync<T>(T item, string path)
        {
            try
            {
                string directoryPath = new FileInfo(path).Directory?.FullName;
                Directory.CreateDirectory(directoryPath);
                using (StreamWriter textWriter = new StreamWriter(path))
                {
                    string contentString = JsonConvert.SerializeObject(item);
                    await textWriter.WriteAsync(contentString);

                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
