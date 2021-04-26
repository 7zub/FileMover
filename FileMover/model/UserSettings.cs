using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FileMover
{
    public class UserSettings
    {
        [JsonRequired] public DateTime Date_change { get; set; }
        [JsonRequired] public string Program_name { get; set; }
        [JsonRequired] public List<int> WidthColRules { get; set; }
        [JsonRequired] public List<int> WidthColHistory { get; set; }
    }
}