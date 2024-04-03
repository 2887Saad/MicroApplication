using Micro.Web.Models.Dto;

namespace Micro.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);

    }
}
