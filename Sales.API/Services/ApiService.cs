﻿using Newtonsoft.Json;
using Sales.Shared.ResponsesApi;

namespace Sales.API.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _urlBase;
        private readonly string _tokenName;
        private readonly string _tokenValue;

        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlBase = _configuration["CoutriesAPI:urlBase"]!;
            _tokenName = _configuration["CoutriesAPI:tokenName"]!;
            _tokenValue = _configuration["CoutriesAPI:tokenValue"]!;
        }

        public async Task<ResponseGeneric> GetListAsync<T>(string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new()
                {
                    BaseAddress = new Uri(_urlBase),
                };

                client.DefaultRequestHeaders.Add(_tokenName, _tokenValue);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ResponseGeneric
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result)!;
                return new ResponseGeneric
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new ResponseGeneric
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
