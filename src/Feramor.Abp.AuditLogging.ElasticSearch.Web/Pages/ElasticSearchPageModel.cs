using Feramor.Abp.AuditLogging.ElasticSearch.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ElasticSearchPageModel : AbpPageModel
{
    protected ElasticSearchPageModel()
    {
        LocalizationResourceType = typeof(ElasticSearchResource);
        ObjectMapperContext = typeof(ElasticSearchWebModule);
    }
}
