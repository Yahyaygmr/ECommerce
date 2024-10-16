using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Exceptions.AppUserExceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Kullanıcı adı veya şifre hatalı !")
        {
        }
        public AuthenticationErrorException(string? message) : base(message)
        {
        }
        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
