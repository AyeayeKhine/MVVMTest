﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMTest.Models
{
   public class User
    {
        [PrimaryKey]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
    }
}
