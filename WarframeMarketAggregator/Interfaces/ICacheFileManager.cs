using System.Threading.Tasks;

namespace WarframeMarketAggregator.Interfaces
{
    public interface ICacheFileManager
    {
	    Task RefreshItems(bool forceRefresh = false);
    }
}
