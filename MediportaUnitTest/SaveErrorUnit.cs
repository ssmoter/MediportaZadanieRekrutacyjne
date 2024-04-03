using FluentAssertions;

using MediportaZadanieRekrutacyjne.Data;
using MediportaZadanieRekrutacyjne.Models;

namespace MediportaUnitTest
{
    public class SaveErrorUnit
    {
        public SaveErrorUnit()
        {
            Sqlite.CreateTablesAsync().Wait();
        }

        [Theory]
        [InlineData("Błąd")]
        public async void SaveError(string request)
        {
            await Sqlite.SaveLog(new Exception(request));
            var obj = await Sqlite.DBAsync.Table<Errors>().FirstOrDefaultAsync(x => x.Message == request);

            obj.Message.Should().Be(request);
        }

    }
}
