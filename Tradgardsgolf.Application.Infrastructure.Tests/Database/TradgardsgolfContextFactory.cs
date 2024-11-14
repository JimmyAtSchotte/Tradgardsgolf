using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Database;

public static class TradgardsgolfContextFactory
{
    internal static TradgardsgolfContext CreateTradgardsgolfContext()
    {
        var options = new DbContextOptionsBuilder<TradgardsgolfContext>()
            .UseCosmos("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "tradgardsgolf-db-test")
            .Options;

        var context = new TradgardsgolfContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        return context;
    }
}