using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Api.Models.Output
{
    public class UserModelOutput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
