﻿#pragma checksum "..\..\Window1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4E72CDF88DA9A1302318E24A55AAEC83"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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


namespace WPFCSharpWebCam {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Window1.xaml"
        internal WPFCSharpWebCam.Window1 mainWindow;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Window1.xaml"
        internal System.Windows.Controls.Image imgVideo;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Window1.xaml"
        internal System.Windows.Controls.Image imgCapture;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntCapture;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntSaveImage;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntResolution;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntSetting;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntStart;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntStop;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\Window1.xaml"
        internal System.Windows.Controls.Button bntContinue;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFCSharpWebCam;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mainWindow = ((WPFCSharpWebCam.Window1)(target));
            
            #line 4 "..\..\Window1.xaml"
            this.mainWindow.Loaded += new System.Windows.RoutedEventHandler(this.mainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgVideo = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.imgCapture = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.bntCapture = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\Window1.xaml"
            this.bntCapture.Click += new System.Windows.RoutedEventHandler(this.bntCapture_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bntSaveImage = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\Window1.xaml"
            this.bntSaveImage.Click += new System.Windows.RoutedEventHandler(this.bntSaveImage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bntResolution = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\Window1.xaml"
            this.bntResolution.Click += new System.Windows.RoutedEventHandler(this.bntResolution_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bntSetting = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\Window1.xaml"
            this.bntSetting.Click += new System.Windows.RoutedEventHandler(this.bntSetting_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bntStart = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\Window1.xaml"
            this.bntStart.Click += new System.Windows.RoutedEventHandler(this.bntStart_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bntStop = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\Window1.xaml"
            this.bntStop.Click += new System.Windows.RoutedEventHandler(this.bntStop_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.bntContinue = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\Window1.xaml"
            this.bntContinue.Click += new System.Windows.RoutedEventHandler(this.bntContinue_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

