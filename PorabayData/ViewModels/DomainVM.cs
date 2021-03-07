using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PorabayData.ViewModels
{
    public class DomainVM
    {
        [Key]
        public int DomianID { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
    }

}
