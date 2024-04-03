using SQLite;

namespace MediportaZadanieRekrutacyjne.Models
{
    public class Errors
    {
        private int id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get => id; set => id = value; }
        private string? message;
        public string? Message { get => message; set => message = value; }
        private string? stackTrace;
        public string? StackTrace { get => stackTrace; set => stackTrace = value; }


        public Errors(Exception ex)
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;
        }
        public Errors()
        { }

        public override string ToString()
        {
            return $"Error:\n {Message} \n{StackTrace}";
        }

    }
}
