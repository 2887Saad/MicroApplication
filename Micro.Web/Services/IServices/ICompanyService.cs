using Micro.Web.Models.Dto;

namespace Micro.Web.Services.IServices
{
    public interface ICompanyService
    {
        public Task<ResponseDto> Get();
        public Task<ResponseDto> Create(CompanyDto companyDto);
        public Task<ResponseDto> Update(CompanyDto companyDto);
        public Task<ResponseDto> Delete(int CompanyId);
        public Task<ResponseDto> GetById(int CompanyId);
        

    }
}
