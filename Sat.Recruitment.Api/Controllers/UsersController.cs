using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models.Input;
using Sat.Recruitment.Api.Models.Output;
using Sat.Recruitment.Business.Infraestructure;
using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserBusiness _userBusiness;

        public UsersController(IMapper mapper, IUserBusiness userBusiness)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userBusiness = userBusiness ?? throw new ArgumentNullException(nameof(userBusiness));
        }


        [HttpPost]
        public async Task<ActionResult<UserModelOutput>> CreateUser([FromBody] UserModelInput userModelInput)
        {
            User userInput = _mapper.Map<User>(userModelInput);

            User userCreated = await _userBusiness.Create(userInput);

            UserModelOutput userModelOutput = _mapper.Map<UserModelOutput>(userCreated);

            return new CreatedAtRouteResult(null, userModelOutput);
        }
    }
}
