namespace Demo.Modules
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => View["index"];
        }
    }
}