using System.ComponentModel.DataAnnotations;

namespace PorabayData.Models
{
    public class Domain
    {
        [Key]
        public int DomianID { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
    }
}
