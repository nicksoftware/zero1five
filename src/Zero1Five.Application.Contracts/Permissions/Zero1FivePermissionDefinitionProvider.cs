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

            //Define your own permissions here. Example:
            //myGroup.AddPermission(Zero1FivePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Zero1FiveResource>(name);
        }
    }
}
