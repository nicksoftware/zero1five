namespace Zero1Five.Permissions
{
    public static class Zero1FivePermissions
    {
        public const string GroupName = "Zero1Five";
        public const string CategoryGroup = GroupName + ".Category";

        public static class Category
        {
            public const string Default = CategoryGroup + ".Category";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}