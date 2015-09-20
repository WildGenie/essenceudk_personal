﻿using EssenceUDK.Platform;
using EssenceUDK.PluginBase.ViewModels.DockableModels;
using GalaSoft.MvvmLight.Ioc;
using EssenceUDK.PluginBase.Models;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLandTile : ToolPaneViewModel
    {
        private IServiceModelLandData _data;

        /// <summary>
        /// Initializes a new instance of the ViewModelLandTile class.
        /// </summary>
        public ViewModelLandTile()
        {
        }

        [PreferredConstructor]
        public ViewModelLandTile(IServiceModelLandData data)
        {
            _data = data;

            _data.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    _selectedLand = ((ModelLandData)(item));
                });
        }

        private ModelLandData _selectedLand;

        public ModelLandData SelectedLand { get { return _selectedLand; } set { _selectedLand = value; RaisePropertyChanged(() => SelectedLand); } }
    }
}