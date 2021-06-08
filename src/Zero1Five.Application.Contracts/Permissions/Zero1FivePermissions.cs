namespace Zero1Five.Permissions
{
    public static class Zero1FivePermissions
    {
        public const string GroupName = "Zero1Five";
        public const string CategoryGroup = GroupName + ".Category";
        public const string GigsGroup = GroupName + ".Gigs";
        public static class Categories
        {
            public const string Default = CategoryGroup + ".Category";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Products
        {
            public const string Default = GigsGroup + ".Product";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string Publish = Default + ".Publish";
        }

        public static class Gigs
        {
            public const string Default = GigsGroup + ".Gig";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string Publish = Default + ".Publish";
        }
    }
}