using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FileMover
{
    public class FSettings
    {
        [JsonRequired] public DateTime Date_change { get; set; }
        [JsonRequired] public string Creator { get; set; }
        [JsonRequired] public string Program_name { get; set; }
        [JsonRequired] public string RenameFile { get; set; }
        [JsonRequired] public int MaxCountHistory { get; set; }
        public List<int> WidthColRules { get; set; }
        public List<int> WidthColHistory { get; set; }
        public DateTime LastClearHistory { get; set; }
    }
}