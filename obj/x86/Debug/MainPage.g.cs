﻿#pragma checksum "C:\Users\Zac\source\repos\CrossWordPuzzle\CrossWordPuzzle\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58CE1ABFC43A382DC59775FE50A8FA62"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrossWordPuzzle
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::CrossWordPuzzle.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ItemsControl obj6;
            private global::Windows.UI.Xaml.Controls.ItemsControl obj8;

            private MainPage_obj1_BindingsTracking bindingsTracking;

            public MainPage_obj1_Bindings()
            {
                this.bindingsTracking = new MainPage_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 6: // MainPage.xaml line 111
                        this.obj6 = (global::Windows.UI.Xaml.Controls.ItemsControl)target;
                        break;
                    case 8: // MainPage.xaml line 96
                        this.obj8 = (global::Windows.UI.Xaml.Controls.ItemsControl)target;
                        this.bindingsTracking.RegisterTwoWayListener_8(this.obj8);
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::CrossWordPuzzle.MainPage)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::CrossWordPuzzle.MainPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update__mainPageData(obj._mainPageData, phase);
                    }
                }
            }
            private void Update__mainPageData(global::CrossWordPuzzle.ViewModel.MainPageData obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update__mainPageData_Definitions(obj.Definitions, phase);
                        this.Update__mainPageData_DisplayBoard(obj.DisplayBoard, phase);
                    }
                }
            }
            private void Update__mainPageData_Definitions(global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition> obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners__mainPageData_Definitions(obj);
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainPage.xaml line 111
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj6, obj, null);
                }
            }
            private void Update__mainPageData_DisplayBoard(global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>> obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners__mainPageData_DisplayBoard(obj);
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // MainPage.xaml line 96
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj8, obj, null);
                }
            }
            private void UpdateTwoWay_8_ItemsSource()
            {
                if (this.initialized)
                {
                    this.dataRoot._mainPageData.DisplayBoard = (global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>>)this.obj8.ItemsSource;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class MainPage_obj1_BindingsTracking
            {
                private global::System.WeakReference<MainPage_obj1_Bindings> weakRefToBindingObj; 

                public MainPage_obj1_BindingsTracking(MainPage_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<MainPage_obj1_Bindings>(obj);
                }

                public MainPage_obj1_Bindings TryGetBindingObject()
                {
                    MainPage_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners__mainPageData_Definitions(null);
                    UpdateChildListeners__mainPageData_DisplayBoard(null);
                }

                public void PropertyChanged__mainPageData_Definitions(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged__mainPageData_Definitions(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition>;
                    }
                }
                private global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition> cache__mainPageData_Definitions = null;
                public void UpdateChildListeners__mainPageData_Definitions(global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Definition> obj)
                {
                    if (obj != cache__mainPageData_Definitions)
                    {
                        if (cache__mainPageData_Definitions != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache__mainPageData_Definitions).PropertyChanged -= PropertyChanged__mainPageData_Definitions;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)cache__mainPageData_Definitions).CollectionChanged -= CollectionChanged__mainPageData_Definitions;
                            cache__mainPageData_Definitions = null;
                        }
                        if (obj != null)
                        {
                            cache__mainPageData_Definitions = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged__mainPageData_Definitions;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)obj).CollectionChanged += CollectionChanged__mainPageData_Definitions;
                        }
                    }
                }
                public void PropertyChanged__mainPageData_DisplayBoard(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>>;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                        }
                        else
                        {
                            switch (propName)
                            {
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void CollectionChanged__mainPageData_DisplayBoard(object sender, global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    MainPage_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>> obj = sender as global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>>;
                    }
                }
                private global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>> cache__mainPageData_DisplayBoard = null;
                public void UpdateChildListeners__mainPageData_DisplayBoard(global::System.Collections.ObjectModel.ObservableCollection<global::System.Collections.ObjectModel.ObservableCollection<global::CrossWordPuzzle.ViewModel.Cell>> obj)
                {
                    if (obj != cache__mainPageData_DisplayBoard)
                    {
                        if (cache__mainPageData_DisplayBoard != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache__mainPageData_DisplayBoard).PropertyChanged -= PropertyChanged__mainPageData_DisplayBoard;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)cache__mainPageData_DisplayBoard).CollectionChanged -= CollectionChanged__mainPageData_DisplayBoard;
                            cache__mainPageData_DisplayBoard = null;
                        }
                        if (obj != null)
                        {
                            cache__mainPageData_DisplayBoard = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged__mainPageData_DisplayBoard;
                            ((global::System.Collections.Specialized.INotifyCollectionChanged)obj).CollectionChanged += CollectionChanged__mainPageData_DisplayBoard;
                        }
                    }
                }
                public void RegisterTwoWayListener_8(global::Windows.UI.Xaml.Controls.ItemsControl sourceObject)
                {
                    sourceObject.RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.ItemsControl.ItemsSourceProperty, (sender, prop) =>
                    {
                        var bindingObj = this.TryGetBindingObject();
                        if (bindingObj != null)
                        {
                            bindingObj.UpdateTwoWay_8_ItemsSource();
                        }
                    });
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 15
                {
                    this.TextBoxStyle = (global::Windows.UI.Xaml.Style)(target);
                }
                break;
            case 7: // MainPage.xaml line 117
                {
                    this.ButtonPrintList = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ButtonPrintList).Click += this.ButtonPrintList_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

