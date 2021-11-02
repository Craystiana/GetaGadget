using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces.Repositories;
using System.Linq;

namespace GetaGadget.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GetaGadgetContext context) : base(context) { }


        public User Get(string emailAddress)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == emailAddress);
        }
    }
}
