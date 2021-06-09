namespace Zero1Five.Blazor.Menus
{
    public class Zero1FiveMenus
    {
        private const string Prefix = "Zero1Five";
        public const string Home = Prefix + ".Home";

        public static class Category
        {
            public const string Name = Prefix + ".Categories";
            public const string AdminLink = "/manage/categories";
        }

        public static class Product
        {
            public const string Name = Prefix + ".Products";
            public const string List = Name + ".List";
            public const string ListUrl = "/products";
            public const string Manage = Prefix + ".Mange.Products";
            public const string ManageUrl = "/manage/products";
        }

        public  static class  Gig
        {
            public const string Name = Prefix + ".gigs";
            public const string List = Name + ".List";
            public const string ListUrl = "/gigs";
            public const string Manage = Prefix + ".Mange.Gigs";
            public const string ManageUrl = "/manage/gigs";   
        }
    }
}