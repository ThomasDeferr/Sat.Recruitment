using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Business.Common;
using Sat.Recruitment.Data.Infraestructure;
using Sat.Recruitment.Entities.Models;
using Sat.Recruitment.Exceptions.Controller;
using Sat.Recruitment.Resources;

namespace Sat.Recruitment.Business.Infraestructure
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserData _userData;

        public UserBusiness(IUserData userData)
        {
            _userData = userData ?? throw new ArgumentNullException(nameof(userData));
        }


        public async Task<User> Create(User newUser)
        {
            newUser.Email = StringExtensions.NormalizeEmail(newUser.Email);
            newUser.Money += CalculateGifMoney(newUser);

            IEnumerable<User> usersFromFile = await _userData.GetUsers();

            bool isDuplicated = usersFromFile.ToList().Exists(u => IsSameUser(newUser, u));

            if (isDuplicated)
                throw new BadRequestException(error: ValidationErrors.UserDuplicated, message: ValidationMessages.UserDuplicated);

            return newUser;
        }


        #region Private methods
        private decimal CalculateGifMoney(User user)
        {
            int gifPercentage = 0;

            switch (user.UserType)
            {
                case UserType.Normal:
                    if (user.Money > 100)
                    {
                        gifPercentage = 80;
                    }
                    else if (10 < user.Money && user.Money < 100)
                    {
                        gifPercentage = 12;
                    }
                    break;
                case UserType.SuperUser:
                    if (user.Money > 100)
                    {
                        gifPercentage = 20;
                    }
                    break;
                case UserType.Premium:
                    if (user.Money > 100)
                    {
                        gifPercentage = 200;
                    }
                    break;
                default:
                    throw new BadRequestException(error: ValidationErrors.UserTypeInvalid, message: ValidationMessages.UserTypeInvalid);
            }

            decimal gifMoney = user.Money * gifPercentage / 100;
            return gifMoney;
        }

        private bool IsSameUser(User userA, User userB)
        {
            bool isSame = false;

            isSame |= userA.Email == userB.Email;
            isSame |= userA.Phone == userB.Phone;
            isSame |= userA.Name == userB.Name && userA.Address == userB.Address;

            return isSame;
        }
        #endregion
    }
}
