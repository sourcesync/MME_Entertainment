﻿#pragma checksum "..\..\UserControl1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0CD228748CF2F82D28F5D98F5CF944EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfKb.Controls;


namespace HerculesWPFDJRequestor {
    
    
    /// <summary>
    /// UserControl1
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class UserControl1 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas_master;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfKb.Controls.FloatingTouchScreenKeyboard Keyboard;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox1;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image2;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock1;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image button1;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image button2;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image3;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\UserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image4;
        
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
            System.Uri resourceLocater = new System.Uri("/HerculesWPFDJRequestor;component/usercontrol1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserControl1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.image1 = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Keyboard = ((WpfKb.Controls.FloatingTouchScreenKeyboard)(target));
            
            #line 17 "..\..\UserControl1.xaml"
            this.Keyboard.TextInput += new System.Windows.Input.TextCompositionEventHandler(this.Keyboard_TextInput);
            
            #line default
            #line hidden
            
            #line 17 "..\..\UserControl1.xaml"
            this.Keyboard.KeyDown += new System.Windows.Input.KeyEventHandler(this.Keyboard_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.textBox1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\UserControl1.xaml"
            this.textBox1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBox1_TextChanged);
            
            #line default
            #line hidden
            
            #line 18 "..\..\UserControl1.xaml"
            this.textBox1.TextInput += new System.Windows.Input.TextCompositionEventHandler(this.textBox1_TextInput);
            
            #line default
            #line hidden
            
            #line 18 "..\..\UserControl1.xaml"
            this.textBox1.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textBox1_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 18 "..\..\UserControl1.xaml"
            this.textBox1.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.textBox1_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.image2 = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.button1 = ((System.Windows.Controls.Image)(target));
            
            #line 21 "..\..\UserControl1.xaml"
            this.button1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.button1_MouseDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\UserControl1.xaml"
            this.button1.TouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.button1_TouchDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.button2 = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\UserControl1.xaml"
            this.button2.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.button2_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.image3 = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.image4 = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

