using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Weasel.Postgresql.Tests
{
    [Collection("schemas")]
    public class create_and_teardown_schemas : IntegrationContext
    {
        public create_and_teardown_schemas() : base("schemas")
        {
        }

        [Fact]
        public async Task can_create_and_drop_a_schema()
        {
            await theConnection.OpenAsync();

            await theConnection.DropSchema("one");
            
            (await SchemaManager.ActiveSchemaNames(theConnection)).ShouldNotContain("one");

            await theConnection.CreateSchema("one");
            
            var schemas = await SchemaManager.ActiveSchemaNames(theConnection);
            
            schemas.ShouldContain("one");
        }
    }
}