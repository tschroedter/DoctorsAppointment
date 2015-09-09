using System;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;

namespace MicroServices.DataAccess.DoctorsSlots.Repositories
{
    public abstract class BaseRepository <TType, TContext>
        : IRepository <TType>
        where TType : IEntity
        where TContext : IDbContext <TType>
    {
        protected BaseRepository([NotNull] TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; private set; }

        public IQueryable <TType> All
        {
            get
            {
                return GetAll();
            }
        }

        public TType FindById(int id)
        {
            return Context.Find(id);
        }

        public void Save(TType instance)
        {
            try
            {
                if ( instance.Id == default ( int ) )
                {
                    Context.Add(instance);
                }
                else
                {
                    Context.SetStateForSlot(instance,
                                            EntityState.Modified);
                }

                Context.SaveChanges(); // todo testing
            }
            catch ( Exception exception )
            {
                throw exception; // todo make catch everywhere and nice
            }
        }

        public void Remove(TType instance)
        {
            Context.Remove(instance);
            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected abstract IQueryable <TType> GetAll();
    }
}