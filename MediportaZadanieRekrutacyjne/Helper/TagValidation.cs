namespace MediportaZadanieRekrutacyjne.Helper
{
    public class TagValidation
    {

        public static (ListOfEnums.OrderType value, string error) OrderValidation(string order)
        {
            ListOfEnums.OrderType orderType = ListOfEnums.OrderType.none;

            if (string.IsNullOrWhiteSpace(order))
            {
                return (orderType, "Empty value");
            }


            if (int.TryParse(order, out int orderInt))
                if (orderInt >= 0 && orderInt < 2)
                    orderType = (ListOfEnums.OrderType)orderInt;


            if (order == nameof(ListOfEnums.OrderType.asc))
                orderType = ListOfEnums.OrderType.asc;
            if (order == nameof(ListOfEnums.OrderType.desc))
                orderType = ListOfEnums.OrderType.desc;


            if (orderType == ListOfEnums.OrderType.none)
            {
                return (orderType, "Incorrect value");
            }
            return (orderType, "");
        }
        public static (ListOfEnums.SortType value, string error) SortValidation(string sort)
        {
            ListOfEnums.SortType sortType = ListOfEnums.SortType.none;

            if (string.IsNullOrWhiteSpace(sort))
            {
                return (sortType, "Empty value");
            }

            if (int.TryParse(sort, out int sortInt))
                if (sortInt >= 0 && sortInt < 2)
                    sortType = (ListOfEnums.SortType)sortInt;

            if (sort == "name")
                sortType = ListOfEnums.SortType.name;
            if (sort == "popular")
                sortType = ListOfEnums.SortType.popular;

            if (sortType == ListOfEnums.SortType.none)
            {
                return (sortType, "Incorrect value");
            }
            return (sortType, "");
        }



    }
}
