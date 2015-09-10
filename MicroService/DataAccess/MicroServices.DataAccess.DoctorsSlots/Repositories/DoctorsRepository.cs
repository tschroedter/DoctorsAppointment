using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class DoctorsRepository
        : BaseRepository <IDoctor, IDoctorsContext>,
          IDoctorsRepository
    {
        public DoctorsRepository([NotNull] IDoctorsContext context)
            : base(context)
        {
        }

        public IEnumerable <IDoctor> FindByLastName(string lastName)
        {
            IQueryable <IDoctor> doctor = All.Where(x => x.LastName == lastName);

            return doctor;
        }

        public IDoctor Delete(int id)
        {
            return Context.Delete(id);
        }

        protected override IQueryable <IDoctor> GetAll()
        {
            return Context.Doctors();
        }
    }
}