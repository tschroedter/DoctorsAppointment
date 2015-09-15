using System;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Days.Nancy.Interfaces;

namespace MicroServices.Days.Nancy
{
    public class DayForResponse : IDayForResponse
    {
        public DayForResponse()
        {
        }

        public DayForResponse([NotNull] IDay day)
        {
            Id = day.Id;
            DoctorId = day.DoctorId;
            Date = day.Date;
        }

        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int Id { get; set; }
    }
}