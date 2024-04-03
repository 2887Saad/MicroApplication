using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;

namespace Micro.Web.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;
        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> Get()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Data = null,
                Url = SD.ApiBase + "api/Category"
            }, withBearer: true);
        }

        public async Task<ResponseDto> Create(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = categoryDto,
                Url = SD.ApiBase + "api/Category"
            }, withBearer: true);
        }

        public async Task<ResponseDto> Update(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.PUT,
                Data = categoryDto,
                Url = SD.ApiBase + "api/Category"
            }, withBearer: true);
        }

        public async Task<ResponseDto> GetById(int categoryId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.ApiBase + "api/Category/" + categoryId
            }, withBearer: true);
        }

        public async Task<ResponseDto> Delete(int categoryId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.DELETE,
                Url = SD.ApiBase + "api/Category/" + categoryId
            }, withBearer: true);
        }
    }
}
