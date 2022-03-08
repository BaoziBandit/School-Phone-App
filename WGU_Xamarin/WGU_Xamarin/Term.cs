using System;
using SQLite;

namespace WGU_Xamarin
{
    [Table("terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int ID { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("start")]
        public DateTime Start { get; set; }
        
        [Column("end")]
        public DateTime End { get; set; }
        
        [Column("startNotify")]
        public bool StartNotify { get; set; }

        [Column("endNotify")]
        public bool EndNotify { get; set; }

        public Term() {}
        public Term(bool startNotify, bool endNotify = false)
        {
            Name = "New Term"; Start = DateTime.Now; End = DateTime.Now.AddDays(60); StartNotify = startNotify; EndNotify = endNotify;
        }
        public Term(string name, DateTime start, DateTime end, bool startNotify, bool endNotify)
        {
            Name = name; Start = start; End = end; StartNotify = startNotify; EndNotify = endNotify;
        }
    }
}
