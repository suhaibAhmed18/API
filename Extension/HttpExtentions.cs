using Azure;
using ReStore.Entities;
using ReStoreAPI.RequestHelpers;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ReStoreAPI.Extension
{
    public static class HttpExtentions
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData,options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

    }
}