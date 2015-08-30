﻿using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EssenceUDKMVVM.Converters
{
    public class ConveterRenderModelToImage : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var renderModel = (Models.Model.RenderModel)value;

            if (renderModel == null) return null;

            var datamanager = ServiceLocator.Current.GetInstance<ViewModelLocator>().UoDataManager.UoDataManager;

            if (datamanager == null) return null;

            var surf = datamanager.CreateSurface(renderModel.Width, renderModel.Height, PixelFormat.Bpp16X1R5G5B5);

            if (renderModel.Flat)
                datamanager.FacetRender.DrawFlatMap(renderModel.Map, renderModel.SeaLevel, ref surf,
                    renderModel.Range, renderModel.X, renderModel.Y, renderModel.MinZ, renderModel.MaxZ);
            else
                datamanager.FacetRender.DrawObliqueMap(renderModel.Map, renderModel.SeaLevel, ref surf,
                    renderModel.Range, renderModel.X, renderModel.Y, renderModel.MinZ, renderModel.MaxZ);

            return surf?.GetSurface().Image;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   

    public class ConverterInvertVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visibility = (Visibility)value;

            switch (visibility)
            {
                case Visibility.Visible:
                    return Visibility.Hidden;

                case Visibility.Hidden:
                    return Visibility.Visible;

                case Visibility.Collapsed:
                    return Visibility.Visible;

                default:
                    return visibility;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}