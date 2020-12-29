// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-27-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="UserRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using UnitBV_Biblioteq.Core.DomainModel;
    using UnitBV_Biblioteq.Core.Repositories;
    /// <summary>
    /// Class UserRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.User}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IUserRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.User}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IUserRepository" />
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserRepository));

        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>The users.</value>
        public IEnumerable<User> Users => AppDbContext.Set<User>();

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public new bool Add(User user)
        {
            try
            {
                if (user == null)
                {
                    Logger.Info("Failed to add null user.");
                    return false;
                }
                if (!this.IsValid(user))
                {
                    Logger.Info("Failed to add user.");
                    return false;
                }

                AppDbContext.Users.Add(user);
                AppDbContext.SaveChanges();
                Logger.Info($"New user was added(id={user.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add user.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return false;
                }
                var existing = AppDbContext.Users.FirstOrDefault(a => a.Id == user.Id);
                if (existing != null)
                {
                    if (!this.IsValid(user))
                    {
                        Logger.Info("Failed to edit user.");
                        return false;
                    }
                    existing.Firstname = user.Firstname;
                    existing.Lastname = user.Lastname;

                    AppDbContext.SaveChanges();

                    Logger.Info($"User with id={user.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(user != null ? $"Failed to update user with id={user.Id}." : $"Failed to update user.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user is valid; otherwise, <c>false</c>.</returns>
        private bool IsValid(User user)
        {
            if ((string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email)) &&
                (string.IsNullOrEmpty(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.PhoneNumber)) &&
                (string.IsNullOrEmpty(user.Address) || string.IsNullOrWhiteSpace(user.Address)))
            {
                Logger.Info("Email, Phone number or Address must be provided.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrEmpty(user.Email))
            {
                user.Email = user.Firstname + user.Lastname + "@noEmail.com";

                if (string.IsNullOrEmpty(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    if (!user.HasValidAddress())
                    {
                        Logger.Info("Address is invalid.");
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(user.Address) || string.IsNullOrWhiteSpace(user.Address))
                    {
                        if (!user.HasValidPhoneNumber())
                        {
                            Logger.Info("Phone number is invalid.");
                            return false;
                        }
                    }
                    else
                    {
                        if (!user.HasValidPhoneNumber() && !user.HasValidAddress())
                        {
                            Logger.Info("Phone number and Address are invalid.");
                            return false;
                        }
                    }
                }
            }
            else
            {
                if ((string.IsNullOrEmpty(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.PhoneNumber)) &&
                    (string.IsNullOrEmpty(user.Address) || string.IsNullOrWhiteSpace(user.Address)))
                {

                    if (!user.HasValidEmail())
                    {
                        Logger.Info("Email is invalid.");
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(user.PhoneNumber) || string.IsNullOrWhiteSpace(user.PhoneNumber))
                    {
                        if (!user.HasValidAddress() && user.HasValidEmail())
                        {
                            Logger.Info("Address is invalid.");
                            return false;
                        }

                        if (user.HasValidAddress() && !user.HasValidEmail())
                        {
                            Logger.Info("Email is invalid.");
                            return false;
                        }

                        if (!user.HasValidAddress() && !user.HasValidEmail())
                        {
                            Logger.Info("Email and Address are invalid.");
                            return false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.Address) || string.IsNullOrWhiteSpace(user.Address))
                        {
                            if (!user.HasValidPhoneNumber() && user.HasValidEmail())
                            {
                                Logger.Info("Phone number is invalid.");
                                return false;
                            }

                            if (user.HasValidPhoneNumber() && !user.HasValidEmail())
                            {
                                Logger.Info("Email is invalid.");
                                return false;
                            }

                            if (!user.HasValidPhoneNumber() && !user.HasValidEmail())
                            {
                                Logger.Info("Email and Phone are invalid.");
                                return false;
                            }
                        }
                        else
                        {
                            if (user.HasValidPhoneNumber() && !user.HasValidAddress() && !user.HasValidEmail())
                            {
                                Logger.Info("Email and Address are invalid.");
                                return false;
                            }

                            if (!user.HasValidPhoneNumber() && user.HasValidAddress() && !user.HasValidEmail())
                            {
                                Logger.Info("Email, Phone number are invalid.");
                                return false;
                            }

                            if (!user.HasValidPhoneNumber() && !user.HasValidAddress() && user.HasValidEmail())
                            {
                                Logger.Info("Phone number and Address are invalid.");
                                return false;
                            }

                            if (user.HasValidPhoneNumber() && user.HasValidAddress() && !user.HasValidEmail())
                            {
                                Logger.Info("Email is invalid.");
                                return false;
                            }

                            if (!user.HasValidPhoneNumber() && user.HasValidAddress() && user.HasValidEmail())
                            {
                                Logger.Info("Phone number is invalid.");
                                return false;
                            }

                            if (user.HasValidPhoneNumber() && user.HasValidAddress() && !user.HasValidEmail())
                            {
                                Logger.Info("Address is invalid.");
                                return false;
                            }

                            if (!user.HasValidPhoneNumber() && !user.HasValidAddress() && !user.HasValidEmail())
                            {
                                Logger.Info("Email, Phone number and Address are invalid.");
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
