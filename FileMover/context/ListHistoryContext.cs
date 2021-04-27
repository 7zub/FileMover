using System;
using System.Collections.Generic;

namespace FileMover
{
    public class ListHistoryContext : Base<History>
    {
        public History history;

        public ListHistoryContext()
        {
            history = new History()
            {
                Date_change = DateTime.Now,
                Creator = Environment.MachineName + "/" + Environment.UserName,
                Item = new List<HistoryItem>()
            };

            history = JsonLoad(history, Const.FileHistory);
        }

        public void EditHistory()
        {
            history.Date_change = DateTime.Now;
            Save();
        }
    }
}