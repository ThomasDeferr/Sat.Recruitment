using System.Threading.Tasks;
using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Business.Infraestructure
{
    public interface IUserBusiness
    {
        Task<User> Create(User newUser);
    }
}
