using System;
using System.ComponentModel.DataAnnotations;

namespace PorabayData.ViewModels
{
    public class ApprovalVM
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LeaveDate { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public string Color { get; set; }
    }
}
