using System;
using System.Collections.Generic;
//using System.Text.Json.Serialization;

namespace ViewpointAPI.Models
{
    public class HistoryDataItems
    {
        public Dictionary<DateTime, double> Items { get; set; }
    }
}
