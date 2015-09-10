using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class DaysRepository
        : BaseRepository <IDay, IDaysContext>,
          IDaysRepository
    {
        public DaysRepository([NotNull] IDaysContext context)
            : base(context)
        {
        }

        public IEnumerable <IDay> FindByDoctorId(int doctorId)
        {
            IQueryable <IDay> days = All.Where(x => x.DoctorId == doctorId);

            return days;
        }

        public IEnumerable <IDay> FindByDate(DateTime dateTime)
        {
            IDay[] days = All.ToArray();

            IEnumerable <IDay> forDateTime = days.Where(x => x.Date.Date == dateTime.Date);

            return forDateTime;
        }

        protected override IQueryable <IDay> GetAll()
        {
            return Context.Days();
        }
    }
}