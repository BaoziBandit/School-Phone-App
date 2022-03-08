using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGU_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
       // public static List<Course> createdCourses = new List<Course>();
       // public static List<Course> updatedCourses = new List<Course>();
       // public static List<Course> deletedCourses = new List<Course>();
        public ObservableCollection<Course> dataCourses = new ObservableCollection<Course>();

        Term pageTerm;

        public TermPage()
        {
            InitializeComponent();
            pageTerm = new Term();

            termLabel.Text = "New Term";
            startDate.Date = DateTime.Now;
            endDate.Date = DateTime.Now.AddDays(15);
            startNotify.IsChecked = false;
            endNotify.IsChecked = false;
        }
        public TermPage(Term selectedTerm)
        {
            InitializeComponent();
            pageTerm = selectedTerm;

            termLabel.Text = pageTerm.Name;
            startDate.Date = pageTerm.Start;
            endDate.Date = pageTerm.End;
            startNotify.IsChecked = pageTerm.StartNotify;
            endNotify.IsChecked = pageTerm.EndNotify;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (pageTerm.ID != 0)
                await SetCourseList();
            CourseList.ItemsSource = dataCourses;
            UpdateDateRange();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if(await DisplayAlert("ALERT!", "Are you sure you want to leave?", "Yes", "No"))
                {
                    if (pageTerm.ID == 0 && dataCourses.Count > 0)
                        DeleteCreatedCourses();
                    await Navigation.PopModalAsync();
                }
            });
            return true;
        }
        private async void DeleteCreatedCourses()
        {
            foreach (Course course in dataCourses)
                await DataManager.DeleteCourse(course);
        }

        private async Task SetCourseList()
        {
            // TODO Figure out how to combine Created courses with Data Courses, and remove any deleted courses. 
            // Can't sort for some reason....
            
            dataCourses = new ObservableCollection<Course>(await DataManager.GetCourses(pageTerm));
           /* if(createdCourses.Count > 0)
                foreach (Course course in createdCourses)
                    dataCourses.Add(course);

            if (deletedCourses.Count > 0)
                foreach (Course course in deletedCourses)
                    dataCourses.Remove(course);

            if (updatedCourses.Count > 0)
                for (int i = 0; i < updatedCourses.Count; i++)
                    for (int j = 0; j < dataCourses.Count; j++)
                        if (updatedCourses[i].ID == dataCourses[j].ID)
                            dataCourses[j] = updatedCourses[i]; */
        }

        private void OnTermLabelTapped(object sender, EventArgs args)
        {
            ((Label)sender).IsVisible = false;
            termEntry.IsVisible = true;
            termEntry.Focus();
        }
        private void termEntry_Focused(object sender, FocusEventArgs e)
        {
            termEntry.CursorPosition = 0;
            termEntry.SelectionLength = termEntry.Text.Length;
        }
        private void OnTermEntryUnfocused(object sender, FocusEventArgs args)
        {
            ((Entry)sender).IsVisible = false;
            termLabel.IsVisible = true;
        }

        /*private async void CourseUpsert()
        {
            if (createdCourses.Count > 0)
                foreach (Course course in createdCourses)
                    if (course.ID == 0)
                     
                    else
                        await DataManager.UpdateCourse(course);
        }*/

        private async void OnSaveTermBtnClicked(object sender, EventArgs e)
        {
            //  CourseUpsert();
            if(!string.IsNullOrEmpty(termLabel.Text) || termLabel.Text == "New Term")
            {
                pageTerm.Name = termLabel.Text;
                pageTerm.Start = startDate.Date;
                pageTerm.End = endDate.Date;
                pageTerm.StartNotify = startNotify.IsChecked;
                pageTerm.EndNotify = endNotify.IsChecked;
                SetNotifications();

                if (pageTerm.ID == 0)
                {
                    int termID = await DataManager.InsertTerm(pageTerm);
                    for (int i = 0; i < dataCourses.Count; i++)
                    {
                        dataCourses[i].TermID = termID;
                        await DataManager.UpdateCourse(dataCourses[i]);
                    }
                }   
                else
                    await DataManager.UpdateTerm(pageTerm);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Invalid!", "Please assign a Course Name", "OK");
            }
            
        }
        private async void OnDeleteTermBtnClicked(object sender, EventArgs e)
        {
            if(await DisplayAlert("ALERT!","Are you sure you want to delete the term and all courses?", "Yes", "No"))
            {
                if (dataCourses.Count > 0)
                {
                    foreach (var course in dataCourses)
                        await DataManager.DeleteCourse(course);
                }
                await DataManager.DeleteTerm(pageTerm);
                await Navigation.PopModalAsync();
            }
        }

        private void SetNotifications()
        {
            if (startNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageTerm.Name} Starting",
                    $"{pageTerm.Name} starting NOW!",
                    pageTerm.ID + 10000,
                    startDate.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageTerm.ID + 10000);
            }
            if (endNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageTerm.Name} Ending",
                    $"{pageTerm.Name} ending NOW!",
                    pageTerm.ID + 20000,
                    endDate.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageTerm.ID + 20000);
            }
        }

        private async void OnAddCourseBtnClicked(object sender, EventArgs e)
        {
            if (dataCourses.Count < 6)
            {
                await Navigation.PushModalAsync(new CoursePage(pageTerm));
                await SetCourseList();
            }
            else
                await DisplayAlert("Too many courses!", "You may only have 6 courses per term.", "OK");
            
        }
        private async void CourseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Course course = (Course)e.Item;
                await Navigation.PushModalAsync(new CoursePage(course, pageTerm));

            await SetCourseList ();
        }

        private void UpdateDateRange()
        {
            startDate.MaximumDate = endDate.Date;
            endDate.MinimumDate = startDate.Date;
        }
        private void startDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateDateRange();
        }
        private void endDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateDateRange();
        }
    }
}