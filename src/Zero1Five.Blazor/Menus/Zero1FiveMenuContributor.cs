using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zero1Five.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Zero1Five.Permissions;
using Microsoft.AspNetCore.Authorization;
using Blazorise;
using Microsoft.Extensions.Localization;

namespace Zero1Five.Blazor.Menus
{
    public class Zero1FiveMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public Zero1FiveMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
                await ConfigureAdminMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private async Task ConfigureAdminMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<Zero1FiveResource>();

            if (await context.AuthorizationService.IsGrantedAsync(Zero1FivePermissions.Categories.Default))
            {
                var admin = context.Menu.GetAdministration();
                var categories = new ApplicationMenuItem(
                    Zero1FiveMenus.Category.Name,
                    l["Menu:ManageCategory"],
                    Zero1FiveMenus.Category.AdminLink,
                    icon: "fas fa-layer-group", 0
                );
                // admin.AddItem(admin);
                admin.Items.Add(categories);
            }
        }
        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<Zero1FiveResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    Zero1FiveMenus.Home,
                    l["Menu:Home"],
                    "/",
                    icon: ""
                )
            );
            await AddProductMenu(context, l);
            await AddGigMenu(context, l);
        }

        private async Task AddGigMenu(MenuConfigurationContext context, IStringLocalizer l)
        {
            var gigsMenu = new ApplicationMenuItem(
                Zero1FiveMenus.Gig.Name,
                l["Menu:Gigs"],
                "",
                icon: ""
            );

            gigsMenu.AddItem(
                new ApplicationMenuItem(
                    Zero1FiveMenus.Gig.List,
                    l["Menu:Gigs"],
                    Zero1FiveMenus.Gig.ListUrl,
                    icon: "fas fa-list"
                ));

            if (await context.IsGrantedAsync(Zero1FivePermissions.Gigs.Default))
            {
                gigsMenu.AddItem(
                    new ApplicationMenuItem(
                        Zero1FiveMenus.Gig.Manage,
                        l["Menu:ManageGigs"],
                        Zero1FiveMenus.Gig.ManageUrl,
                        icon: "fas fa-list",
                        requiredPermissionName: Zero1FivePermissions.Gigs.Default
                    ));
            }

            context.Menu.Items.Insert(1, gigsMenu);
        }

        private async Task AddProductMenu(MenuConfigurationContext context, IStringLocalizer l)
        {
            var productsMenu = new ApplicationMenuItem(
                Zero1FiveMenus.Product.Name,
                l["Menu:Products"],
                "",
                icon: ""
            );

            productsMenu.AddItem(
                new ApplicationMenuItem(
                    Zero1FiveMenus.Product.List,
                    l["Menu:Products"],
                    Zero1FiveMenus.Product.ListUrl,
                    icon: "fas fa-list"
                ));

            if (await context.IsGrantedAsync(Zero1FivePermissions.Products.Default))
            {
                productsMenu.AddItem(
                    new ApplicationMenuItem(
                        Zero1FiveMenus.Product.Manage,
                        l["Menu:ManageProduct"],
                        Zero1FiveMenus.Product.ManageUrl,
                        icon: "fas fa-list",
                        requiredPermissionName: Zero1FivePermissions.Products.Default
                    ));
            }

            context.Menu.Items.Insert(1, productsMenu);
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            if (currentUser.IsAuthenticated)
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    "Account.Manage",
                    accountStringLocalizer["ManageYourProfile"],
                    $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
                    icon: "fa fa-cog",
                    order: 1000,
                    null));
            }

            return Task.CompletedTask;
        }
    }
}
