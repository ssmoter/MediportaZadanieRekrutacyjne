using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;

namespace MediportaZadanieRekrutacyjne.Services
{
    public interface ITagCache
    {
        Task<Tag[]> GetCache(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page);
        Task SaveCache(Tag[] tags);
    }
}