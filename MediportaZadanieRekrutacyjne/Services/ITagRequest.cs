using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;

namespace MediportaZadanieRekrutacyjne.Services
{
    public interface ITagRequest
    {
        Task<Tag[]> GetTags(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page);
        Task<Tag[]> GetTagsIfRequestIsOutOfLimit(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page);
    }
}