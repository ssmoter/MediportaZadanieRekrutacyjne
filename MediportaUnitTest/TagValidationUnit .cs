using FluentAssertions;

using MediportaZadanieRekrutacyjne.Helper;

namespace MediportaUnitTest
{
    public class TagValidationUnit
    {
        public static IEnumerable<object[]> OrderValue =>
            [
                ["0",(ListOfEnums.OrderType.desc,"")],
                ["1",(ListOfEnums.OrderType.asc,"")],
                ["desc",(ListOfEnums.OrderType.desc,"")],
                ["asc",(ListOfEnums.OrderType.asc,"")],
                ["a",(ListOfEnums.OrderType.none, "Incorrect value")],
                ["",(ListOfEnums.OrderType.none, "Empty value")],
            ];


        [Theory]
        [MemberData(nameof(OrderValue))]
        public void TestCorrectValidationInOrderType(string request,
            (ListOfEnums.OrderType value, string error) result)
        {
            var obj = MediportaZadanieRekrutacyjne.Helper.TagValidation.OrderValidation(request);

            obj.Should().Be(result);
        }


        public static IEnumerable<object[]> SortValue =>
            [
                ["0",(ListOfEnums.SortType.name,"")],
                ["1",(ListOfEnums.SortType.popular,"")],
                ["name", (ListOfEnums.SortType.name,"")],
                ["popular", (ListOfEnums.SortType.popular, "")],
                ["a",(ListOfEnums.SortType.none, "Incorrect value")],
                ["",(ListOfEnums.SortType.none, "Empty value")],
            ];

        [Theory]
        [MemberData(nameof(SortValue))]
        public void TestCorrectValidationInSortType(string request,
                 (ListOfEnums.SortType value, string error) result)
        {
            var obj = MediportaZadanieRekrutacyjne.Helper.TagValidation.SortValidation(request);

            obj.Should().Be(result);
        }

    }
}
