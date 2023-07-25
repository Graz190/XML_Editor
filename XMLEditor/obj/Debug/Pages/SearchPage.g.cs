﻿#pragma checksum "..\..\..\Pages\SearchPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E8BA45BEC05B3DE76F868A08DB65953D11EE20F236BF09A7229FBC36BDEDE4F6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
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


namespace XMLEditor {
    
    
    /// <summary>
    /// SearchPage
    /// </summary>
    public partial class SearchPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel searchWindow;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox openFilePathBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openFileButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTermBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label searchTermLabel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\SearchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgIdValue;
        
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
            System.Uri resourceLocater = new System.Uri("/XMLEditor;component/pages/searchpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\SearchPage.xaml"
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
            this.searchWindow = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.openFilePathBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.openFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Pages\SearchPage.xaml"
            this.openFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchTermBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.StartButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Pages\SearchPage.xaml"
            this.StartButton.Click += new System.Windows.RoutedEventHandler(this.runSearch);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ExitButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Pages\SearchPage.xaml"
            this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.shutdownApp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.searchTermLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.dgIdValue = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
