using System;
using System.Collections.Generic;

namespace FileMover
{
    public class ListSettingsContext : Base<FSettings>
    {
        public FSettings settings;

        public ListSettingsContext()
        {
            settings = new FSettings()
            {
                Date_change = DateTime.Now,
                Creator = Environment.MachineName + "/" + Environment.UserName,
                Program_name = Const.DefaultProgramName,
                RenameFile = Const.RenameFile,
                MaxCountHistory = Const.MaxCountHistory,
                LastClearHistory = DateTime.Now,
                WidthColHistory = new List<int> { 110,120,270,270,70,70,80 }
            };

            settings = JsonLoad(settings, Const.FileSettings);
        }

        public void EditSettings()
        {
            settings.Date_change = DateTime.Now;
            Save();
        }
    }
}