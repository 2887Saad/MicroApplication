using Micro.Web.Models.Dto;

namespace Micro.Web.Services.IServices
{
    public interface ICategoryService
    {
        public Task<ResponseDto> Get();
        public Task<ResponseDto> Create(CategoryDto categoryDto);
        public Task<ResponseDto> Update(CategoryDto categoryDto);
        public Task<ResponseDto> Delete(int categoryId);
        public Task<ResponseDto> GetById(int categoryId);
    }
}
