using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Opt.MarkupTags
{
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Settings"),
                menu => menu.Add(T("Markup Tags"), "1.0", item => item.Action("Index", "Admin", new { area = "MainBit.MarkupTags" })
                    .Permission(StandardPermissions.SiteOwner))
            );
        }
    }
}
