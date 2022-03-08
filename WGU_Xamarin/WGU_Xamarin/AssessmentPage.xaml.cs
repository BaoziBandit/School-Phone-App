using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using System;

namespace WGU_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
        Course pageCourse;
        public AssessmentPage(Course course)
        {
            pageCourse = course;
            InitializeComponent();

            perfNotify.IsChecked = pageCourse.PerfNotify;
            objNotify.IsChecked = pageCourse.ObjNotify;
            perfDueDate.Date = pageCourse.PerfDueDate;
            objDueDate.Date = pageCourse.ObjDueDate;
            perfSelected.IsChecked = pageCourse.PerfSelected;
            objSelected.IsChecked = pageCourse.ObjSelected;

            if (pageCourse.PerfAssName != null && pageCourse.ObjAssName != null)
            {
                perfAssNameLabel.Text = pageCourse.PerfAssName;
                ObjAssNameLabel.Text = pageCourse.ObjAssName;
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        private async void saveAssessments_Clicked(object sender, EventArgs e)
        {
            pageCourse.PerfNotify = perfNotify.IsChecked;
            pageCourse.PerfDueDate = perfDueDate.Date;
            pageCourse.PerfSelected = perfSelected.IsChecked;
            pageCourse.ObjNotify = objNotify.IsChecked;
            pageCourse.ObjDueDate = objDueDate.Date;
            pageCourse.ObjSelected= objSelected.IsChecked;
            pageCourse.PerfAssName = perfAssNameLabel.Text;
            pageCourse.ObjAssName = ObjAssNameLabel.Text;
            SetNotifications();

            await Navigation.PopModalAsync();
        }

        private void SetNotifications()
        {
            if (perfNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageCourse.Name} Starting",
                    $"{pageCourse.Name} Performance due NOW!",
                    pageCourse.ID + 50000,
                    perfDueDate.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageCourse.ID + 50000);
            }
            if (objNotify.IsChecked)
                CrossLocalNotifications.Current.Show($"Course {pageCourse.Name} Ending",
                    $"{pageCourse.Name} Objective due NOW!",
                    pageCourse.ID + 60000,
                    objDueDate.Date);
            else
            {
                CrossLocalNotifications.Current.Cancel(pageCourse.ID + 60000);
            }
        }

        string objNameOrig;
        string perfNameOrig;

        private void OnObjAssNameLabelTapped(object sender, EventArgs e)
        {
            objNameOrig = ObjAssNameLabel.Text;

            ((Label)sender).IsVisible = false;
            ObjAssNameEntry.IsVisible = true;
            ObjAssNameEntry.Focus();
        }
        private void OnObjAssNameEntryFocused(object sender, FocusEventArgs e)
        {
            ObjAssNameEntry.CursorPosition = 0;
            ObjAssNameEntry.SelectionLength = ObjAssNameEntry.Text.Length;
        }
        private void OnObjAssNameEntryUnfocused(object sender, FocusEventArgs args)
        {
            if (ObjAssNameEntry.Text.Trim() == "")
            {
                ObjAssNameLabel.Text = objNameOrig;
            }

            ((Entry)sender).IsVisible = false;
            ObjAssNameLabel.IsVisible = true;
        }

        private void OnPerfAssNameLabelTapped(object sender, EventArgs e)
        {
            perfNameOrig = perfAssNameLabel.Text;

            ((Label)sender).IsVisible = false;
            perfAssNameEntry.IsVisible = true;
            perfAssNameEntry.Focus();
        }
        private void OnPerfAssNameEntryFocused(object sender, FocusEventArgs e)
        {
            perfAssNameEntry.CursorPosition = 0;
            perfAssNameEntry.SelectionLength = perfAssNameEntry.Text.Length;
        }
        private void OnPerfAssNameEntryUnfocused(object sender, FocusEventArgs args)
        {
            if (perfAssNameEntry.Text.Trim() == "")
            {
                perfAssNameLabel.Text = perfNameOrig;
            }

            ((Entry)sender).IsVisible = false;
            perfAssNameLabel.IsVisible = true;
        }
    }
}