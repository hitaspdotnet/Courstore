﻿

#pragma checksum "C:\Users\Boy\Documents\Visual Studio 2013\Projects\Courstore\CloudEDU\CloudEDU\Login\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58FE3F65BF60B3DB532D6B26FE77BEA9"
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
    partial class Login : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 25 "..\..\..\Login\Login.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.InputUsername_TextChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 36 "..\..\..\Login\Login.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.InputPassword_KeyDown;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 40 "..\..\..\Login\Login.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.LoginButton_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 43 "..\..\..\Login\Login.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SignUpButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


