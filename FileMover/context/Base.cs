using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileMover
{
    public class Base<T>
    {
        private T modelJson;
        private string fileJson;
        private static List<string> messages = new List<string>();
        private JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = (s, e) =>
            {
                if (e.CurrentObject is T depObj)
                    messages.Add(string.Format("Ошибка: " + e.ErrorContext.Error.Message));
                else
                    messages.Add(e.ErrorContext.Error.Message);

                e.ErrorContext.Handled = true;
            },
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            Converters = new[] { new StringEnumConverter() }
        };

        protected T JsonLoad(T tObj, string fileJson)
        {
            this.fileJson = fileJson;
            if (File.Exists(fileJson))
            {
                string json = File.ReadAllText(
                    Application.StartupPath + @"\" + fileJson,
                    Encoding.GetEncoding(1251)
                );

                modelJson = JsonConvert.DeserializeObject<T>(json, settings);

                if (messages.Count > 0)
                    throw new Exception($"Ошибка чтения файла { fileJson }\n{ String.Join("\n", messages) }");
            }
            else
            {
                modelJson = tObj;
                Save();
            }
            return modelJson;
        }

        protected void Save()
        {
            File.WriteAllText(
                fileJson,
                JsonConvert.SerializeObject(modelJson, Formatting.Indented, settings),
                Encoding.GetEncoding(1251)
            );
        }
    }
}