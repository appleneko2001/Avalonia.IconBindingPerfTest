using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using Material.Icons;

namespace Avalonia.PackIconPerfTest
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Random _random;
        private Dictionary<int, MaterialIconKind> _kinds;
        
        public MainWindowViewModel()
        {
            _random = new Random();
            _test1Result = new TestElementViewModel()
            {
                Icon = MaterialIconKind.Null,
                Status = "Null"
            };
            _test2Result = new TestElementViewModel()
            {
                Icon = MaterialIconKind.Null,
                Status = "Null"
            };
            _kinds = new Dictionary<int, MaterialIconKind>();
            int i = 0;
            foreach (var kind in Enum.GetNames(typeof(MaterialIconKind)))
            {
                _kinds.Add(i, (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), kind));
                i++;
            }
        }

        private TestElementViewModel? _test1Result;
        public TestElementViewModel? Test1Result => _test1Result;
        
        private TestElementViewModel? _test2Result;
        public TestElementViewModel? Test2Result => _test2Result;

        private int _times = 10000;

        public int Times
        {
            get => _times;
            set
            {
                _times = value;
                OnPropertyChanged();
            }
        }

        private bool _canExecuteTest = true;

        public bool CanExecuteTest
        {
            get => _canExecuteTest;
            set
            {
                _canExecuteTest = value;
                OnPropertyChanged();
            }
        }

        public async void ExecuteTest(object param)
        {
            if(!_canExecuteTest)
                return;
            CanExecuteTest = false;

            var targets = new List<MaterialIconKind>();
            for (int i = 0; i < _times; i++)
            {
                targets.Add(_kinds[_random.Next(0, _kinds.Count)]);
            }
            
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Stopwatch timer = new Stopwatch();

                    TimeSpan GetResultAndReset()
                    {
                        timer.Stop();
                        var result = timer.Elapsed;
                        timer.Reset();
                        return result;
                    }
                    
                    // Start Procedures of tests
                    
                    Test1Result.Status = $"Measuring";
                    timer.Start();
                    for (int y = 0; y < targets.Count; y++)
                    {
                        Test1Result.Icon = targets[y];
                    }
                    var result = GetResultAndReset();
                    Test1Result.Status = $"{result.Ticks / 10000f} ms";
                    
                    Test2Result.Status = $"Measuring";
                    timer.Start();
                    for (int y = 0; y < targets.Count; y++)
                    {
                        Test2Result.Icon = targets[y];
                    }
                    result = GetResultAndReset();
                    Test2Result.Status =  $"{result.Ticks / 10000f} ms";
                    // End of tests
                }
                catch
                {
                    
                }

                CanExecuteTest = true;
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}