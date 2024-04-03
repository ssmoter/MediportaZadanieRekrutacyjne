using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;
using MediportaZadanieRekrutacyjne.Services;

namespace MediportaZadanieRekrutacyjne.Data
{
    public class TagService(ITagRequest request, ITagCache tagCache) : ITagService
    {
        private readonly ITagRequest _request = request;
        private readonly ITagCache _tagCache = tagCache;

        public async Task<IResult> GetTags(ListOfEnums.SortType sort, ListOfEnums.OrderType order, bool refreshData, int page)
        {
            try
            {
                int pageSize = Helper.TagRequestValue.PageSize;
                int pageSizeMulti = Helper.TagRequestValue.PageSizeMulti;

                Tag[] tags = [];


                if (!refreshData)
                {
                    tags = await _tagCache.GetCache(sort, order, page);
                }

                if (refreshData || tags.Length == 0)
                {
                    var tagTasks = new Task<Tag[]>[(pageSizeMulti)];
                    tags = new Tag[pageSize * pageSizeMulti];
                    int n = 0;
                    for (int i = page; i < page + pageSizeMulti; i++)
                    {
                        tagTasks[n] = _request.GetTags(sort, order, i);
                        //tagTasks[n] = _request.GetTagsIfRequestIsOutOfLimit(sort, order, i);
                        n++;
                    }

                    Task.WaitAll(tagTasks);

                    for (int i = 0; i < tagTasks.Length; i++)
                    {
                        Array.Copy(tagTasks[i].Result, 0, tags, i * pageSize, tagTasks[i].Result.Length);
                    }
                    await _tagCache.SaveCache(tags);
                }

                return Results.Ok(tags);
            }
            catch (Exception ex)
            {
                await Sqlite.SaveLog(ex);
                return Results.BadRequest(ex.Message);
            }
        }



    }
}
