using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorabayData.ViewModels
{
    public class ApprovalVM
    {
        [Key]
        public int Id { get; set; }
        public DateTime LeaveDate { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public int Status { get; set; }
    }
}
