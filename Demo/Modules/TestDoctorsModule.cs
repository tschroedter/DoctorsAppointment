using Nancy;

namespace Demo.Modules
{
    public class TestDoctorsModule : NancyModule
    {
        public TestDoctorsModule()
        {
            Get [ "/test/doctors" ] = _ => View [ "doctors" ];
        }
    }
}