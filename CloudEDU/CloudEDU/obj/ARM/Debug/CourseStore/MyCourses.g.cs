﻿

#pragma checksum "C:\cloudEDUproject\CloudEDUClientNew\CloudEDU\CloudEDU\CourseStore\MyCourses.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5909C197D4195E4C7FE6A69570B42FD3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudEDU.CourseStore
{
    partial class MyCourses : global::CloudEDU.Common.GlobalPage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 26 "..\..\..\CourseStore\MyCourses.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.GridView_ItemClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 70 "..\..\..\CourseStore\MyCourses.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.UserProfileButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 66 "..\..\..\CourseStore\MyCourses.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BackButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


