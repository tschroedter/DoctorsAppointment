using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
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

        public IDoctor Create() // todo can be removed???
        {
            return Context.Create("FirstName",
                                  "LastName");
        }

        // todo testing
        public IDoctor Create(string firstName,
                              string lastName)
        {
            return Context.Create(firstName,
                                  lastName);
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