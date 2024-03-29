﻿using Zero1Five.Localization;
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

            var GigsGroup = context.AddGroup(Zero1FivePermissions.GigsGroup, L("ManageGigs"));
            var GigPermissions = GigsGroup.AddPermission(Zero1FivePermissions.Gigs.Default, L("Permission:Gig"));
            GigPermissions.AddChild(Zero1FivePermissions.Gigs.Create, L("Permission:Create"));
            GigPermissions.AddChild(Zero1FivePermissions.Gigs.Edit, L("Permission:Edit"));
            GigPermissions.AddChild(Zero1FivePermissions.Gigs.Delete, L("Permission:Delete"));
            GigPermissions.AddChild(Zero1FivePermissions.Gigs.Publish, L("Permission:Publish"));

            var productPermissions = GigsGroup.AddPermission(Zero1FivePermissions.Products.Default, L("Permission:Product"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Create, L("Permission:Create"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Edit, L("Permission:Edit"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Delete, L("Permission:Delete"));
            productPermissions.AddChild(Zero1FivePermissions.Products.Publish, L("Permission:Publish"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Zero1FiveResource>(name);
        }
    }
}
