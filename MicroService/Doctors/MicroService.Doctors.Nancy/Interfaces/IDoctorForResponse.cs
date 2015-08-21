namespace MicroServices.Doctors.Nancy.Interfaces
{
    public interface IDoctorForResponse
    {
        string LastName { get; }
        string FirstName { get; }
        int Id { get; }
    }
}