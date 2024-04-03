using MediportaZadanieRekrutacyjne.Helper;

namespace MediportaZadanieRekrutacyjne.Services
{
    public interface ITagService
    {
        Task<IResult> GetTags(ListOfEnums.SortType sort, ListOfEnums.OrderType order, bool refreshData, int page);
    }
}
