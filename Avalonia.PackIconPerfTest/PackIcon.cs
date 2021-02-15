using System;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Material.Icons;

namespace Avalonia.PackIconPerfTest
{
    public class PackIcon : TemplatedControl
    {  
        internal static readonly Lazy<IDictionary<MaterialIconKind, string>> _dataIndex = new(MaterialIconDataFactory.DataSetCreate);
        
        static PackIcon()
        { 
        }

        public static readonly StyledProperty<MaterialIconKind> KindProperty
            = AvaloniaProperty.Register<PackIcon, MaterialIconKind>(nameof(Kind), notifying: KindPropertyChangedCallback);

        private static void KindPropertyChangedCallback(IAvaloniaObject sender, bool before)
        {
            ((PackIcon)sender).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind
        {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly StyledProperty<StreamGeometry> DataProperty
            = AvaloniaProperty.Register<PackIcon, StreamGeometry>(nameof(Data));

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary> 
        public StreamGeometry Data
        {
            get => GetValue(DataProperty);
            private set => SetValue(DataProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        } 

        private void UpdateData()
        {
            string data = null; 
            _dataIndex.Value?.TryGetValue(Kind, out data);
            var g = StreamGeometry.Parse(data);
            this.Data = g;
        }
    }
}