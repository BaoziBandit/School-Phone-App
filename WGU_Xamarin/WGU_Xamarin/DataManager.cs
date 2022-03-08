using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WGU_Xamarin
{
    public static class DataManager
    {
        // TODO Need to configure DataManager

        private static string dbFile = "master.db";
        private static string dbPath = FileSystem.AppDataDirectory;
        private static SQLiteAsyncConnection conn;

        public static async void BuildAsyncData()
        {
            bool buildData = IsNewDatabase();

            if(conn == null)
            {
                conn = new SQLiteAsyncConnection(Path.Combine(dbPath, dbFile));
                await CreateTables();
                if(buildData)
                {
                    AddInitialData();
                }
            }
        }

        private static bool IsNewDatabase()
        {
            return !File.Exists(Path.Combine(dbPath, dbFile));
        }
        private static async void AddInitialData()
        {
            Term initialTerm = new Term("Term 1", new DateTime(2022, 4, 5), new DateTime(2022, 10, 4), false, false);
            initialTerm.ID = await InsertTerm(initialTerm);
            
            Course course = new Course("Intro to IT", Course.Statuses.Planned, new DateTime(2022, 4, 5), new DateTime(2022, 10, 4), false, false, "Introduction to Information Technology", "Something worth sharing.");
            course.TermID = initialTerm.ID;
            course.PerfSelected = true; course.PerfDueDate = new DateTime(2022, 9, 21);
            course.PerfAssName = "Performance Assessment";
            course.ObjSelected = true; course.ObjDueDate = new DateTime(2022, 10, 3);
            course.ObjAssName = "Objective Assessment";
            course.InstructorName = "Corey Denning"; course.InstructorPhone = "123-456-7890"; course.InstructorEmail = "cdenn25@wgu.edu";
            _ = await InsertCourse(course);
        }


        private static async Task CreateTables()
        {
            await Task.WhenAll(
                conn.CreateTableAsync<Term>(),
                conn.CreateTableAsync<Course>(),
                conn.CreateTableAsync<Assessment>()
            );
        }
        
        public static async Task<List<Term>> GetTerms()
        {
            return await conn.Table<Term>().OrderBy(t => t.Start).ToListAsync();
        }
        public static async Task<int> InsertTerm(Term term)
        {
            return await conn.InsertAsync(term);
        }
        public static async Task UpdateTerm(Term term)
        {
            await conn.UpdateAsync(term);
        }
        public static async Task DeleteTerm(Term term)
        {
           await conn.DeleteAsync<Term>(term.ID);
        }

        public static async Task<List<Course>> GetCourses(Term term)
        {
            return await conn.Table<Course>().Where(c => c.TermID == term.ID).OrderBy(c => c.ID).ToListAsync();
        }
        public static async Task<int> InsertCourse(Course course)
        {
            return await conn.InsertAsync(course);
        }
        public static async Task UpdateCourse(Course course)
        {
            await conn.UpdateAsync(course);
        }
        public static async Task DeleteCourse(Course course)
        {
            await conn.DeleteAsync<Course>(course.ID);
        }
    }
}
