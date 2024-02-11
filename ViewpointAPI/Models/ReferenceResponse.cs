using System;
using System.Text.Json.Serialization;

namespace ViewpointAPI.Models

{
    public class ReferenceResponse
    {
        public string Identifier { get; set; }

        public string Field { get; set; }

        public string Value { get; set; }
    }
}