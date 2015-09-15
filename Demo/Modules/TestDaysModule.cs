using Nancy;

namespace Demo.Modules
{
    public class TestDaysModule : NancyModule
    {
        public TestDaysModule()
        {
            Get [ "/test/days" ] = _ => View [ "days" ];
        }
    }
}