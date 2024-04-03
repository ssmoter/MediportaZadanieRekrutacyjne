using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;

using SQLite;

namespace MediportaZadanieRekrutacyjne.Data
{
    public class Sqlite
    {
        public static SQLiteAsyncConnection DBAsync =>
        new(SqlConstants.Path, SqlConstants.Flags);


        public static async Task CreateTablesAsync()
        {
            if (!await CheckTable(nameof(Errors)))
            {
                await DBAsync.CreateTableAsync<Errors>();
            }
            if (!await CheckTable(nameof(TagEntities)))
            {
                await DBAsync.CreateTableAsync<TagEntities>();
            }
        }
        public static async Task<bool> CheckTable(string name)
        {
            var tableInfo = await DBAsync.GetTableInfoAsync(name);
            bool exist = tableInfo.Count > 0;
            return exist;
        }
        public static async Task SaveLog(Exception exception)
        {
            Errors ex = new(exception);
            Console.WriteLine(ex.ToString());
            await DBAsync.ExecuteAsync($"INSERT INTO {nameof(Errors)} ({nameof(Errors.Message)},{nameof(Errors.StackTrace)}) VALUES (?,?)",
              ex.Message, ex.StackTrace);
            //await DBAsync.InsertAsync(ex);
        }


        public static void CreateTables()
        {
            var db = new SQLiteConnection(SqlConstants.Path, SqlConstants.Flags);
            try
            {
                if (!CheckTable(nameof(Errors), db))
                {
                    db.Execute($"CREATE TABLE IF NOT EXISTS {nameof(Errors)} (" +
                        $"{nameof(Errors.Id)} INTEGER PRIMARY KEY AUTOINCREMENT," +
                        $" {nameof(Errors.Message)} TEXT," +
                        $" {nameof(Errors.StackTrace)} TEXT);");
                    // db.CreateTable<Errors>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (!CheckTable(nameof(TagEntities), db))
                {
                    db.Execute($"CREATE TABLE IF NOT EXISTS {nameof(TagEntities)} (" +
                       $"{nameof(TagEntities.Id)} TEXT PRIMARY KEY," +
                       $"{nameof(TagEntities.CollectivesJson)} TEXT," +
                       $"{nameof(TagEntities.SynonymsJson)} TEXT," +
                       $"{nameof(TagEntities.Count)} INTEGER," +
                       $"{nameof(TagEntities.Has_Synonyms)} INTEGER," +
                       $"{nameof(TagEntities.Is_Moderator_Only)} INTEGER," +
                       $"{nameof(TagEntities.Is_Required)} INTEGER," +
                       $"{nameof(TagEntities.Last_Activity_Date)} TEXT," +
                       $"{nameof(TagEntities.Name)} TEXT," +
                       $"{nameof(TagEntities.User_Id)} INTEGER);");
                    // db.CreateTable<TagEntities>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static bool CheckTable(string name, SQLiteConnection db)
        {
            var tableInfo = db.GetTableInfo(name);
            bool exist = tableInfo.Count > 0;
            return exist;
        }

    }
}
