﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorabayData.ViewModels
{
    public class LoginVM
    {
        [Key]
        public int LoginID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
    }
}
