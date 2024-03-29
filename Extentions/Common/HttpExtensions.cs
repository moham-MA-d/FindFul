﻿using DTO.Pagination;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Extensions.Common
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int pageIndex, int pageSize, int totalItems, int totalPages)
        {
            var paginationHeader = new PagiantionHeader(pageIndex - 1, pageSize, totalItems, totalPages);
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, option));
            
            // We Have to add this, because we already added a custom header : Pagination
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
