using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.LocalNotifications;


namespace WGU_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public Course pageCourse = null;
        Term courseTerm;
        bool validCourse;

      // add case
        public CoursePage(Term term)
        {
            InitializeComponent();
            pageCourse = new Course();
            courseTerm = term;
            pageCourse.Name = "Course Name";
            pageCourse.Status = Course.Statuses.Planned;
            pageCourse.Start = DateTime.Now;
            pageCourse.End = DateTime.Now.AddDays(15);
        }

        //   this is the edit case. there were two so we removed one

        public CoursePage(Course course, Term term)
        {
            InitializeComponent();
            pageCourse = course;
            courseTerm = term;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            statusPicker.ItemsSource = new Course.Statuses[] { Course.Statuses.Planned, Course.Statuses.Active, Course.Statuses.Complete, Course.Statuses.Dropped };
            pageCourse.TermID = courseTerm.ID;
            courseLabel.Text = string.IsNullOrEmpty(pageCourse.Name) ? "Course Name" : pageCourse.Name;
            statusPicker.SelectedItem = pageCourse.Status;
            startPicker.Date = pageCourse.Start;
            startNotify.IsChecked = pageCourse.StartNotify;
            endPicker.Date = pageCourse.End;
            endNotify.IsChecked = pageCourse.EndNotify;
            description.Text = pageCourse.Description;
            notes.Text = pageCourse.Notes;
            instLabel.Text = string.IsNullOrEmpty(pageCourse.InstructorName) ? "Add Instructor" : pageCourse.InstructorName;
            SetAssessments();
            UpdateDateRange();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("ALERT!", "Are you sure you want to leave?", "Yes", "No"))
                {
                    await Navigation.PopModalAsync();
                }
            });
            return true;
        }

        private void OnCourseLabelTapped(object sender, EventArgs args)
        {
            ((Label)sender).IsVisible = false;
            courseEntry.IsVisible = true;
            courseEntry.Focus();
        }
        private void courseEntry_Focused(object sender, FocusEventArgs e)
        {
            courseEntry.CursorPosition = 0;
            courseEntry.SelectionLength = courseEntry.Text.Length;
        }
        private void OnCourseEntryUnfocused(object sender, FocusEventArgs args)
        {
            ((Entry)sender).IsVisible = false;
            courseLabel.IsVisible = true;
        }

        private void OnPickerIndexChanged(object sender, EventArgs e)
        {
            switch (statusPicker.SelectedItem)
            {

                case Course.Statuses.Active:
                case Course.Statuses.Complete:
                    statusPicker.TextColor = Color.LimeGreen;
                    break;

                case Course.Statuses.Dropped:
                    statusPicker.TextColor = Color.FromHex("#CC0000");
                    break;

                case Course.Statuses.Planned:
                    statusPicker.TextColor = Color.FromHex("#0000CC");
                    break;

            }
        }

        private void SetAssessments()
        {
            SetAssessmentsVisible();
            performance.IsVisible = pageCourse.PerfSelected;
            performance.Text = !string.IsNullOrEmpty(pageCourse.PerfAssName) ? pageCourse.PerfAssName + " - (P)" : "Add Assessment Name";
            objective.IsVisible = pageCourse.ObjSelected;
            objective.Text = !string.IsNullOrEmpty(pageCourse.ObjAssName) ? pageCourse.ObjAssName + " - (O)" : "Add Assessment Name";
        }
        private void SetAssessmentsVisible()
        {
            if (pageCourse.PerfSelected && pageCourse.ObjSelected)
            {
                assessLabel.IsVisible = true;
                performance.IsVisible = true;
                objective.IsVisible = true;
                addAssBtn.IsVisible = false;
            }
            else if (pageCourse.PerfSelected && !pageCourse.ObjSelected)
            {
                assessLabel.IsVisible = true;
                performance.IsVisible = true;
                objective.IsVisible = false;
                addAssBtn.IsVisible = true;
            }
            else if (!pageCourse.PerfSelected && pageCourse.ObjSelected)
            {
                assessLabel.IsVisible = true;
                performance.IsVisible = false;
                objective.IsVisible = true;
                addAssBtn.IsVisible = true;
            }
            else
            {
                assessLabel.IsVisible = false;
                performance.IsVisible = false;
                objective.IsVisible = false;
                addAssBtn.IsVisible = true;
            }
        }
        private async void OnAssessmentLabelTapped(object sender, EventArgs e)
        {
            SaveCourse();
            await Navigation.PushModalAsync(new AssessmentPage(pageCourse));
            SetAssessments();
        }
        private async void addAssBtn_Clicked(object sender, EventArgs e)
        {
            SaveCourse();
            await Navigation.PushModalAsync(new AssessmentPage(pageCourse));
            SetAssessments();
        }

        private void OnInstructorLabelTapped(object sender, EventArgs e)
        {
            SaveCourse();
            SetInstructor();
            instLabel.Text = string.IsNullOrEmpty(pageCourse.InstructorName) ? "Add Instructor" : pageCourse.InstructorName;
        }
        public async void SetInstructor()
        {
            await Navigation.PushModalAsync(new InstructorPage(pageCourse));
        }

        private void SaveCourse()
        {
            if (!string.IsNullOrEmpty(courseLabel.Text) || courseLabel.Text == "Course Name")
            {
                pageCourse.Name = courseLabel.Text;
                pageCourse.Status = (Course.Statuses)statusPicker.SelectedItem;
                pageCourse.Start = startPicker.Date;
                pageCourse.StartNotify = startNotify.IsChecked;
                pageCourse.End = endPicker.Date;
                pageCourse.EndNotify = endNotify.IsChecked;
                pageCourse.Description = description.Text;
                pageCourse.Notes = notes.Text;
                validCourse = true;
            }
            else
            {
                validCourse = false;
                DisplayAlert("Invalid!", "Please assign a Course Name", "OK");
            }
        }
        private async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            SaveCourse();
            if(validCourse)
            {
                if (pageCourse.ID != 0) // is edit case
                {
                    await DataManager.UpdateCourse(pageCourse);
                }
                else //add case
                {
                    var id = await DataManager.InsertCourse(pageCourse);
                    pageCourse.ID = id;
                }
                SetNotifications();
                await Navigation.PopModalAsync();
            }
        }
        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            if (pageCourse.ID == 0) //add
            {
           //     TermPage.createdCourses.Remove(pageCourse);
            }
            else //edit
            {
                await DataManager.DeleteCourse(pageCourse);
            }
            await Navigation.PopModalAsync();
        }

        private void SetNotifications()
        {
            if (startNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageCourse.Name} Starting", 
                    $"{pageCourse.Name} starting NOW!", 
                    pageCourse.ID + 30000, 
                    startPicker.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageCourse.ID + 30000);
            }
            if (endNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageCourse.Name} Ending",
                    $"{pageCourse.Name} ending NOW!",
                    pageCourse.ID + 40000,
                    endPicker.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageCourse.ID + 40000);
            }
        }

        private void startPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateDateRange();
        }

        private void endPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateDateRange();
        }
        private void UpdateDateRange()
        {
            startPicker.MaximumDate = endPicker.Date;
            endPicker.MinimumDate = startPicker.Date;
        }

        private async void ShareButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(notes.Text);
        }
    }
}