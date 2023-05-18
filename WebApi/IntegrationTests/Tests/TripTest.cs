
using IntegrationTests.Client;
using Xunit;

namespace IntegrationTests.Tests
{
    public class TripTest
    {
        [Fact]
        public async Task testAll()
        {
            var client = new TripClient("http://localhost:12500");
            int count = (await client.getAllExcursii()).Count;
            // Add:
            var excursie = await client.addExcursie(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1, 1, 12);
            Assert.Equal(count+1, (await client.getAllExcursii()).Count);
            Assert.Equal(excursie.id, (await client.getExcursie(excursie.id)).id);
            // Update:
            await client.updateExcursie(excursie.id, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1, 1, 12);
            Assert.Equal(count+1, (await client.getAllExcursii()).Count);
            // Delete:
            await client.deleteExcursie(excursie.id);
            Assert.Equal(count, (await client.getAllExcursii()).Count);
        }
    }
}
