using System;
using System.Collections.Generic;
using System.Text;

namespace PorabayData.ViewModels
{
    public class FilterVM
    {
        public int? Length { get; set; }
        public int? Draw { get; set; }
        public int? Start { get; set; }
        public List<Columns> Columns { get; set; }
        public List<Order> Order { get; set; }
        public Search search { get; set; }
    }

    public class Columns
    {
        public string? Data { get; set; }
        public string? Name { get; set; }
        public bool? Orderable { get; set; }
        public Search? Search { get; set; }
        public bool? Searchable { get; set; }
    }

    public class ColumnsFilter
    {
        public string? Data { get; set; }
        public Search? Search { get; set; }
    }



    public class Order
    {
        public int? Column { get; set; }
        public string? Dir { get; set; }
    }

    public class Search
    {
        public bool? Regex { get; set; }
        public string? Value { get; set; }
    }
}
