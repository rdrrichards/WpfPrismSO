using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System;
using Prism.Events;
using WpfPrismSO.Events;

namespace WpfPrismSO.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _name;
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            BoundCommand = new DelegateCommand<DragEventArgs>(DoWork);
            BoundCommand2 = new DelegateCommand(DoWork2);
            BoundCommand3 = new DelegateCommand(DoWork3);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PollingEvent>().Subscribe(s => MVFieldToBindTo = s);
        }

        private void DoWork3()
        {
            MVFieldToBindTo = "Cum Bubbles!!";
        }

        private void DoWork2()
        {
            MVFieldToBindTo = "Bubble Yum!!";
        }

        private void DoWork(DragEventArgs e)
        {
            MVFieldToBindTo = "Juicy Friut!";
        }

        public DelegateCommand<DragEventArgs> BoundCommand { get; set; }
        public DelegateCommand BoundCommand2 { get; set; }
        public DelegateCommand BoundCommand3 { get; set; }
        public string MVFieldToBindTo { get { return _name; } set { SetProperty(ref _name, value); } }
    }
}
