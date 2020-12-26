using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using log4net;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Repository<TEntity>));

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            try
            {
                var entity = Context.Set<TEntity>().Find(id);
                Logger.Info($"The entity with id={id} was retrieved.");

                return entity;
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to get the entity.");
                Logger.Error(ex.Message, ex);

                return null;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                var entities = Context.Set<TEntity>().ToList();
                Logger.Info("The entities were retrieved.");

                return entities;
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to get all entities.");
                Logger.Error(ex.Message, ex);

                return null;
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = Context.Set<TEntity>().Where(predicate);
                Logger.Info("The entity was found.");
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to find the entity.");
                Logger.Error(ex.Message, ex);

                return null;
            }
        }

        public bool Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
                
                Context.SaveChanges();

                Logger.Info("New entity was added.");

            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add the entity.");
                Logger.Error(ex.Message, ex);

                return false;
            }

            return true;
        }

        public bool Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                
                Context.SaveChanges();

                Logger.Info("New entity was removed.");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to remove the entity.");
                Logger.Error(ex.Message, ex);

                return false;
            }
        }
    }
}

