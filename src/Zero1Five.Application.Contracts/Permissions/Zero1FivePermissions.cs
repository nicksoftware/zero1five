namespace Zero1Five.Permissions
{
    public static class Zero1FivePermissions
    {
        public const string GroupName = "Zero1Five";
        public const string CategoryGroup = GroupName + ".Category";
        public const string ProductGroup = GroupName + ".Product";
        public static class Categories
        {
            public const string Default = CategoryGroup + ".Category";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Products
        {
            public const string Default = ProductGroup + ".Product";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string Publish = Default + ".Publish";
        }
    }
}