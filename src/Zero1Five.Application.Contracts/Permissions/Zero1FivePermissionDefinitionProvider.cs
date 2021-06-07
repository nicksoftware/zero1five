using Zero1Five.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Zero1Five.Permissions
{
    public class Zero1FivePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(Zero1FivePermissions.GroupName);

            var CategoryGroup = context.AddGroup(Zero1FivePermissions.CategoryGroup, L("Menu:ManageCategory"));
            var messagePermissions = CategoryGroup.AddPermission(Zero1FivePermissions.Categories.Default, L("Permission:Category"));
            messagePermissions.AddChild(Zero1FivePermissions.Categories.Create, L("Permission:Create"));
            messagePermissions.AddChild(Zero1FivePermissions.Categories.Edit, L("Permission:Edit"));
            messagePermissions.AddChild(Zero1FivePermissions.Categories.Delete, L("Permission:Delete"));

            var productsGroup = context.AddGroup(Zero1FivePermissions.ProductGroup, L("Menu:ManageProduct"));
            var productPermissions = productsGroup.AddPermission(Zero1FivePermissions.Products.Default, L("Permission:Product"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Create, L("Permission:Create"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Edit, L("Permission:Edit"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Zero1FiveResource>(name);
        }
    }
}
