﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EssenceUDK.Controls.Ultima
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EssenceUDK.Controls.Ultima"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EssenceUDK.Controls.Ultima;assembly=EssenceUDK.Controls.Ultima"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ListBoxViewTiles/>
    ///
    /// </summary>
    public class ListBoxViewTiles : ListView
    {
        static ListBoxViewTiles()
        {
            if (!WpfHelper.IsInDesignMode)
                DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxViewTiles), new FrameworkPropertyMetadata(typeof(ListBoxViewTiles)));
	      

        }

        public ListBoxViewTiles() : base()
        {
			//ItemsSource = new System.Collections.ObjectModel.ObservableCollection<EssenceUDK.Platform.UOBaseViewModel>();
        }

        public bool Texture
        {
            get { return (bool)GetValue(TextureProperty); }
            set { SetValue(TextureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Texture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextureProperty =
            DependencyProperty.Register("Texture", typeof(bool), typeof(ListBoxViewTiles), new UIPropertyMetadata(false));

       

        #region DependencyProperty Content

        /// <summary>
        /// Registers a dependency property as backing store for the Content property
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ListBoxViewTiles),
            new FrameworkPropertyMetadata(null,
                  FrameworkPropertyMetadataOptions.AffectsRender |
                  FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        /// <value>The Content.</value>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        #endregion



    }
}
