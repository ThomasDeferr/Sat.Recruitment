using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Data.Infraestructure
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
