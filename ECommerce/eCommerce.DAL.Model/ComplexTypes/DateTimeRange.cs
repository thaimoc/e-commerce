using System;

namespace eCommerce.DAL.Model.ComplexTypes
{
    public class DateTimeRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsDirty { get; set; }
    }
}