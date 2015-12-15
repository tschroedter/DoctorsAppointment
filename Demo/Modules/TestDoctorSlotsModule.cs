using Nancy;

namespace Demo.Modules
{
    public class TestDoctorSlotsModule : NancyModule
    {
        public TestDoctorSlotsModule()
        {
            Get [ "/test/doctorSlots" ] = _ => View [ "doctorSlots" ];
        }
    }
}