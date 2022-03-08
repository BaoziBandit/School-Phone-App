using System;
using SQLite;

namespace WGU_Xamarin
{
    [Table("assessments")]
    public class Assessment
    {
        [Column("id"), PrimaryKey, AutoIncrement, Unique, NotNull]      
        public int? ID { get; set; }
        [Column("name"), NotNull]    
        public string Name { get; set; }
        [Column("type"), NotNull]   
        public string Type { get; set; }
        [Column("dueDate"), NotNull] 
        public DateTime DueDate { get; set; }
        [Column("notify"), NotNull]  
        public bool Notify { get; set; }
        [Indexed]
        [Column("courseId")]
        public int CourseID { get; set; }

        public Assessment() { }
        public Assessment(string name, string type, bool notify = false)
        {
            Name = name;
            Type = type;
            DueDate = DateTime.Now.AddDays(5);
            Notify = notify;
        }
        public Assessment(int id, string name, string type, DateTime dueDate, bool notify = false)
        {
            ID = id;
            Name = name;
            Type = type;
            DueDate = dueDate;
            Notify = notify;
        }
    }
}
