using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WarframeMarketAggregator.Models
{
    public class ItemStatistic
    {
        public string Id { get; set; }

        public int MinPrice { get; set; }

        public int OpenPrice { get; set; }

        public float AvgPrice { get; set; }

        public float MovingAvg { get; set; }

        public int Median { get; set; }

        public int MaxPrice { get; set; }

        [JsonProperty(PropertyName = "datetime", ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTime DateTime { get; set; }

        public int Volume { get; set; }

        public int ClosedPrice { get; set; }

        public int DonchTop { get; set; }

        public int DonchBot { get; set; }
    }
}
