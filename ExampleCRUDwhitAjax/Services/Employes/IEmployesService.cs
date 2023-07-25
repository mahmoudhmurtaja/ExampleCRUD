using ExampleCRUDwhitAjax.Models;

namespace ExampleCRUDwhitAjax.Services.Employes
{
    public interface IEmployesService
    {
        Task<PagedResultDto<List<Employe>>> GetAllAsync(PagedResultRequestDto<Employe> input);
        Task<Employe> GetByIdAsync(int id);
        Task<OperationResult> CreateEditAsync(Employe input);
        Task<bool> DeleteAsync(int id);
    }
}
