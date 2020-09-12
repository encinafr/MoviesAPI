using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamsPagination<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int cantRecordsPerPage)
        {
            double cant = await queryable.CountAsync();
            double cantPages = Math.Ceiling(cant / cantRecordsPerPage);
            httpContext.Response.Headers.Add("recordsPerPage", cantPages.ToString());
        }
    }
}
