// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="Repository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using log4net;
    using UnitBV_Biblioteq.Core.Repositories;
    /// <summary>
    /// Class Repository.
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{TEntity}" />
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Repository<TEntity>));

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TEntity.</returns>
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

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
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

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
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

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    Logger.Info("Failed to add null entity.");
                    return false;
                }
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

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Remove(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    Logger.Info("Failed to remove null entity.");
                    return false;
                }
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

