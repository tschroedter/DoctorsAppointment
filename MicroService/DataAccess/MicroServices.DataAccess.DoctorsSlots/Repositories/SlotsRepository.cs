using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public sealed class SlotsRepository
        : BaseRepository <ISlot, ISlotsContext>,
          ISlotsRepository
    {
        public SlotsRepository([NotNull] ISlotsContext context)
            : base(context)
        {
        }

        protected override IQueryable <ISlot> GetAll()
        {
            return Context.Slots();
        }
    }
}