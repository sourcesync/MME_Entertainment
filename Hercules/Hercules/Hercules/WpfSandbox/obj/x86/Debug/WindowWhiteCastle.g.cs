﻿#pragma checksum "..\..\..\WindowWhiteCastle.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1EE578353DD2B6EB1BBA645EC19F1C9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FullScreenDemo;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfSandbox;


namespace WpfSandbox {
    
    
    /// <summary>
    /// WindowWhiteCastle
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class WindowWhiteCastle : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas_master;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfSandbox.UserControlBlank ctlblank;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfSandbox.UserControlMain ctlmain;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfSandbox.UserControlMenu ctlmenu;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfSandbox.UserControlChoose ctlchoose;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton FullScreenButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\WindowWhiteCastle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfSandbox;component/windowwhitecastle.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowWhiteCastle.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.canvas_master = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.ctlblank = ((WpfSandbox.UserControlBlank)(target));
            return;
            case 3:
            this.ctlmain = ((WpfSandbox.UserControlMain)(target));
            return;
            case 4:
            this.ctlmenu = ((WpfSandbox.UserControlMenu)(target));
            return;
            case 5:
            this.ctlchoose = ((WpfSandbox.UserControlChoose)(target));
            return;
            case 6:
            this.FullScreenButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 7:
            this.image1 = ((System.Windows.Controls.Image)(target));
            
            #line 27 "..\..\..\WindowWhiteCastle.xaml"
            this.image1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.image1_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.image2 = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\..\WindowWhiteCastle.xaml"
            this.image2.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.image1_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

