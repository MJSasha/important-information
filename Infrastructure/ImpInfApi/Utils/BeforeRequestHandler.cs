using ImpInfCommon.Data.Definitions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImpInfApi.Utils
{
    public class BeforeRequestHandler
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;

        public BeforeRequestHandler(RequestDelegate next, AppSettings appSettings)
        {
            _next = next;
            this.appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            

            await _next(context);
        }
    }
}
