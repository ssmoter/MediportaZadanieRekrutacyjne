namespace MediportaZadanieRekrutacyjne.Helper
{
    public class SqlConstants
    {
        public const SQLite.SQLiteOpenFlags Flags =
                     SQLite.SQLiteOpenFlags.ReadWrite |
                     SQLite.SQLiteOpenFlags.Create |
                     SQLite.SQLiteOpenFlags.SharedCache;

        public static string Name => $"{AppDomain.CurrentDomain.FriendlyName}.db3";
        public static string Path => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Name);
    }
}
