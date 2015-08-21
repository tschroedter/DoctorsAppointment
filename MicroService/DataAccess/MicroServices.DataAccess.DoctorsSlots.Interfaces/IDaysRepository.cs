using System;
using System.Collections.Generic;

namespace MicroServices.DataAccess.DoctorsSlots.Interfaces
{
    public interface IDaysRepository : IRepository <IDay>
    {
        IEnumerable <IDay> FindByDoctorId(int doctorId);
        IEnumerable <IDay> FindByDate(DateTime dateTime);
    }
}