using System;
using System.Collections.Generic;

namespace FileMover
{
    public class ListRulesContext : Base<FRules>
    {
        public FRules frules;

        public ListRulesContext()
        {
            frules = new FRules()
            {
                Date_change = DateTime.Now,
                Creator = Environment.MachineName + "/" + Environment.UserName,
                Item = new List<RuleItem>()
            };

            frules = JsonLoad(frules, Const.FileRules);
        }

        public void EditRule()
        {
            frules.Date_change = DateTime.Now;
            Save();
        }
    }
}