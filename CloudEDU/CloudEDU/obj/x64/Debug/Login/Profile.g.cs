﻿

#pragma checksum "C:\cloudEDUproject\CloudEDUClientNew\CloudEDU\CloudEDU\Login\Profile.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4C1F78080FD95372D592A6155DD98534"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudEDU.Login
{
    partial class Profile : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 119 "..\..\..\Login\Profile.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BackButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 104 "..\..\..\Login\Profile.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.SaveImage_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 105 "..\..\..\Login\Profile.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.ResetImage_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 88 "..\..\..\Login\Profile.xaml"
                ((global::Windows.UI.Xaml.Controls.PasswordBox)(target)).PasswordChanged += this.Password_PasswordChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 77 "..\..\..\Login\Profile.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.degreeBox_SelectionChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


