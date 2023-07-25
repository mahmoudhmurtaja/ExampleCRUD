using ExampleCRUDwhitAjax.Models;
using ExampleCRUDwhitAjax.Resources;
using ExampleCRUDwhitAjax.Services;
using ExampleCRUDwhitAjax.Services.Employes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExampleCRUDwhitAjax.Controllers
{
    public class EmployeController : Controller
    {
        private readonly IEmployesService _employesService;
        public EmployeController(IEmployesService employesService)
        {
            _employesService = employesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View();


        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            var inputSearch = Request.Form["search[value]"];
            var obj = !string.IsNullOrEmpty(inputSearch)
                ? JsonConvert.DeserializeObject<Employe>(inputSearch) : new Employe();

            var result = await _employesService.GetAllAsync(new PagedResultRequestDto<Employe>
            {
                SearchValue = obj,
                SortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")],
                SortColumnDirection = Request.Form["order[0][dir]"],
                PageSize = int.Parse(Request.Form["length"]),
                Skip = int.Parse(Request.Form["start"])
            });

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(int id)
        {
            var employe = await _employesService.GetByIdAsync(id);
            if (employe != null)
                return PartialView("_CreateEditModal", employe);

            return PartialView("_CreateEditModal", new Employe());
        }

        [HttpPost]
        public async Task<OperationResult> CreateEdit(Employe input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                var message = string.Join("<br>  ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                result.Message = message;
                return result;
            }

            return await _employesService.CreateEditAsync(input);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            return await _employesService.DeleteAsync(id);
        }
    }
}
