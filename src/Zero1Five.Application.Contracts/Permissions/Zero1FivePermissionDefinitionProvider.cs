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
            var messagePermissions = CategoryGroup.AddPermission(Zero1FivePermissions.Category.Default, L("Permission:Category"));
            messagePermissions.AddChild(Zero1FivePermissions.Category.Create, L("Permission:Create"));
            messagePermissions.AddChild(Zero1FivePermissions.Category.Update, L("Permission:Update"));
            messagePermissions.AddChild(Zero1FivePermissions.Category.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Zero1FiveResource>(name);
        }
    }
}
