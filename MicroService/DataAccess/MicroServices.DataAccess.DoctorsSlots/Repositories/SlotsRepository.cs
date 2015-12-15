using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class SlotsRepository
        : BaseRepository <ISlot, ISlotsContext>,
          ISlotsRepository
    {
        public SlotsRepository([NotNull] ISlotsContext context)
            : base(context)
        {
        }

        public IEnumerable <ISlot> FindByDayId(int dayId)
        {
            IQueryable <ISlot> slots = All.Where(x => x.DayId == dayId);

            return slots;
        }

        protected override IQueryable <ISlot> GetAll()
        {
            return Context.Slots();
        }
    }
}