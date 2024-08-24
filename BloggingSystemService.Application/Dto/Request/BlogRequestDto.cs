using BloggingSystemService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Dto.Request
{
    public class BlogRequestDto
    {

        public string Name { get; set; }
        public string Url { get; set; }

        public int AuthorId { get; set; }


    }
}
