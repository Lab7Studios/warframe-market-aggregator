using System.Collections.Generic;

namespace WarframeMarketAggregator.Models
{
    public class Item
    {
        public ItemLocalization En { get; set; }

        public string UrlName { get; set; }

        public int Ducats { get; set; }

        public int MasteryLevel { get; set; }

        public bool SetRoot { get; set; }

        public string Icon { get; set; }

        public string IconFormat { get; set; }

        public string SubIcon { get; set; }

        public int TradingTax { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
