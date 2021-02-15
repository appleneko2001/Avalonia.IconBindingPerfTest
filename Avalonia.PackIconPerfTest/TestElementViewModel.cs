using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Material.Icons;

namespace Avalonia.PackIconPerfTest
{
    public class TestElementViewModel : INotifyPropertyChanged
    {
        private MaterialIconKind? _icon;

        public MaterialIconKind? Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        private string? _status;
        
        public string? Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}