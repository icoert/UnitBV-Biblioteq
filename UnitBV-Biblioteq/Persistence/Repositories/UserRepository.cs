using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserRepository));
        private AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<User> Users => AppDbContext.Set<User>();

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
