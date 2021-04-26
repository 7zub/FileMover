using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileMover
{
    public class ListRulesContext
    {
        public FRules frules;
        private static List<string> messages = new List<string>();
        private JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Error = (s, e) =>
            {
                var depObj = e.CurrentObject as FRules;
                if (depObj != null)
                    messages.Add(string.Format("Ошибка: " + e.ErrorContext.Error.Message));
                else
                    messages.Add(e.ErrorContext.Error.Message);

                e.ErrorContext.Handled = true;
            },
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
        };

        public ListRulesContext()
        {
            if (File.Exists(Const.FileRules))
            {
                string json = File.ReadAllText(
                    Application.StartupPath + @"\" + Const.FileRules,
                    Encoding.GetEncoding(1251)
                );

                frules = JsonConvert.DeserializeObject<FRules>(json, settings);

                if (messages.Count > 0)
                    throw new Exception($"Ошибка чтения файла { Const.FileRules }\n{ String.Join("\n", messages) }");
            }
            else
            {
                frules = new FRules()
                {
                    Date_change = DateTime.Now,
                    Program_name = "Перемещатор файлов",
                    Creator = Environment.MachineName + "/" +  Environment.UserName,
                    Item = new List<RuleItem>()
                };

                Save();
            }
        }

        public void EditRule()
        {
            frules.Date_change = DateTime.Now;
            Save();
        }

        private void Save()
        {
            File.WriteAllText(
                Const.FileRules,
                JsonConvert.SerializeObject(frules, Formatting.Indented, settings),
                Encoding.GetEncoding(1251)
            );
        }
    }
}