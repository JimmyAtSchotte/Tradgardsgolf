using FluentAssertions;
using Tradgardsgolf.Api.CosmosRUTracking;

namespace Tradgardsgolf.Api.Tests.CosmosRUTracking;

[TestFixture]
public class CosmosRUTrackerTests
{
    [Test]
    public void ShouldSumSingleRU()
    {
        var ruTracker = new CosmosRUTracker();
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 13,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.TotalCharge("1").FirstOrDefault().Ru.Should().Be(13.33);
    }
    
    [Test]
    public void ShouldSumSingleRUWithDot()
    {
        var ruTracker = new CosmosRUTracker();
        ruTracker.Log("info: 2024-11-05 11:28:14.782 CosmosEventId.ExecutedReadNext[30102] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReadNext (216.791 ms, 2.3 RU) ActivityId='da7dad4d-bd0c-491a-bcc4-acb7c9d79c02', Container='Course', Partition='(null)', Parameters=[]", "1");
        ruTracker.TotalCharge("1").FirstOrDefault().Ru.Should().Be(2.3);
    }

    
    
    [Test]
    public void ShouldSumMultipleRu()
    {
        var ruTracker = new CosmosRUTracker();
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 13,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 10,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 10,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.TotalCharge("1").FirstOrDefault().Ru.Should().Be(33.99);
    }
    
        
    [Test]
    public void ShouldSplitOnTraceId()
    {
        var ruTracker = new CosmosRUTracker();
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 13,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 10,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "2");
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 25,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "3");
        ruTracker.TotalCharge("1").FirstOrDefault().Ru.Should().Be(13.33);
        ruTracker.TotalCharge("2").FirstOrDefault().Ru.Should().Be(10.33);
        ruTracker.TotalCharge("3").FirstOrDefault().Ru.Should().Be(25.33);
    }
    
    [Test]
    public void ShouldGroupOnSecond()
    {
        var ruTracker = new CosmosRUTracker();
        ruTracker.Log("info: 2024-11-05 10:14:30.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 13,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.Log("info: 2024-11-05 10:14:31.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 10,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.Log("info: 2024-11-05 10:14:32.157 CosmosEventId.ExecutedReplaceItem[30105] (Microsoft.EntityFrameworkCore.Database.Command) \n      Executed ReplaceItem (9 ms, 25,33 RU) ActivityId='d3cc5aea-9072-4cc3-ab59-c5221a3c2a60', Container='Scorecard', Id='d9e13dc7-a0a6-445c-016f-08dcfd7a225c', Partition='2bd438eb-3aca-4a5e-cddb-08dcfd7a223a'", "1");
        ruTracker.TotalCharge("1").ElementAt(0).Ru.Should().Be(13.33);
        ruTracker.TotalCharge("1").ElementAt(1).Ru.Should().Be(10.33);
        ruTracker.TotalCharge("1").ElementAt(2).Ru.Should().Be(25.33);
    }
}