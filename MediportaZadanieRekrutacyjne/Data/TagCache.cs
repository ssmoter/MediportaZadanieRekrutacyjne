using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;
using MediportaZadanieRekrutacyjne.Services;

namespace MediportaZadanieRekrutacyjne.Data
{
    public class TagCache : ITagCache
    {

        public async Task SaveCache(Tag[] tags)
        {
            try
            {
                var maxCount = await Sqlite.DBAsync.Table<TagEntities>().CountAsync() + tags.Length;
                var tagEntities = new TagEntities[tags.Length];
                var tasks = new Task[tags.Length];
                for (int i = 0; i < tags.Length; i++)
                {
                    if (tags[i].Count != 0)
                    {
                        tags[i].CountPercent = ((double)tags[i].Count / maxCount) * 100;
                    }

                    tagEntities[i] = new TagEntities(tags[i]);
                    tasks[i] = Sqlite.DBAsync.ExecuteAsync(SqlQuery.SaveTags(tagEntities[i]));
                }

                if (tasks.Length > 0)
                {
                    await Task.WhenAll(tasks);
                }
                //await Sqlite.DBAsync.InsertAllAsync(tagEntities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tag[]> GetCache(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            try
            {
                var tagEntities = await Sqlite.DBAsync.QueryAsync<TagEntities>(SqlQuery.GetTagsQuery(sort, order, page));
                var maxCount = await Sqlite.DBAsync.Table<TagEntities>().CountAsync() + tagEntities.Count;

                var tag = new Tag[tagEntities.Count];

                for (int i = 0; i < tagEntities.Count; i++)
                {
                    tag[i] = new Tag(tagEntities[i]);
                    tag[i].CountPercent = ((double)tag[i].Count / maxCount) * 100;
                }
                return tag;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
