using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Data.Infraestructure
{
    public class UserData : IUserData
    {
        public UserData()
        {
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            List<User> result = null;

            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + "/Files/Users.json"))
            {
                string json = await reader.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<User>>(json);
            }

            return result;
        }
    }
}
