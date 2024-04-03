using MediportaZadanieRekrutacyjne.Models;

using System.Text;

namespace MediportaZadanieRekrutacyjne.Helper
{
    public class SqlQuery
    {
        private readonly static StringBuilder sb = new();
        public static string GetTagsQuery(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            sb.Clear();
            sb.Append("SELECT * FROM TagEntities ");
            if (sort != ListOfEnums.SortType.none && order != ListOfEnums.OrderType.none)
            {
                if (sort == ListOfEnums.SortType.name)
                    sb.Append("ORDER BY Name ");
                if (sort == ListOfEnums.SortType.popular)
                    sb.Append("ORDER BY Count ");
                sb.Append(order);
                sb.Append(' ');
            }
            sb.Append("LIMIT ");
            sb.Append(TagRequestValue.PageSize * TagRequestValue.PageSizeMulti);
            sb.Append(' ');
            sb.Append("OFFSET ");
            if (page == 1)
                sb.Append(0);
            else
                sb.Append(page * TagRequestValue.PageSize * TagRequestValue.PageSizeMulti);

            return sb.ToString();
        }
        public static string SaveTags(TagEntities tag)
        {
            sb.Clear();
            sb.Append("INSERT INTO ");
            sb.Append(nameof(TagEntities));
            sb.Append('(');
            sb.Append(nameof(TagEntities.Id));

            if (!string.IsNullOrEmpty(tag.CollectivesJson))
            {
                sb.Append(',');
                sb.Append(nameof(TagEntities.CollectivesJson));
            }


            if (!string.IsNullOrEmpty(tag.SynonymsJson))
            {
                sb.Append(',');
                sb.Append(nameof(TagEntities.SynonymsJson));
            }
            sb.Append(',');
            sb.Append(nameof(TagEntities.Count));
            sb.Append(',');
            sb.Append(nameof(TagEntities.Has_Synonyms));
            sb.Append(',');
            sb.Append(nameof(TagEntities.Is_Moderator_Only));
            sb.Append(',');
            sb.Append(nameof(TagEntities.Is_Required));
            if (tag.Last_Activity_Date.HasValue)
            {
                sb.Append(',');
                sb.Append(nameof(TagEntities.Last_Activity_Date));
            }
            sb.Append(',');
            sb.Append(nameof(TagEntities.Name));
            sb.Append(',');
            sb.Append(nameof(TagEntities.User_Id));




            sb.Append(") VALUES ('");
            sb.Append(tag.Id);
            sb.Append("',");
            if (!string.IsNullOrEmpty(tag.CollectivesJson))
            {
                sb.Append('\'');
                sb.Append(tag.CollectivesJson);
                sb.Append("',");
            }
            if (!string.IsNullOrEmpty(tag.SynonymsJson))
            {
                sb.Append('\'');
                sb.Append(tag.SynonymsJson);
                sb.Append("',");
            }
            sb.Append(tag.Count);
            sb.Append(",");
            sb.Append(tag.Has_Synonyms);
            sb.Append(",");
            sb.Append(tag.Is_Moderator_Only);
            sb.Append(",");
            sb.Append(tag.Is_Required);
            if (tag.Last_Activity_Date.HasValue)
            {
                sb.Append(",'");
                sb.Append(tag.Last_Activity_Date);
                sb.Append("','");
            }
            else
                sb.Append(',');
            sb.Append('\'');
            sb.Append(tag.Name);
            sb.Append("',");
            sb.Append(tag.User_Id);
            sb.Append(");");

            return sb.ToString();
        }
    }
}
