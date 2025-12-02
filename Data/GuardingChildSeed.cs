using System.Text.Json;
using GuardingChild.Models;

namespace GuardingChild.Data
{
    public static class GuardingChildSeed
    {
        public static async Task SeedAsync(GuardingChildContext dbContext)
        {
            if (!dbContext.kids.Any())
            {
                var kidsData = File.ReadAllText("../GuardingChild/Data/DataSeed/kids.json");
                var kids = JsonSerializer.Deserialize<List<Kid>>(kidsData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter()}
                });
                if (kids?.Count > 0) {
                    foreach (var kid in kids) 
                    {
                        await dbContext.Set<Kid>().AddAsync(kid);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
