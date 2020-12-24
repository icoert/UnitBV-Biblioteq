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

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            var logger = LogManager.GetLogger(typeof(Repository<TEntity>));
            try
            {
                var entity = Context.Set<TEntity>().Find(id);
                logger.Info($"The entity with id={id} was retrieved.");

                return entity;
            }
            catch (Exception ex)
            {
                logger.Info("Failed to get the entity.");
                logger.Error(ex.Message, ex);

                return null;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            var logger = LogManager.GetLogger(typeof(Repository<TEntity>));

            try
            {
                var entities = Context.Set<TEntity>().ToList();
                logger.Info("The entities were retrieved.");

                return entities;
            }
            catch (Exception ex)
            {
                logger.Info("Failed to get all entities.");
                logger.Error(ex.Message, ex);

                return null;
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var logger = LogManager.GetLogger(typeof(Repository<TEntity>));

            try
            {
                var entity = Context.Set<TEntity>().Where(predicate);
                logger.Info("The entity was found.");
                return entity;
            }
            catch (Exception ex)
            {
                logger.Info("Failed to find the entity.");
                logger.Error(ex.Message, ex);

                return null;
            }
        }

        public bool Add(TEntity entity)
        {
            var logger = LogManager.GetLogger(typeof(Repository<TEntity>));

            try
            {
                Context.Set<TEntity>().Add(entity);
                logger.Info("New entity was added.");

            }
            catch (Exception ex)
            {
                logger.Info("Failed to add the entity.");
                logger.Error(ex.Message, ex);

                return false;
            }

            return true;
        }

        public bool Remove(TEntity entity)
        {
            var logger = LogManager.GetLogger(typeof(Repository<TEntity>));

            try
            {
                Context.Set<TEntity>().Remove(entity);
                logger.Info("New entity was removed.");

                return true;
            }
            catch (Exception ex)
            {
                logger.Info("Failed to remove the entity.");
                logger.Error(ex.Message, ex);

                return false;
            }
        }
    }
}

