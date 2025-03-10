using Models;
using Services.Pagging;

namespace Services.Product
{
    public interface IBlindBoxService
    {
        Task<IEnumerable<BlindBox>> GetAllAsync();
        Task<BlindBox> GetByIdAsync(int id);
        Task<BlindBox> AddAsync(BlindBox blindBox);
        Task<BlindBox> UpdateAsync(BlindBox blindBox);
        Task DeleteAsync(int id);

        Task<PaginatedList<BlindBox>> GetAll(int pageNumber, int pageSize);
    }
}
