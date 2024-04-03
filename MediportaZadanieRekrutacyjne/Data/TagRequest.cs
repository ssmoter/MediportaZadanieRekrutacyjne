using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;
using MediportaZadanieRekrutacyjne.Services;

using RestSharp;

using System.Text.Json;

namespace MediportaZadanieRekrutacyjne.Data
{
    public class TagRequest(RestClientOptions restClientOptions) : ITagRequest
    {
        private readonly RestClient _restClient = new(restClientOptions);

        public async Task<Tag[]> GetTags(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            string uri = $"tags?page={page}&pagesize={TagRequestValue.PageSize}&order={order}&sort={sort}&site=stackoverflow";

            var request = new RestRequest(uri);

            var response = _restClient.ExecuteAsync(request);

            try
            {
                await response;

                if (response.Result.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(response.Result.Content))
                    {
                        var tagResponse = JsonSerializer.Deserialize<TagResponse>(
                                response.Result.Content, TagResponseSerializerContext.Default.TagResponse);

                        if (tagResponse is not null) { return tagResponse.Items; }
                    }
                }

                throw new Exception(response.Result.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Tag[]> GetTagsIfRequestIsOutOfLimit(ListOfEnums.SortType sort, ListOfEnums.OrderType order, int page)
        {
            var tag = new Tag[TagRequestValue.PageSize];

            for (int i = 0; i < tag.Length; i++)
            {
                tag[i] = new Tag()
                {
                    Name = Path.GetRandomFileName(),
                    Count = i,
                };
            }
            await Task.Delay(10);
            return tag;
        }

    }
}
