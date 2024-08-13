using BloggingSystemService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Dto.Response
{
    public class AuthorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
