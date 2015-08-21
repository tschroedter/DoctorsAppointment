using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Doctors.Nancy.Interfaces;

namespace MicroServices.Doctors.Nancy
{
    public sealed class DoctorForResponse : IDoctorForResponse
    {
        public DoctorForResponse([NotNull] IDoctor doctor)
        {
            Id = doctor.Id;
            FirstName = doctor.FirstName;
            LastName = doctor.LastName;
        }

        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public int Id { get; private set; }
    }
}