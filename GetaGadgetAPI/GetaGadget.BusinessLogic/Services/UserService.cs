using GetaGadget.Common.Enums;
using GetaGadget.Domain.DTO.User;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GetaGadget.BusinessLogic.Services
{
    public class UserService : BaseService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public User Register(RegisterModel model)
        {
            User user = null;

            model.EmailAddress = model.EmailAddress.Replace(" ", string.Empty);

            if (UnitOfWork.UserRepository.Get(model.EmailAddress) == null)
            {
                var salt = GenerateSalt();

                user = new User
                {
                    EmailAddress = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserRoleId = (int)UserRoleType.User,
                    Salt = salt,
                    PasswordHash = CreatePasswordHash(model.Password, salt)
                };

                UnitOfWork.UserRepository.Add(user);
            }

            Save();

            return user;
        }

        public User Login(string emailAddress, string password)
        {
            var user = UnitOfWork.UserRepository.Get(emailAddress.Replace(" ", string.Empty));

            if (user != null && user.PasswordHash == CreatePasswordHash(password, user.Salt))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        private string GenerateSalt()
        {
            byte[] salt = new byte[50 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private string CreatePasswordHash(string password, string salt)
        {
            using var sha1 = SHA1.Create();
            {
                var hashedBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
