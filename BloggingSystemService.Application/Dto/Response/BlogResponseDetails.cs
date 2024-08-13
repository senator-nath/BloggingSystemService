﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Application.Dto.Response
{
    public class BlogResponseDetails
    {
        public string Message { get; set; }
        public BlogResponseDto ResponseDetails { get; set; }
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
