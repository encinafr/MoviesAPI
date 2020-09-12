using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        private int recordsPerPage = 10;
        private readonly int cantMaxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get => cantMaxRecordsPerPage;
            set
            {
                recordsPerPage = (value > cantMaxRecordsPerPage) ? cantMaxRecordsPerPage : value;
            }
        }
    }
}
