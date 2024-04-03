using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using static Micro.Web.Utility.SD;
using System.Net;
using System;
using Micro.Web.Utility;

namespace Micro.Web.Services.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _ITokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider iTokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _ITokenProvider = iTokenProvider;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer)
        {
            try { 
                HttpClient Client = _httpClientFactory.CreateClient("Micro");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                if (withBearer)
                {
                    string token = _ITokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }
                message.RequestUri = new Uri(requestDto.Url);
                if(requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE: 
                        message.Method = HttpMethod.Delete; 
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }
                HttpResponseMessage? ApiResponse = null;
                ApiResponse = await Client.SendAsync(message);
                switch (ApiResponse.StatusCode)
                    {
                    case HttpStatusCode.NotFound:
                        return new()
                        {
                            IsSuccess = false,
                            Message = ErrorMessages.NotFound
                        };
                    case HttpStatusCode.Forbidden:
                        return new()
                        {
                            IsSuccess = false,
                            Message = ErrorMessages.AccessDenied
                        };
                    case HttpStatusCode.Unauthorized:
                        return new()
                        {
                            IsSuccess = false,
                            Message = ErrorMessages.Unauthorized
                        };
                    case HttpStatusCode.InternalServerError:
                        return new()
                        {
                            IsSuccess = false,
                            Message = ErrorMessages.InternalServerError
                        };
                    default:
                        var ApiContent = await ApiResponse.Content.ReadAsStringAsync();
                        ResponseDto? ApiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(ApiContent);
                        return ApiResponseDto;
                }
            }
            catch (Exception ex)
            {

                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString(),
                };
            }
        }
    }
}
