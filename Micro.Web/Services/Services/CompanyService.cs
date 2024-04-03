using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;
using System.ComponentModel.Design;

namespace Micro.Web.Services.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseService _baseService;
        public CompanyService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> Get()
        {
            return await _baseService.SendAsync(new RequestDto() 
            {
                ApiType = Utility.SD.ApiType.GET,
                Data = null,
                Url = SD.ApiBase + "api/Company"
            },withBearer:true);
        }

        public async Task<ResponseDto> Create(CompanyDto companyDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = companyDto,
                Url = SD.ApiBase + "api/Company"
            },withBearer:true);
        }

        public async Task<ResponseDto> Update(CompanyDto companyDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.PUT,
                Data = companyDto,
                Url = SD.ApiBase + "api/Company"
            },withBearer: true);
        }

        public async Task<ResponseDto> GetById(int CompanyId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.ApiBase + "api/Company/" + CompanyId
            },withBearer:true);
        }

        public async Task<ResponseDto> Delete (int companyId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.DELETE,
                Url = SD.ApiBase + "api/Company/" + companyId
            },withBearer:true);
        }
    }
}
