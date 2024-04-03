using SQLite;

using System.Text.Json;

using System.Text.Json.Serialization;

namespace MediportaZadanieRekrutacyjne.Models
{
    public class TagResponse
    {
        [JsonPropertyName("items")]
        public Tag[] Items { get; set; }
        [JsonPropertyName("has_more")]
        public bool Has_more { get; set; }
        [JsonPropertyName("quota_max")]
        public int Quota_max { get; set; }
        [JsonPropertyName("quota_remaining")]
        public int Quota_remaining { get; set; }
        public TagResponse()
        {
            Items ??= [];
        }

        [JsonConstructor]
        public TagResponse(Tag[] items, bool has_more, int quota_max, int quota_remaining)
        {
            Items = items;
            Has_more = has_more;
            Quota_max = quota_max;
            Quota_remaining = quota_remaining;
        }
    }

    public class Tag
    {
        [SQLite.Ignore]
        [JsonPropertyName("collectives")]
        public Collectives[]? Collectives { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [Ignore]
        public double CountPercent { get; set; }
        [JsonPropertyName("has_Synonyms")]
        public bool Has_Synonyms { get; set; }
        [JsonPropertyName("is_Moderator_Only")]
        public bool Is_Moderator_Only { get; set; }
        [JsonPropertyName("is_Required")]
        public bool Is_Required { get; set; }
        [JsonPropertyName("last_Activity_Date")]
        public DateTime? Last_Activity_Date { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [SQLite.Ignore]
        [JsonPropertyName("synonyms")]
        public string[]? Synonyms { get; set; }
        [JsonPropertyName("user_Id")]
        public int User_Id { get; set; }
        public Tag(TagEntities tag)
        {
            if (tag is null)
                return;

            if (!string.IsNullOrEmpty(tag.CollectivesJson))
                Collectives = JsonSerializer.Deserialize<Collectives[]>(tag.CollectivesJson, CollectivesSerializerContext.Default.CollectivesArray);
            if (!string.IsNullOrEmpty(tag.SynonymsJson))
                Synonyms = JsonSerializer.Deserialize<string[]>(tag.SynonymsJson, TagResponseSerializerContext.Default.StringArray);

            Count = tag.Count;
            Has_Synonyms = tag.Has_Synonyms;
            Is_Moderator_Only = tag.Is_Moderator_Only;
            Is_Required = tag.Is_Required;
            Last_Activity_Date = tag.Last_Activity_Date;
            Name = tag.Name;
            User_Id = tag.User_Id;
        }
        public Tag()
        { }

    }
    public class Collectives
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("external_Links")]
        public External_Links[]? External_Links { get; set; }
        [JsonPropertyName("link")]
        public string? Link { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
        [JsonPropertyName("tags")]
        public string[]? Tags { get; set; }


    }
    public class External_Links
    {
        [JsonPropertyName("link")]
        public string? Link { get; set; }
        [JsonPropertyName("type")]
        public Type? Type { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter<Type>))]
    public enum Type
    {
        website,
        twitter,
        github,
        facebook,
        instagram,
        support,
        linkedin
    }

    [JsonSerializable(typeof(TagResponse))]
    internal partial class TagResponseSerializerContext : JsonSerializerContext
    { }
    [JsonSerializable(typeof(Tag[]))]
    internal partial class TagSerializerContext : JsonSerializerContext
    { }
    [JsonSerializable(typeof(Collectives[]))]
    internal partial class CollectivesSerializerContext : JsonSerializerContext
    { }
}
