﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        Dtos.TokenDtos.Token CreateAccessToken(int minute);
    }
}
