﻿using Netby.Todo.Site.API.Services.API.Tasks.Enums;

namespace Netby.Todo.Site.API.Services.API.Tasks.Models
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public StatusTask Status { get; set; }

    }
}
