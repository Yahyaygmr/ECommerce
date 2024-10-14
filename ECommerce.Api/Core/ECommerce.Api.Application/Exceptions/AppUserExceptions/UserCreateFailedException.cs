using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Exceptions.AppUserExceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException():base("Kullanıcı Oluşturulurken Beklenmeyen Bir Hata Oluştu!")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
