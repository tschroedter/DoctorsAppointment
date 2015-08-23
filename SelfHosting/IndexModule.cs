namespace SelfHosting
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index"];
        }
    }
}