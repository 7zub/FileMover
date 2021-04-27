using FileMover.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public class History
    {
        [JsonRequired] public DateTime Date_change { get; set; }
        [JsonRequired] public string Creator { get; set; }
        [JsonRequired] public List<HistoryItem> Item { get; set; }
    }

    public class HistoryItem
    {
        [JsonRequired] public int Id { get; set; }
        [JsonRequired] public DateTime DateMove { get; set; }
        [JsonRequired] public string Filename { get; set; }
        [JsonRequired] public string DirStart { get; set; }
        [JsonRequired] public string DirDest { get; set; }
        //[JsonRequired] public op Operation { get; set; }
        //[JsonRequired] public ifEx IfExist { get; set; }
        [JsonRequired] public int Duration { get; set; }
        [JsonRequired] public int FileSize { get; set; }
        //[JsonRequired]
        public Response result { get; set; }
    }
}