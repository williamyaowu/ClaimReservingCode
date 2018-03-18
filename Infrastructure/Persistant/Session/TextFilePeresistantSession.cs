using System;
using System.Collections.Generic;
using System.IO;

namespace ClaimReserving.Infrastructure.Persistant.Session
{
    public class TextFilePeresistantSession
        : IPersistantSession
    {
        public string FilePath;
        public TextFilePeresistantSession(string filePath)
        {
            this.FilePath = filePath;
            generateFileIfNotExist(this.FilePath);
        }

        public TextFilePeresistantSession(string filePath,bool deleteOriginal):this(filePath)
        {
            if (deleteOriginal && File.Exists(this.FilePath))
            {
                File.Delete(this.FilePath);
            }

            generateFileIfNotExist(this.FilePath);
        }


        public void Dispose()
        {

        }

        public IList<object> ReadAll()
        {
            var resultList = new List<object>();
            try
            {
                var readStream = File.OpenText(this.FilePath);

                string record = null;
                do
                {
                    record = readStream.ReadLine();

                    if (!string.IsNullOrEmpty(record))
                        resultList.Add(record);
                }
                while (record != null);
            }
            catch (Exception ex)
            {
                throw;
            }
            return resultList;
        }

        public void Write(object obj)
        {
            try
            {
                var writeStream = File.AppendText(this.FilePath);

                writeStream.WriteLine(obj.ToString());

                writeStream.Flush();
                writeStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void generateFileIfNotExist(string path)
        {
            if (!File.Exists(this.FilePath))
            {
                var connection = File.Create(this.FilePath);
                connection.Close();
            }
        }
    }
}
