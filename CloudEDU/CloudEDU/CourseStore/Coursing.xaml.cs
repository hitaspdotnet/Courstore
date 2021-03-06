﻿using CloudEDU.Common;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace CloudEDU.CourseStore
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Coursing : GlobalPage
    {
        /// <summary>
        /// The course
        /// </summary>
        Course course;
        /// <summary>
        /// The c information
        /// </summary>
        List<object> cInfo = null;

        /// <summary>
        /// The page red
        /// </summary>
        SolidColorBrush pageRed;
        /// <summary>
        /// The page blue
        /// </summary>
        SolidColorBrush pageBlue;
        /// <summary>
        /// The page green
        /// </summary>
        SolidColorBrush pageGreen;
        /// <summary>
        /// The page white
        /// </summary>
        SolidColorBrush pageWhite;
        /// <summary>
        /// The page black
        /// </summary>
        SolidColorBrush pageBlack;

        /// <summary>
        /// Constructor, initialize the components.
        /// </summary>
        public Coursing()
        {
            this.InitializeComponent();

            pageRed = this.Resources["PageRed"] as SolidColorBrush;
            pageBlue = this.Resources["PageBlue"] as SolidColorBrush;
            pageGreen = this.Resources["PageGreen"] as SolidColorBrush;
            pageWhite = new SolidColorBrush(Colors.White);
            pageBlack = new SolidColorBrush(Colors.Black);

            Constants.coursing = this;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<object> courseInfo = e.Parameter as List<object>;
            cInfo = courseInfo;
            course = courseInfo[0] as Course;
            DataContext = course;
            NavigateText.Text = courseInfo[1] as string;
            CourseTitle.Text = Constants.UpperInitialChar(course.Title);

            HomeBorder.Background = pageRed;
            LecturesBorder.Background = pageWhite;
            NotesBorder.Background = pageWhite;

            HomeText.Foreground = pageWhite;
            LecturesText.Foreground = pageBlack;
            NotesText.Foreground = pageBlack;

            detailFrame.Navigate(typeof(CoursingDetail.Home), cInfo);
            UserProfileBt.DataContext = Constants.User;

        }

        /// <summary>
        /// Invoked when back button is clicked and return the last page.
        /// </summary>
        /// <param name="sender">The back button clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(CourseStore.Courstore));
            }
        }

        /// <summary>
        /// Invoked when home text is tapped and navigating to the home fame
        /// </summary>
        /// <param name="sender">The home text tapped.</param>
        /// <param name="e">Event data that describes how the tap was initiated.</param>
        private void HomeText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ContentBackgroundRect.Fill != pageRed)
            {
                NavigateToHome();
            }
        }

        /// <summary>
        /// Invoked when lecture text is tapped and navigating to the lecture fame
        /// </summary>
        /// <param name="sender">The lecture text tapped.</param>
        /// <param name="e">Event data that describes how the tap was initiated.</param>
        private void LecturesText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ContentBackgroundRect.Fill != pageBlue)
            {
                NavigateToLecture();
            }
        }

        /// <summary>
        /// Invoked when note text is tapped and navigating to the note fame
        /// </summary>
        /// <param name="sender">The note text tapped.</param>
        /// <param name="e">Event data that describes how the tap was initiated.</param>
        private void NotesText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ContentBackgroundRect.Fill != pageGreen)
            {
                NavigateToNote();
            }
        }

        /// <summary>
        /// Handles the Click event of the UserProfileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof());
        }


        /// <summary>
        /// Navigates to note.
        /// </summary>
        public void NavigateToNote()
        {
            HomeBorder.Background = pageWhite;
            LecturesBorder.Background = pageWhite;
            NotesBorder.Background = pageGreen;

            HomeText.Foreground = pageBlack;
            LecturesText.Foreground = pageBlack;
            NotesText.Foreground = pageWhite;

            ContentBackgroundRect.Fill = pageGreen;
            detailFrame.Navigate(typeof(CoursingDetail.Note), course);
        }

        /// <summary>
        /// Navigates to lecture.
        /// </summary>
        public void NavigateToLecture()
        {
            HomeBorder.Background = pageWhite;
            LecturesBorder.Background = pageBlue;
            NotesBorder.Background = pageWhite;

            HomeText.Foreground = pageBlack;
            LecturesText.Foreground = pageWhite;
            NotesText.Foreground = pageBlack;

            ContentBackgroundRect.Fill = pageBlue;

            detailFrame.Navigate(typeof(CoursingDetail.Lecture), course);
        }

        /// <summary>
        /// Navigates to home.
        /// </summary>
        public void NavigateToHome()
        {
            HomeBorder.Background = pageRed;
            LecturesBorder.Background = pageWhite;
            NotesBorder.Background = pageWhite;

            HomeText.Foreground = pageWhite;
            LecturesText.Foreground = pageBlack;
            NotesText.Foreground = pageBlack;

            ContentBackgroundRect.Fill = pageRed;

            detailFrame.Navigate(typeof(CoursingDetail.Home), cInfo);
        }
    }
}
