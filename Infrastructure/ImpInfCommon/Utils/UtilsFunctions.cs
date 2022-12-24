using ImpInfCommon.Exceptions;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.Utils
{
    public static class UtilsFunctions
    {
        public static T GetRefitService<T>(HttpClient httpClient)
        {
            return RestService.For<T>(httpClient, new RefitSettings
            {
                ExceptionFactory = ThrowErrorResponseException
            });
        }

        private static async Task<Exception> ThrowErrorResponseException(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode) return new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
            return null;
        }
    }
}
