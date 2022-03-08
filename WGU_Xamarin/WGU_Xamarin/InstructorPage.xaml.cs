using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGU_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructorPage : ContentPage
    {
        bool nameValid;
        bool phoneValid;
        bool emailValid;

        Color validTxt = Color.Green;
        Color invalidTxt = Color.Red;


        Course pageCourse;
        public InstructorPage(Course course)
        {
            pageCourse = course;
            InitializeComponent();
            
            profName.Text = string.IsNullOrWhiteSpace(pageCourse.InstructorName) ? "Name" : pageCourse.InstructorName;
            nameValid = !(string.IsNullOrWhiteSpace(pageCourse.InstructorName) || pageCourse.InstructorName == "Name");
            profPhone.Text = string.IsNullOrWhiteSpace(pageCourse.InstructorPhone) ? "Phone" : pageCourse.InstructorPhone;
            phoneValid = !(string.IsNullOrWhiteSpace(pageCourse.InstructorPhone) || pageCourse.InstructorPhone == "Phone");
            profEmail.Text = string.IsNullOrWhiteSpace(pageCourse.InstructorEmail) ? "Email" : pageCourse.InstructorEmail;
            emailValid = !(string.IsNullOrWhiteSpace(pageCourse.InstructorEmail) || pageCourse.InstructorEmail == "Email");
        }

        private void OnNameTapped(object sender, EventArgs e)
        {
            ((Label)sender).IsVisible = false;
            entryName.IsVisible = true;
            entryName.Focus();
        }
        private void entryName_Focused(object sender, FocusEventArgs e)
        {
            entryName.CursorPosition = 0;
            entryName.SelectionLength = entryName.Text.Length;
            if (entryName.Text == "Name")
                entryName.Text = "";
        }
        private void entryName_Unfocused(object sender, FocusEventArgs e)
        {
            ((Entry)sender).IsVisible = false;
            profName.IsVisible = true;

            nameValid = Validation.ValidateName(profName.Text);

            if (nameValid)
                profName.TextColor = validTxt;
            else
                profName.TextColor = invalidTxt;
            if (string.IsNullOrEmpty(profName.Text))
                profName.Text = "Name";
        }

        private void OnPhoneTapped(object sender, EventArgs e)
        {
            ((Label)sender).IsVisible = false;
            entryPhone.IsVisible = true;
            entryPhone.Focus();
        }
        private void entryPhone_Focused(object sender, FocusEventArgs e)
        {
            entryPhone.CursorPosition = 0;
            entryPhone.SelectionLength = entryPhone.Text.Length;
            if (entryPhone.Text == "Phone")
                entryPhone.Text = "";
        }
        private void entryPhone_Unfocused(object sender, FocusEventArgs e)
        {
            ((Entry)sender).IsVisible = false;
            profPhone.IsVisible = true;

            phoneValid = Validation.ValidatePhone(profPhone.Text.Trim());
            if(phoneValid)
                profPhone.TextColor = validTxt;
            else
                profPhone.TextColor = invalidTxt;
            if (string.IsNullOrEmpty(profPhone.Text))
                profPhone.Text = "Phone";
        }

        private void OnEmailTapped(object sender, EventArgs e)
        {
            ((Label)sender).IsVisible = false;
            entryEmail.IsVisible = true;
            entryEmail.Focus();
        }
        private void entryEmail_Focused(object sender, FocusEventArgs e)
        {
            entryEmail.CursorPosition = 0;
            entryEmail.SelectionLength = entryEmail.Text.Length;
            if (entryEmail.Text == "Email")
                entryEmail.Text = "";
        }
        private void entryEmail_Unfocused(object sender, FocusEventArgs e)
        {
            ((Entry)sender).IsVisible = false;
            profEmail.IsVisible = true;

            emailValid = Validation.ValidateEmail(profEmail.Text.Trim());
            if(emailValid)
                profEmail.TextColor = validTxt;
            else
                profEmail.TextColor = invalidTxt;
            if (string.IsNullOrEmpty(profEmail.Text))
                profEmail.Text = "Email";
        }

        private async void saveInstructBtn_Clicked(object sender, EventArgs e)
        {
            if (nameValid && phoneValid && emailValid)
            {
                pageCourse.InstructorName = profName.Text;
                pageCourse.InstructorPhone = profPhone.Text;
                pageCourse.InstructorEmail = profEmail.Text;

                await Navigation.PopModalAsync();
            }
            else
                await DisplayAlert("Invalid!", "Please enter a valid name, phone number and email address.", "Thank you");
            
        }
    }
}