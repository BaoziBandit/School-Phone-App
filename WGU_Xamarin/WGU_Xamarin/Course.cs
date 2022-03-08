using System;
using SQLite;


namespace WGU_Xamarin
{
    [Table("courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int ID{ get; set; }
        
        [Column("name")]        public string Name { get; set; }
        [Column("status")]      public Statuses Status { get; set; }
        [Column("start")]       public DateTime Start { get; set; }
        [Column("end")]         public DateTime End { get; set; }
        [Column("startNotify")] public bool StartNotify { get; set; }
        [Column("endNotify")]   public bool EndNotify { get; set; }
        [Column("description")] public string Description { get; set; }
        [Column("notes")]       public string Notes { get; set; }

        [Column("perfAssName")] public string PerfAssName { get; set; }
        [Column("perfDueDate")] public DateTime PerfDueDate { get; set; }
        [Column("perfNotify")]  public bool PerfNotify { get; set; }
        [Column("perfSelected")]  public bool PerfSelected { get; set; }
        [Column("objAssName")]  public string ObjAssName { get; set; }
        [Column("objDueDate")]  public DateTime ObjDueDate { get; set; }
        [Column("objNotify")]   public bool ObjNotify { get; set; }
        [Column("objSelected")]   public bool ObjSelected { get; set; }

        [Column("instructorName")]   public string InstructorName { get; set; }
        [Column("instructorPhone")]   public string InstructorPhone { get; set; }
        [Column("instructorEmail")]   public string InstructorEmail { get; set; }
        [Indexed, Column("termId")]         public int TermID{ get; set; }
        
        
        public Course() {

            PerfDueDate = DateTime.Now;
            ObjDueDate = DateTime.Now.AddDays(15);
        }
        public Course(string name, Statuses status, DateTime start, DateTime end, bool startNotify, bool endNotify, string description, string notes)
        { 
            Name = name;
            Status = status;
            Start = start;
            End = end;
            StartNotify = startNotify;
            EndNotify = endNotify;
            Description = description;
            Notes = notes;
        }
        public Course(string name = "New Course", bool startNotify = false, bool endNotify = false, string description = "Description...", string notes = "Notes...")
        {
            Name = name;
            Status = Statuses.Active;
            Start = DateTime.Now;
            End = DateTime.Now.AddDays(15);
            StartNotify = startNotify;
            EndNotify = endNotify;
            Description = description;
            Notes = notes;
        }

        public enum Statuses
        {
            Planned,
            Active,
            Complete,
            Dropped
        }
    }
}
