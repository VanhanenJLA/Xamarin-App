﻿using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace DesignApp.Core
{
    static class ViewModelLocator
    {
        #region Autowiring
        public static readonly BindableProperty AutoWireViewModelProperty
                            = BindableProperty.CreateAttached(
                                "AutoWireViewModel",
                                typeof(bool),
                                typeof(ViewModelLocator),
                                default(bool),
                                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
                return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
                return;

            var viewModel = App.Container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
        #endregion
    }

}
