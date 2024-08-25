using BloggingSystemService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Dto.Response
{
    public class BlogResponseDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        // public IEnumerable<PostResponseDto> Posts { get; set; } = new List<PostResponseDto>();

        //public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
