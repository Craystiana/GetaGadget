using GetaGadget.Domain.Entities;

namespace GetaGadget.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User Get(string emailAddress);
    }
}
