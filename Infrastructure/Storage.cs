using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure
{
    public class Storage
    {
        private string _filePath;


        public Storage(string filePath)
        {
            _filePath = filePath;
        }


        public CoursesAndEnrolmentStorage Load()
        {
            if (!File.Exists(_filePath))
                return new CoursesAndEnrolmentStorage();

            var json = File.ReadAllText(_filePath);

            var storage = JsonSerializer.Deserialize<CoursesAndEnrolmentStorage>(json);
            if (storage == null)
                throw new Exception("Deserialization return null.");

            return storage;
        }

        public void Save(CoursesAndEnrolmentStorage storage)
        {
            var json = JsonSerializer.Serialize(storage);
            File.WriteAllText(_filePath, json);

        }
    }
}
