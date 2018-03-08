using System.Threading.Tasks;
using Eventaris.Domain;

namespace EventarisXDal
{
    public interface IUserService
    {
        User GetById(int id);
        User GetByName(string firstName, string lastName);

        Task<bool> New(User user);  
        Task<bool> Update(User user);

    }
}
    