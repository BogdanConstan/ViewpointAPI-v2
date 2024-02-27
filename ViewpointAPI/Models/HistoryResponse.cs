using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ViewpointAPI.Models
{
    public class HistoryResponse
    {
        public string Identifier { get; set; }

        public string Field { get; set; }

        public int Count => Data?.Items.Count ?? 0;
       
        public HistoryDataItems Data { get; set; }
    }
}
