namespace CoronaWatchDB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RegionDBs")]
    public class RegionDB
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        [Key]
        [StringLength(4)]
        public string ISOCode { get; set; }
    }
}
