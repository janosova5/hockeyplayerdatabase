﻿#pragma checksum "..\..\DialogWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D86FBDEF045FBADA18F180BC61BC0585"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HockeyPlayerDatabase.MainApp;
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


namespace HockeyPlayerDatabase.MainApp {
    
    
    /// <summary>
    /// DialogWindow
    /// </summary>
    public partial class DialogWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\DialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\DialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CategoryCombobox;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\DialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ClubCombobox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HockeyPlayerDatabase.MainApp;component/dialogwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DialogWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\DialogWindow.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButtonWasClicked);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 51 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButtonWasClicked);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 54 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.KrpWasTyped);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 57 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TitleWasTyped);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 60 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FirstNameWasTyped);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 63 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.LastNameWasTyped);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\DialogWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.YearWasTyped);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CategoryCombobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 69 "..\..\DialogWindow.xaml"
            this.CategoryCombobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CategoryWasSelected);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ClubCombobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 72 "..\..\DialogWindow.xaml"
            this.ClubCombobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ClubWasSelected);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

