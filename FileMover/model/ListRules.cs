using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public class FRules
    {
        [JsonRequired] public DateTime Date_change { get; set; }
        [JsonRequired] public string Program_name { get; set; }
        [JsonRequired] public string Creator { get; set; }
        [JsonRequired] public List<RuleItem> Item { get; set; }
    }

    public class RuleItem
    {
        [JsonRequired] public int Id { get; set; }
        [JsonRequired] public bool Status { get; set; }
        [JsonRequired] public string DirStart { get; set; }
        [JsonRequired] public string DirDest { get; set; }
        [JsonRequired] public op Operation { get; set; }
        [JsonRequired] public ifEx IfExist { get; set; }
        [JsonRequired] public string FileMask { get; set; }
    }
}