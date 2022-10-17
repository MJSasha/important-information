﻿using ImpInfCommon.Data.Other;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBotLib.Exceptions;

namespace ImpInfCommon.ApiServices
{
    public class AuthService : BaseService
    {
        public AuthService(string backRoot, string entityRoot = null, string token = "") : base(entityRoot, backRoot, token) { }

        public async Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            var json = Serialize(registrationModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/" + chatId.ToString(), data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode,
                                                                await httpResponse.Content.ReadAsStringAsync());
        }
    }
}