using FluentAssertions;

using MediportaZadanieRekrutacyjne.Data;
using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;
using MediportaZadanieRekrutacyjne.Services;

namespace MediportaUnitTest
{
    public class TagCacheUnit
    {
        public static ITagCache _tagCache;
        public TagCacheUnit()
        {
            _tagCache = new MediportaZadanieRekrutacyjne.Data.TagCache();
            MediportaZadanieRekrutacyjne.Data.Sqlite.CreateTablesAsync().Wait();
        }


        public static IEnumerable<object[]> ParametersSave =>
            [
             [new Tag[] {new(),new() }],
             [new Tag[] {new(),new(),new(),new() }],
            ];

        [Theory]
        [MemberData(nameof(ParametersSave))]
        public async void SaveCache(Tag[] tags)
        {
            var tagsCoutBefore = await MediportaZadanieRekrutacyjne.Data.Sqlite.DBAsync.Table<TagEntities>().CountAsync();

            await _tagCache.SaveCache(tags);
            var tagsCoutAfter = await MediportaZadanieRekrutacyjne.Data.Sqlite.DBAsync.Table<TagEntities>().CountAsync();

            tagsCoutAfter.Should().BeGreaterThan(tagsCoutBefore);
        }




        public static IEnumerable<object[]> ParametersGet =>
            [
             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc,1)],
             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.desc,1)],
             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.asc,1)],
             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.asc,1)],
            ];


        [Theory]
        [MemberData(nameof(ParametersGet))]
        public async void GetCache(
            (ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page) request)
        {
            var obj = await _tagCache.GetCache(request.sort, request.order, request.page);

            obj.Should().NotBeNull();
        }

        public static IEnumerable<object[]> ParametersGetQuery =>
            [
             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc,1)
                ,"SELECT * FROM TagEntities " +
                "ORDER BY Name desc " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.desc,1)
                ,"SELECT * FROM TagEntities " +
                "ORDER BY Count desc " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.asc,1)
                ,"SELECT * FROM TagEntities " +
                "ORDER BY Name asc " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.asc,1)
                ,"SELECT * FROM TagEntities " +
                "ORDER BY Count asc " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

             [(ListOfEnums.SortType.none, ListOfEnums.OrderType.desc,1)
                ,"SELECT * FROM TagEntities " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.none,1)
                ,"SELECT * FROM TagEntities " +
                $"LIMIT {TagRequestValue.PageSize * TagRequestValue.PageSizeMulti} " +
                $"OFFSET {1 * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti}"],

            ];

        [Theory]
        [MemberData(nameof(ParametersGetQuery))]
        public void ValidQueryTooDB((ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page) request, string result)
        {
            var obj = SqlQuery.GetTagsQuery(request.sort, request.order, request.page);

            obj.Should().Be(result);
        }

    }
}
