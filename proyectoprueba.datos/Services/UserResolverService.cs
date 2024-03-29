﻿using Microsoft.AspNetCore.Http;

namespace proyectoprueba.Web.Services.Security
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string GetUser()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }
    }
}
