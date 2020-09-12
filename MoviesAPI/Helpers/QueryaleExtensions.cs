﻿using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public static class QueryaleExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            return queryable.
                Skip((pagination.Page - 1) * pagination.RecordsPerPage).
                Take(pagination.RecordsPerPage);

        }
    }
}
