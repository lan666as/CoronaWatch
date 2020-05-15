namespace CoronaWatchDB
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReportDBs")]
    public class ReportDB
    {
        public int Id { get; set; }

        [StringLength(4)]
        public string ISOCode { get; set; }

        public int? Confirmed { get; set; }

        public int? Recovered { get; set; }

        public int? Death { get; set; }

        public int? Active { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual RegionDB RegionDB { get; set; }
    }
}
