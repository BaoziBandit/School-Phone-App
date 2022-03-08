using Plugin.LocalNotifications;
using System;
using Xamarin.Forms;

namespace WGU_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DataManager.BuildAsyncData();

            // CreateExampleData();
            // CrossLocalNotifications.Current.Show("title", "body", 101);
            // CrossLocalNotifications.Current.Show("title2", "body2", 102);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region Unused
        //void CreateExampleData()
        //{
        //    Term[] t = new Term[] {
        //        new Term
        //        {
        //            ID = 1,
        //            Name = "Term 1",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Term
        //        {
        //            ID = 2,
        //            Name = "Term 2",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Term
        //        {
        //            ID = 3,
        //            Name = "Term 3",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        }
        //    };

        //    Course[] c = new Course[] {

        //        new Course
        //        {
        //            ID = 1,
        //            Name = "Course 1",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Course
        //        {
        //            ID = 2,
        //            Name = "Course 2",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Course
        //        {
        //            ID = 3,
        //            Name = "Course 3",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Course
        //        {
        //            ID = 4,
        //            Name = "Course 4",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Course
        //        {
        //            ID = 5,
        //            Name = "Course 5",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        },
        //        new Course
        //        {
        //            ID = 6,
        //            Name = "Course 6",
        //            Start = DateTime.Now,
        //            End = DateTime.Now.AddDays(5),
        //        }
        //    };


        //    for (int i = 0; i < t.Length; i++)
        //    {
        //        bool termExists = false;

        //        foreach (Term term in DataManager.terms)
        //        {
        //            if (t[i].Name == term.Name)
        //                termExists = true;
        //        }
        //        if (!termExists)
        //            DataManager.terms.Add(t[i]);

        //        for (int j = 0; j < c.Length; j++)
        //        {
        //            bool courseExsists = false;

        //            foreach (Course course in DataManager.terms[i].courses)
        //            {
        //                if (c[j].Name == course.Name)
        //                    courseExsists = true;

        //                if (!courseExsists)
        //                {
        //                    DataManager.terms[i].courses.Add(course);
        //                    //c[j].Objective = new Assessment(0,"Objective", "Objective", DateTime.Now, false);
        //                    //c[j].Performance = new Assessment(0, "Performance", "Performance", DateTime.Now, false);
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
