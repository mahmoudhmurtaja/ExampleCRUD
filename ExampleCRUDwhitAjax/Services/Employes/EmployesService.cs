using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleCRUDwhitAjax.Models;
using ExampleCRUDwhitAjax.Data;
using ExampleCRUDwhitAjax.Resources;

namespace ExampleCRUDwhitAjax.Services.Employes
{
    public class EmployesService : IEmployesService
    {
        private readonly ApplicationDbContext _context;

        public EmployesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<List<Employe>>> GetAllAsync(PagedResultRequestDto<Employe> input)
        {
            IQueryable<Employe> employes = _context.Employes
                .Where(x => string.IsNullOrEmpty(input.SearchValue.Keyword)
                ? true : (x.Name.Contains(input.SearchValue.Keyword)));


            if (!(string.IsNullOrEmpty(input.SortColumn) && string.IsNullOrEmpty(input.SortColumnDirection)))
                employes = employes.OrderBy(string.Concat(input.SortColumn, " ", input.SortColumnDirection));

            return new PagedResultDto<List<Employe>>()
            {
                Data = await employes.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
                TotalCount = await employes.CountAsync()
            };
        }

        public async Task<Employe> GetByIdAsync(int id)
        {
            return await _context.Employes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OperationResult> CreateEditAsync(Employe input)
        {
            var result = new OperationResult();
            try
            {
                if (input.Id == 0)
                {
                    await _context.Employes.AddAsync(input);
                }
                else
                {
                    input.UpdatedOn = DateTime.Now;
                    _context.Employes.Update(input);
                    _context.Entry<Employe>(input).Property(x => x.CreatedOn).IsModified = false;
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message;
                var execptionType = message.Split("_")[message.Split("_").Length - 1].Split("'")[0];

                switch (execptionType.ToLower())
                {
					case "uniqueemail":
						result.Message = Messages.UniqueEmail;
						break;
					case "uniquephoneno":
						result.Message = Messages.UniquePhoneNo;
						break;
					default:
                        result.Message = Messages.Failed;
                        break;
                       
                }
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employe = await _context.Employes.SingleOrDefaultAsync(x => x.Id == id);
            if (employe != null)
            {
                employe.IsDeleted = true;
				employe.DeletedOn = DateTime.Now;
				employe.Email += $"_{Guid.NewGuid()}";
				employe.PhoneNumber += $"_{Guid.NewGuid()}";
                _context.Employes.Update(employe);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
