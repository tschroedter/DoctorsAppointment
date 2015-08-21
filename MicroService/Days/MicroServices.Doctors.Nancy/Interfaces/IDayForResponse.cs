using System;

namespace MicroServices.Days.Nancy.Interfaces
{
    public interface IDayForResponse
    {
        DateTime Date { get; set; }
        int DoctorId { get; set; }
        int Id { get; set; }
    }
}