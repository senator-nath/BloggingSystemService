using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Dto.Response
{
    public class PaginatedBlogResponseDto
    {
        public string Message { get; set; }
        public IEnumerable<BlogResponseDto> ResponseDetails { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool IsSuccess { get; set; }
        public string AuthorName { get; set; }
    }
}
