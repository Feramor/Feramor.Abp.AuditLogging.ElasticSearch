using GeneralTest.Samples;
using Xunit;

namespace GeneralTest.EntityFrameworkCore.Applications;

[Collection(GeneralTestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<GeneralTestEntityFrameworkCoreTestModule>
{

}
