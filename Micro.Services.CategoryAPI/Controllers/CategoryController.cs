using AutoMapper;
using Micro.Services.CategoryAPI.Data;
using Micro.Services.CategoryAPI.Models;
using Micro.Services.CategoryAPI.Models.Dto;
using Micro.Services.CategoryAPI.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro.Services.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DBContext _DBContext;
        private readonly ResponseDto _ResponseDto;
        private readonly IMapper _Mapper;
        public CategoryController(DBContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _Mapper = mapper;
            _ResponseDto = new();
        }

        [HttpPost]
        public async Task<ResponseDto> Create([FromBody] CategoryDto obj)
        {
            try
            {
                Category category = _Mapper.Map<Category>(obj);
                await _DBContext.Categories.AddAsync(category);
                await _DBContext.SaveChangesAsync();
                return _ResponseDto;
            }
            catch (Exception ex)
            {
                _ResponseDto.Message = ex.Message;
                _ResponseDto.IsSuccess = false;
                return _ResponseDto;
            }
        }
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Category> categories = await _DBContext.Categories.ToListAsync();
                _ResponseDto.Result = _Mapper.Map<IEnumerable<CategoryDto>>(categories);
                return _ResponseDto;
            }
            catch (Exception ex)
            {
                _ResponseDto.Result = null;
                _ResponseDto.IsSuccess = false;
                _ResponseDto.Message = ex.Message;
                return _ResponseDto;

            }
        }
        [HttpPut]
        public async Task<ResponseDto> Update([FromBody] CategoryDto obj)
        {
            try
            {
                if (obj != null)
                {
                    Category category = _Mapper.Map<Category>(obj);
                    Category? ToUpdate = await _DBContext.Categories.FindAsync(obj.Id);
                    if (ToUpdate != null)
                    {
                        ToUpdate.Name = obj.Name;
                        ToUpdate.Type = obj.Type;
                        ToUpdate.Description = obj.Description;
                        await _DBContext.SaveChangesAsync();
                        _ResponseDto.IsSuccess = true;
                        _ResponseDto.Message = "";
                        return _ResponseDto;
                    }
                    else
                    {
                        _ResponseDto.IsSuccess = true;
                        _ResponseDto.Message = ErrorMessages.NotFound;
                        return _ResponseDto;
                    }
                }
                else
                {
                    _ResponseDto.IsSuccess = true;
                    _ResponseDto.Message = ErrorMessages.DataNotAvailable;
                    return _ResponseDto;
                }
            }
            catch (Exception ex)
            {
                _ResponseDto.Message = ex.Message;
                _ResponseDto.IsSuccess = false;
                return _ResponseDto;
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    Category? ToDelete = await _DBContext.Categories.FindAsync(id);
                    if (ToDelete != null)
                    {
                        _DBContext.Categories.Remove(ToDelete);
                        await _DBContext.SaveChangesAsync();
                        _ResponseDto.IsSuccess = true;
                        _ResponseDto.Message = "";
                        return _ResponseDto;
                    }
                    else
                    {
                        _ResponseDto.IsSuccess = false;
                        _ResponseDto.Message = ErrorMessages.NotFound;
                        return _ResponseDto;
                    }
                }
                else
                {
                    _ResponseDto.IsSuccess = true;
                    _ResponseDto.Message = ErrorMessages.DataNotAvailable;
                    return _ResponseDto;
                }
            }
            catch (Exception ex)
            {
                _ResponseDto.Message = ex.Message;
                _ResponseDto.IsSuccess = false;
                return _ResponseDto;
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var obj = await _DBContext.Categories.FirstOrDefaultAsync(x => x.Id == id); //predicate logic
                    if (obj != null)
                    {
                        _ResponseDto.IsSuccess = true;
                        _ResponseDto.Result = obj;
                        _ResponseDto.Message = "";
                        return _ResponseDto;
                    }
                    else
                    {
                        _ResponseDto.Message = ErrorMessages.NotFound;
                        _ResponseDto.IsSuccess = false;
                        return _ResponseDto;
                    }
                }
                else
                {
                    _ResponseDto.IsSuccess = true;
                    _ResponseDto.Message = ErrorMessages.DataNotAvailable;
                    return _ResponseDto;
                }

            }
            catch (Exception ex)
            {
                _ResponseDto.Message = ex.Message;
                _ResponseDto.IsSuccess = false;
                return _ResponseDto;

            }
        }
    }
}
