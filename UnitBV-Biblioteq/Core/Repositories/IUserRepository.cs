using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Users { get; }

        bool EditUser(User user);
    }
}
