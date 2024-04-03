using FluentAssertions;

using MediportaZadanieRekrutacyjne.Data;
using MediportaZadanieRekrutacyjne.Helper;

namespace MediportaUnitTest
{
    public class TagServiceTestIntegration
    {
        public static MediportaZadanieRekrutacyjne.Services.ITagService _tagservice;

        public TagServiceTestIntegration()
        {
            var config = TagRequestUnit.InitConfiguration();

            MediportaZadanieRekrutacyjne.Helper.TagRequestValue.PageSizeMulti = int.Parse(config["StackOverflowAPIPageSizeMulti"]);
            MediportaZadanieRekrutacyjne.Helper.TagRequestValue.PageSize = int.Parse(config["StackOverflowAPIPageSize"]);

            _tagservice = new TagService(
                new TagRequest(new RestSharp.RestClientOptions(config["StackOverflowAPI"])),
                new TagCache());
            Sqlite.CreateTablesAsync().Wait();
        }


        [Theory]
        [InlineData(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc, 1)]
        public async void GetNewDataFromSOAPI(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            var obj = await _tagservice.GetTags(sort, order, true, page);

            obj.Should().NotBeNull();
        }

        [Theory]
        [InlineData(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc, 2)]
        public async void GetOldDataFromSOAPI(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            var obj = await _tagservice.GetTags(sort, order, false, page);

            obj.Should().NotBeNull();
        }
        [Theory]
        [InlineData(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc, 10)]
        public async void GetOldDataFromSOAPIPage10(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            var obj = await _tagservice.GetTags(sort, order, false, page);

            obj.Should().NotBeNull();
        }
    }
}
