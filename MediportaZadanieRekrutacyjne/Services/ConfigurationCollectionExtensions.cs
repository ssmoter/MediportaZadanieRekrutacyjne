namespace MediportaZadanieRekrutacyjne.Services
{
    public static class ConfigurationCollectionExtensions
    {
        public static void AddMyConfiguration(this IConfiguration configuration)
        {
            if (int.TryParse(configuration.GetSection("StackOverflowAPIPageSize").Value, out int pageSize))
                Helper.TagRequestValue.PageSize = pageSize;
            if (int.TryParse(configuration.GetSection("StackOverflowAPIPageSizeMulti").Value, out int pageSizeMulti))
                Helper.TagRequestValue.PageSizeMulti = pageSizeMulti;



        }

    }
}
