using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Web.Menus;

public class ElasticSearchMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(ElasticSearchMenus.Prefix, displayName: "ElasticSearch", "~/ElasticSearch", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
