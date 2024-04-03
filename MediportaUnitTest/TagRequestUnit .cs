using FluentAssertions;

using MediportaZadanieRekrutacyjne.Helper;

using Microsoft.Extensions.Configuration;

namespace MediportaUnitTest
{
    public class TagRequestUnit
    {
        MediportaZadanieRekrutacyjne.Services.ITagRequest _tagRequest;
        public TagRequestUnit()
        {
            var config = InitConfiguration();
            MediportaZadanieRekrutacyjne.Helper.TagRequestValue.PageSizeMulti = int.Parse(config["StackOverflowAPIPageSizeMulti"]);
            MediportaZadanieRekrutacyjne.Helper.TagRequestValue.PageSize = int.Parse(config["StackOverflowAPIPageSize"]);
            _tagRequest = new MediportaZadanieRekrutacyjne.Data.TagRequest
                (new RestSharp.RestClientOptions(config["StackOverflowAPI"]));
        }

        public static IEnumerable<object[]> Parameters =>
            [
             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.desc,1)],
             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.desc,1)],
             [(ListOfEnums.SortType.name, ListOfEnums.OrderType.asc,1)],
             [(ListOfEnums.SortType.popular, ListOfEnums.OrderType.desc,1)],
            ];

        [Theory]
        [MemberData(nameof(Parameters))]
        public async void GetRequest(
            (ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page) request)
        {
            var obj = await _tagRequest.GetTags(request.sort, request.order, request.page);

            obj.Should().NotBeNullOrEmpty();
        }





        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
