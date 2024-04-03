using Micro.Services.CompanyAPI.Data;
using Micro.Services.CompanyAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Micro.Services.CompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
/*using Micro.Identity;*/
using Micro.Services.CompanyAPI.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Micro.Services.CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly DBContext _DBContext;
        private readonly ResponseDto _ResponseDto;
        private readonly IMapper _Mapper;
        public CompanyController(DBContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _ResponseDto = new();
            _Mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Company> companies = await _DBContext.Companies.ToListAsync();
                _ResponseDto.Result = _Mapper.Map<IEnumerable<CompanyDto>>(companies);
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
        [HttpPost]
        public async Task<ResponseDto> Create([FromBody] CompanyDto company)
        {
            try
            {
                Company obj = _Mapper.Map<Company>(company);
                await _DBContext.Companies.AddAsync(obj);
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
        [Route("{id:int}")]
        public async Task<ResponseDto> GetById(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var obj = await _DBContext.Companies.FirstOrDefaultAsync(x=>x.Id == id); //predicate logic
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
            catch(Exception ex) 
            {
                _ResponseDto.Message = ex.Message;
                _ResponseDto.IsSuccess = false;
                return _ResponseDto;

            }
        }
        [HttpPut]
        public async Task<ResponseDto> Update([FromBody] CompanyDto company)
        {
            try
            {
                if (company != null)
                {
                    Company obj = _Mapper.Map<Company>(company);
                    Company? ToUpdate = await _DBContext.Companies.FindAsync(obj.Id);
                    if (ToUpdate != null)
                    {
                        ToUpdate.Name = obj.Name;
                        ToUpdate.EmailAddress = obj.EmailAddress;
                        ToUpdate.PhoneNumber = obj.PhoneNumber;
                        ToUpdate.Address = company.Address;
                        ToUpdate.Status = obj.Status;
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
                    Company? ToDelete = await _DBContext.Companies.FindAsync(id);
                    if (ToDelete != null)
                    {
                        _DBContext.Companies.Remove(ToDelete);
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
    }
}
