﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EssenceUDKMVVM.Controls.MapMaker.TextureManager
{
    /// <summary>
    /// Interaction logic for TextureTransitionEditor.xaml
    /// </summary>
    public partial class TextureTransitionEditorList : UserControl
    {
        public TextureTransitionEditorList()
        {
            InitializeComponent();
        }


        /// <summary>
        /// The <see cref="SelectedItem" /> dependency property's name.
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        /// <summary>
        /// Gets or sets the value of the <see cref="SelectedItem" />
        /// property. This is a dependency property.
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="SelectedItem" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            SelectedItemPropertyName,
            typeof(object),
            typeof(TextureTransitionEditorList),
            new UIPropertyMetadata(null));


        /// <summary>
        /// The <see cref="CommandClone" /> dependency property's name.
        /// </summary>
        public const string CommandClonePropertyName = "CommandClone";

        /// <summary>
        /// Gets or sets the value of the <see cref="CommandClone" />
        /// property. This is a dependency property.
        /// </summary>
        public ICommand CommandClone
        {
            get
            {
                return (ICommand)GetValue(CommandCloneProperty);
            }
            set
            {
                SetValue(CommandCloneProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CommandClone" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandCloneProperty = DependencyProperty.Register(
            CommandClonePropertyName,
            typeof(ICommand),
            typeof(TextureTransitionEditorList),
            new UIPropertyMetadata(null));

        /// <summary>
        /// The <see cref="CommandAdd" /> dependency property's name.
        /// </summary>
        public const string CommandAddPropertyName = "CommandAdd";

        /// <summary>
        /// Gets or sets the value of the <see cref="CommandAdd" />
        /// property. This is a dependency property.
        /// </summary>
        public ICommand CommandAdd
        {
            get
            {
                return (ICommand)GetValue(CommandAddProperty);
            }
            set
            {
                SetValue(CommandAddProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CommandAdd" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandAddProperty = DependencyProperty.Register(
            CommandAddPropertyName,
            typeof(ICommand),
            typeof(TextureTransitionEditorList),
            new UIPropertyMetadata(null));


        /// <summary>
        /// The <see cref="CommandDelete" /> dependency property's name.
        /// </summary>
        public const string CommandDeletePropertyName = "CommandDelete";

        /// <summary>
        /// Gets or sets the value of the <see cref="CommandDelete" />
        /// property. This is a dependency property.
        /// </summary>
        public ICommand CommandDelete
        {
            get
            {
                return (ICommand)GetValue(CommandDeleteProperty);
            }
            set
            {
                SetValue(CommandDeleteProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CommandDelete" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandDeleteProperty = DependencyProperty.Register(
            CommandDeletePropertyName,
            typeof(ICommand),
            typeof(TextureTransitionEditorList),
            new UIPropertyMetadata(null));


    }
}
