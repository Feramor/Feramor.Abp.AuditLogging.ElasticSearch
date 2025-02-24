using Xunit;

namespace GeneralTest.EntityFrameworkCore;

[CollectionDefinition(GeneralTestTestConsts.CollectionDefinitionName)]
public class GeneralTestEntityFrameworkCoreCollection : ICollectionFixture<GeneralTestEntityFrameworkCoreFixture>
{

}
