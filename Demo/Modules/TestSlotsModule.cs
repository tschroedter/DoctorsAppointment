using Nancy;

namespace Demo.Modules
{
    public class TestSlotsModule : NancyModule
    {
        public TestSlotsModule()
        {
            Get [ "/test/slots" ] = _ => View [ "slots" ];
        }
    }
}