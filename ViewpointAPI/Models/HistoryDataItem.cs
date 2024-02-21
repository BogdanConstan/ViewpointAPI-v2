using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ViewpointAPI.Models
{
    public class HistoryDataItem
    {
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }
}
