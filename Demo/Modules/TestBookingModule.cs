using Nancy;

namespace Demo.Modules
{
    public class TestBookingModule : NancyModule
    {
        public TestBookingModule()
        {
            Get["/test/booking"] = _ => View["booking"];
        }
    }
}