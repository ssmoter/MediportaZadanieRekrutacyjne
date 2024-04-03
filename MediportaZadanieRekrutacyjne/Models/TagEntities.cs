using System.Text.Json;

namespace MediportaZadanieRekrutacyjne.Models
{
    public class TagEntities : Tag
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }
        public string? CollectivesJson { get; set; }
        public string? SynonymsJson { get; set; }

        public TagEntities(Tag tag)
        {
            Id = Guid.NewGuid();
            if (tag is null)
                return;

            if (tag.Collectives is not null)
                CollectivesJson = JsonSerializer.Serialize(tag.Collectives, CollectivesSerializerContext.Default.CollectivesArray);
            if (tag.Synonyms is not null)
                SynonymsJson = JsonSerializer.Serialize(tag.Synonyms, TagResponseSerializerContext.Default.StringArray);

            Count = tag.Count;
            Has_Synonyms = tag.Has_Synonyms;
            Is_Moderator_Only = tag.Is_Moderator_Only;
            Is_Required = tag.Is_Required;
            Last_Activity_Date = tag.Last_Activity_Date;
            Name = tag.Name;
            User_Id = tag.User_Id;
        }
        public TagEntities()
        { }
    }
}
