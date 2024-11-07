using dblw9;

public class StorageAnalyticsService
{
    private readonly MyDbContext _context;

    public StorageAnalyticsService(MyDbContext context)
    {
        _context = context;
    }

    public List<StorageAnalytics> GetStorageAnalytics()
    {
        var analytics = _context.ItemsInStorages
            .GroupBy(i => i.StorageId)
            .Select(g => new StorageAnalytics
            {
                StorageId = g.Key,
                AveragePrice = g.Average(i => i.Item!.Cost), 
                LatestArrival = g.Max(i => i.ArrialDate),
                EarliestArrival = g.Min(i => i.ArrialDate),
                TotalItems = g.Count()
            }).ToList();

        return analytics;
    }
}

