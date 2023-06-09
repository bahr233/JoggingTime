﻿using JoggingTime.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingTime.Models
{
    public class User:BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
