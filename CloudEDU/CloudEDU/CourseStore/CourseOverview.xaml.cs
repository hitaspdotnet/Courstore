﻿using CloudEDU.Common;
using CloudEDU.Service;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CloudEDU.CourseStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CourseOverview : GlobalPage
    {
        /// <summary>
        /// The course
        /// </summary>
        Course course;
        /// <summary>
        /// The CTX
        /// </summary>
        CloudEDUEntities ctx = null;
        /// <summary>
        /// The teach courses
        /// </summary>
        DataServiceQuery<COURSE_AVAIL> teachCourses;
        /// <summary>
        /// The buy courses
        /// </summary>
        DataServiceQuery<ATTEND> buyCourses;

        /// <summary>
        /// The is teach
        /// </summary>
        bool isTeach;
        /// <summary>
        /// The is buy
        /// </summary>
        bool isBuy;

        /// <summary>
        /// The dba
        /// </summary>
        DBAccessAPIs dba;
        /// <summary>
        /// Constructor, initialize the components
        /// </summary>
        public CourseOverview()
        {
            this.InitializeComponent();
            ctx = new CloudEDUEntities(new Uri(Constants.DataServiceURI));
            dba = new DBAccessAPIs();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {





            course = e.Parameter as Course;


            //// Prerequisite initialization:






            /*
            for (int i = 0; i < 30; ++i)
            {
                AddToPreRequestCoursesPanel("Algorithm" + i);
            }
            AddToPreRequestCoursesPanel("We See");
            */


            List<Constants.DepCourse> results = Constants.DepCourse.GetDepCourses(course.Title);



            foreach (Constants.DepCourse dc in results)
            {

                System.Diagnostics.Debug.WriteLine("if learned");
                if (!Constants.DepCourse.IfLearned(dc.CourseName))
                {
                    System.Diagnostics.Debug.WriteLine("Learned");
                    AddToPreRequestCoursesPanel(dc.CourseName);
                }

                //System.Diagnostics.Debug.WriteLine(dc.CourseName);
            }





            try
            {
                DataServiceQuery<COURSE_AVAIL> cDsq = (DataServiceQuery<COURSE_AVAIL>)(from kc in ctx.COURSE_AVAIL
                                                                                       where course.ID == kc.ID
                                                                                       select kc);
                TaskFactory<IEnumerable<COURSE_AVAIL>> tf = new TaskFactory<IEnumerable<COURSE_AVAIL>>();
                COURSE_AVAIL tmpCourse = (await tf.FromAsync(cDsq.BeginExecute(null, null), iar => cDsq.EndExecute(iar))).FirstOrDefault();
                course = Constants.CourseAvail2Course(tmpCourse);
            }
            catch
            {
                ShowMessageDialog("Network connection error2!");
                Frame.GoBack();
            }

            UserProfileBt.DataContext = Constants.User;
            DataContext = course;
            introGrid.DataContext = course;

            frame.Navigate(typeof(CourseDetail.Overview), course);

            isTeach = false;
            isBuy = false;

            try
            {
                teachCourses = (DataServiceQuery<COURSE_AVAIL>)(from teachC in ctx.COURSE_AVAIL
                                                                where teachC.TEACHER_NAME == Constants.User.NAME
                                                                select teachC);
                TaskFactory<IEnumerable<COURSE_AVAIL>> teachTF = new TaskFactory<IEnumerable<COURSE_AVAIL>>();
                IEnumerable<COURSE_AVAIL> tcs = await teachTF.FromAsync(teachCourses.BeginExecute(null, null), iar => teachCourses.EndExecute(iar));

                buyCourses = (DataServiceQuery<ATTEND>)(from buyC in ctx.ATTEND
                                                        where buyC.CUSTOMER_ID == Constants.User.ID
                                                        select buyC);
                TaskFactory<IEnumerable<ATTEND>> buyCF = new TaskFactory<IEnumerable<ATTEND>>();
                IEnumerable<ATTEND> bcs = await buyCF.FromAsync(buyCourses.BeginExecute(null, null), iar => buyCourses.EndExecute(iar));


                foreach (var t in tcs)
                {
                    if (t.ID == course.ID)
                    {
                        isTeach = true;
                        break;
                    }
                }

                foreach (var b in bcs)
                {
                    if (b.COURSE_ID == course.ID)
                    {
                        isBuy = true;
                        break;
                    }
                }
            }
            catch
            {
                ShowMessageDialog("Network connection error3!");
                Frame.GoBack();
            }

            if (course.Price == null || course.Price.Value == 0)
            {
                PriceTextBlock.Text = "Free";
            }
            else
            {
                PriceTextBlock.Text = "$ " + Math.Round(course.Price.Value, 2);
            }
            System.Diagnostics.Debug.WriteLine(course.Rate);
            SetStarsStackPanel(course.Rate ?? 0);

            if (isTeach)
            {
                courseButton.Content = "Teach";
            }
            else if (isBuy)
            {
                courseButton.Content = "Attend";
            }
            else
            {
                courseButton.Content = "Buy";
            }
        }


        /// <summary>
        /// The number prerequisite
        /// </summary>
        private int numPrerequisite = 0;
        /// <summary>
        /// Adds to pre request courses panel.
        /// </summary>
        /// <param name="courseName">Name of the course.</param>
        private void AddToPreRequestCoursesPanel(string courseName)
        {






            numPrerequisite++;
            Grid newGrid = new Grid();
            Viewbox courseViewbox = new Viewbox()
            {
                Stretch = Stretch.Uniform,
                Width = 380,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 30
            };
            TextBlock courseTextBlock = new TextBlock()
            {
                Text = courseName,
                FontSize = 20
            };
            Border overBorder = new Border();
            TextBlock overviewText = new TextBlock()
            {
                Text = "Overview >",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 15
            };
            overviewText.Tapped += OverPreRequest_Clicked;

            courseViewbox.Child = courseTextBlock;
            overBorder.Child = overviewText;
            newGrid.Children.Add(courseViewbox);
            newGrid.Children.Add(overBorder);

            prerequestCoursesPanel.Children.Add(newGrid);
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
        /// Invoked when overview button is clicked and navigating to the overview part.
        /// </summary>
        /// <param name="sender">The overview button clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            var primaryStyle = Application.Current.Resources["TextPrimaryButtonStyle"] as Style;
            var secondaryStyle = Application.Current.Resources["TextSecondaryButtonStyle"] as Style;

            if (OverviewButton.Style != primaryStyle)
            {
                frame.Navigate(typeof(CourseDetail.Overview), course);

                OverviewButton.Style = primaryStyle;
                DetailsButton.Style = secondaryStyle;
                CommentsButton.Style = secondaryStyle;
            }
        }

        /// <summary>
        /// Invoked when detail button is clicked and navigating to the detail part.
        /// </summary>
        /// <param name="sender">The detail button clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var primaryStyle = Application.Current.Resources["TextPrimaryButtonStyle"] as Style;
            var secondaryStyle = Application.Current.Resources["TextSecondaryButtonStyle"] as Style;

            if (DetailsButton.Style != primaryStyle)
            {
                frame.Navigate(typeof(CourseDetail.Detail), course);

                OverviewButton.Style = secondaryStyle;
                DetailsButton.Style = primaryStyle;
                CommentsButton.Style = secondaryStyle;
            }
        }

        /// <summary>
        /// Invoked when comment button is clicked and navigating to the comment part.
        /// </summary>
        /// <param name="sender">The comment button clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void CommentsButton_Click(object sender, RoutedEventArgs e)
        {
            var primaryStyle = Application.Current.Resources["TextPrimaryButtonStyle"] as Style;
            var secondaryStyle = Application.Current.Resources["TextSecondaryButtonStyle"] as Style;

            if (CommentsButton.Style != primaryStyle)
            {
                frame.Navigate(typeof(CourseDetail.Comment), course);

                OverviewButton.Style = secondaryStyle;
                DetailsButton.Style = secondaryStyle;
                CommentsButton.Style = primaryStyle;
            }
        }

        /// <summary>
        /// Set the rate star according the rate.
        /// </summary>
        /// <param name="rate">The rate of course.</param>
        private void SetStarsStackPanel(double rate)
        {
            int fillInt = (int)rate;
            int blankInt = 5 - fillInt - 1;
            double percentFill = rate - (double)fillInt;

            for (int i = 0; i < fillInt; ++i)
            {
                TextBlock fillStarTextBlock = new TextBlock
                {
                    Style = Application.Current.Resources["SubheaderTextStyle"] as Style,
                    Foreground = new SolidColorBrush(Colors.White),
                    Text = Constants.FillStar
                };
                rateStarsPanel.Children.Add(fillStarTextBlock);
            }
            if (rate == 5) return;
            double width = Constants.StarWidth * percentFill;
            TextBlock halfFillStarTextBlock = new TextBlock
            {
                Style = Application.Current.Resources["SubheaderTextStyle"] as Style,
                Foreground = new SolidColorBrush(Colors.White),
                Text = Constants.FillStar,
                Width = width
            };
            TextBlock halfBlankStarTextBlock = new TextBlock
            {
                Style = Application.Current.Resources["SubheaderTextStyle"] as Style,
                Foreground = new SolidColorBrush(Colors.White),
                Text = Constants.BlankStar,
                Margin = new Thickness(-width, 0, 0, 0)
            };
            rateStarsPanel.Children.Add(halfFillStarTextBlock);
            rateStarsPanel.Children.Add(halfBlankStarTextBlock);
            for (int i = 0; i < blankInt; ++i)
            {
                TextBlock blankStarTextBlock = new TextBlock
                {
                    Style = Application.Current.Resources["SubheaderTextStyle"] as Style,
                    Foreground = new SolidColorBrush(Colors.White),
                    Text = Constants.BlankStar,
                };
                rateStarsPanel.Children.Add(blankStarTextBlock);
            }
        }

        /// <summary>
        /// Handles the Click event of the UserProfileButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login.Profile));
        }





        /// <summary>
        /// Buys the course.
        /// </summary>
        /// <param name="bt">The bt.</param>
        /// <param name="courseInfo">The course information.</param>
        private async void BuyCourse(Button bt, List<Object> courseInfo)
        {


            bool isToBuy = false;
            bool isHaveBuy = false;

            var buySure = new MessageDialog("Are you sure to buy this course?", "Buy Course");
            buySure.Commands.Add(new UICommand("Yes", (command) =>
            {
                isToBuy = true;
            }));
            buySure.Commands.Add(new UICommand("No", (command) =>
            {
                isToBuy = false;
                return;
            }));
            await buySure.ShowAsync();

            if (isToBuy)
            {
                try
                {
                    string uri = "/EnrollCourse?customer_id=" + Constants.User.ID + "&course_id=" + course.ID;
                    TaskFactory<IEnumerable<int>> tf = new TaskFactory<IEnumerable<int>>();
                    IEnumerable<int> code = await tf.FromAsync(ctx.BeginExecute<int>(new Uri(uri, UriKind.Relative), null, null), iar => ctx.EndExecute<int>(iar));
                    isHaveBuy = true;

                    if (code.FirstOrDefault() != 0)
                    {
                        isHaveBuy = false;
                        var buyError = new MessageDialog("You don't have enough money. Please contact Scott Zhao.", "Buy Failed");
                        buyError.Commands.Add(new UICommand("Close"));
                        await buyError.ShowAsync();
                        return;
                    }

                }
                catch
                {
                    ShowMessageDialog("Network connection error1!");
                    return;
                }
            }

            if (isHaveBuy)
            {
                var buyOkMsg = new MessageDialog("Do you want to start learning?", "Buy successfully");
                buyOkMsg.Commands.Add(new UICommand("Yes", (command) =>
                {
                    courseInfo.Add("attending");
                    Frame.Navigate(typeof(Coursing), courseInfo);
                }));
                buyOkMsg.Commands.Add(new UICommand("No", (command) =>
                {
                    bt.Content = "Attend";
                }));
                await buyOkMsg.ShowAsync();
            }
        }



        /// <summary>
        /// The temporary button
        /// </summary>
        private Button tmpButton;

        /// <summary>
        /// Handles the Click event of the AttendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AttendButton_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            tmpButton = bt;
            List<object> courseInfo = new List<object>();
            courseInfo.Add(course);

            if (bt.Content == null || bt.Content.ToString().Equals("")) return;

            if (bt.Content.ToString() == "Teach")
            {
                courseInfo.Add("teaching");
                Frame.Navigate(typeof(Coursing), courseInfo);
            }
            else if (bt.Content.ToString() == "Attend")
            {

                if (numPrerequisite == 0)
                {
                    courseInfo.Add("attending");
                    Frame.Navigate(typeof(Coursing), courseInfo);
                }
                else
                {





                    prerequestCoursePopup.IsOpen = true;
                }
                //Frame.Navigate(typeof(Coursing), courseInfo);
            }
            else if (bt.Content.ToString() == "Buy")
            {


                if (numPrerequisite != 0)
                {
                    //courseInfo.Add("attending");

                    //Frame.Navigate(typeof(Coursing), courseInfo);








                    prerequestCoursePopup.IsOpen = true;
                    return;
                }
                else
                {

                    BuyCourse(bt, courseInfo);
                }
                /*

                bool isToBuy = false;
                bool isHaveBuy = false;

                var buySure = new MessageDialog("Are you sure to buy this course?", "Buy Course");
                buySure.Commands.Add(new UICommand("Yes", (command) =>
                    {
                        isToBuy = true;
                    }));
                buySure.Commands.Add(new UICommand("No", (command) =>
                    {
                        isToBuy = false;
                        return;
                    }));
                await buySure.ShowAsync();

                if (isToBuy)
                {
                    try
                    {
                        string uri = "/EnrollCourse?customer_id=" + Constants.User.ID + "&course_id=" + course.ID;
                        TaskFactory<IEnumerable<int>> tf = new TaskFactory<IEnumerable<int>>();
                        IEnumerable<int> code = await tf.FromAsync(ctx.BeginExecute<int>(new Uri(uri, UriKind.Relative), null, null), iar => ctx.EndExecute<int>(iar));
                        isHaveBuy = true;

                        if (code.FirstOrDefault() != 0)
                        {
                            isHaveBuy = false;
                            var buyError = new MessageDialog("You don't have enough money. Please contact Scott Zhao.", "Buy Failed");
                            buyError.Commands.Add(new UICommand("Close"));
                            await buyError.ShowAsync();
                            return;
                        }

                    }
                    catch
                    {
                        ShowMessageDialog("Network connection error1!");
                        return;
                    }
                }

                if (isHaveBuy)
                {
                    var buyOkMsg = new MessageDialog("Do you want to start learning?", "Buy successfully");
                    buyOkMsg.Commands.Add(new UICommand("Yes", (command) =>
                        {
                            courseInfo.Add("attending");
                            Frame.Navigate(typeof(Coursing), courseInfo);
                        }));
                    buyOkMsg.Commands.Add(new UICommand("No", (command) =>
                        {
                            bt.Content = "Attend";
                        }));
                    await buyOkMsg.ShowAsync();
                }
                 */
            }
        }

        /// <summary>
        /// Upload information error MessageDialog.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private async void ShowMessageDialog(string msg)
        {
            var messageDialog = new MessageDialog(msg);
            messageDialog.Commands.Add(new UICommand("Close"));
            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Handles the Clicked event of the OverPreRequest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TappedRoutedEventArgs"/> instance containing the event data.</param>
        private async void OverPreRequest_Clicked(object sender, TappedRoutedEventArgs e)
        {
            TextBlock tappedText = sender as TextBlock;
            Grid tappedGrid = (tappedText.Parent as Border).Parent as Grid;
            var viewBoxes = from vb in tappedGrid.Children.OfType<Viewbox>()
                            select vb;
            TextBlock courseText = viewBoxes.First().Child as TextBlock;

            System.Diagnostics.Debug.WriteLine("course Name = " + courseText.Text);

            DataServiceQuery<COURSE_AVAIL> cDsq = (DataServiceQuery<COURSE_AVAIL>)(from kc in ctx.COURSE_AVAIL
                                                                                   where kc.TITLE == courseText.Text
                                                                                   select kc);
            TaskFactory<IEnumerable<COURSE_AVAIL>> tf = new TaskFactory<IEnumerable<COURSE_AVAIL>>();
            COURSE_AVAIL tmpCourse = (await tf.FromAsync(cDsq.BeginExecute(null, null), iar => cDsq.EndExecute(iar))).FirstOrDefault();
            Course navigateToCourse = Constants.CourseAvail2Course(tmpCourse);

            Frame.Navigate(typeof(CourseOverview), navigateToCourse);
        }

        /// <summary>
        /// Handles the Click event of the ClosePopupButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            prerequestCoursePopup.IsOpen = false;
        }

        /// <summary>
        /// Handles the Click event of the ClosePopupContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ClosePopupContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (tmpButton.Content.ToString() == "Attend")
            {
                System.Diagnostics.Debug.WriteLine("Continue Button Clicked");
                prerequestCoursePopup.IsOpen = false;
                List<object> courseInfo = new List<object>();
                courseInfo.Add(course);
                courseInfo.Add("attending");
                Frame.Navigate(typeof(Coursing), courseInfo);
            }
            else if (tmpButton.Content.ToString() == "Buy")
            {
                System.Diagnostics.Debug.WriteLine("Continue Button Clicked with buy");
                List<object> courseInfo = new List<object>();
                courseInfo.Add(course);
                BuyCourse(tmpButton, courseInfo);
            }



        }
    }
}
