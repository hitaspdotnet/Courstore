﻿

#pragma checksum "C:\cloudEDUproject\CloudEDUClientNew\CloudEDU\CloudEDU\Login\LoginDefault.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "483F29E1CC16247030BF45BACA259993"
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
    partial class LoginDefault : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel UsersStack; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.PasswordBox InputPassword; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.CheckBox CheckAutoLogin; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Login/LoginDefault.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            UsersStack = (global::Windows.UI.Xaml.Controls.StackPanel)this.FindName("UsersStack");
            InputPassword = (global::Windows.UI.Xaml.Controls.PasswordBox)this.FindName("InputPassword");
            CheckAutoLogin = (global::Windows.UI.Xaml.Controls.CheckBox)this.FindName("CheckAutoLogin");
        }
    }
}



