using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Micro.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _CompanyService;
        public CompanyController(ICompanyService companyService)
        {
            _CompanyService = companyService;
        }
        [Authorize(Roles =SD.RoleCustomer)]
        public async Task<IActionResult> Index()
        {
            ResponseDto response = await _CompanyService.Get();
             if (response.IsSuccess && response.Result != null)
            {
                List<CompanyDto> companies = JsonConvert.DeserializeObject<List<CompanyDto>>(response.Result.ToString());
                return View(companies);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CompanyDto companyDto)
        {
            _CompanyService.Create(companyDto);
            return View();
        }
        [HttpGet("[controller]/Update/{companyId?}")]
        public async Task<IActionResult> Update(int companyId)
        {
            ResponseDto response = await _CompanyService.GetById(companyId);
            if (response.IsSuccess || response.Result != null)
            { 
                CompanyDto? company = JsonConvert.DeserializeObject<CompanyDto>(response.Result.ToString());
                return View(company);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CompanyDto companyDto)
        {
            var obj = await _CompanyService.Update(companyDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/Delete/{companyId?}")]
        public async Task<IActionResult> Delete(int companyId)
        {
            ResponseDto response = await _CompanyService.GetById(companyId);
            if (response.IsSuccess || response.Result != null)
            {
                CompanyDto? company = JsonConvert.DeserializeObject<CompanyDto>(response.Result.ToString());
                return View(company);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CompanyDto companyDto)
        {
            var obj = await _CompanyService.Delete(companyDto.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
