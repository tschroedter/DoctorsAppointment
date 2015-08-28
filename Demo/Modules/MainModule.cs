using Nancy;

namespace Demo.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get [ "/main" ] = _ => View [ "main" ];
        }
    }
}