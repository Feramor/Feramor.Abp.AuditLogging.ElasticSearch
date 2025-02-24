using GeneralTest.Samples;
using Xunit;

namespace GeneralTest.EntityFrameworkCore.Domains;

[Collection(GeneralTestTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<GeneralTestEntityFrameworkCoreTestModule>
{

}
