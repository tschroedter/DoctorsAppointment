namespace Demo.Modules
{
    using Nancy;

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/main"] = _ => View["main"];
        }
    }
}