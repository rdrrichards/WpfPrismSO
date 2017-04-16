using Prism.Commands;
using System.Windows;
using Prism.Events;
using WpfPrismSO.Events;

namespace WpfPrismSO.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _name;
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            BoundCommand = new DelegateCommand<DragEventArgs>(DoWork);
            BoundCommand2 = new DelegateCommand(DoWork2);
            BoundCommand3 = new DelegateCommand(DoWork3);

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PollingEvent>().Subscribe(s => MVFieldToBindTo = s);
            //_eventAggregator.GetEvent<OrderSelectedEvent>().Subscribe(TestEventHandler);
        }

        private void TestEventHandler(object obj)
        {
            var ev = obj as OrderSelectedPayload;
            if (ev != null)
            {
                MVFieldToBindTo = "IT WORKED!";
            }
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

        //protected override void DoWorkBase()
        //{
        //    // base.DoWorkBase();
        //    MVFieldToBindTo = "From Concrete Class";

        //}

        public DelegateCommand<DragEventArgs> BoundCommand { get; set; }
        public DelegateCommand BoundCommand2 { get; set; }
        public DelegateCommand BoundCommand3 { get; set; }
        public string MVFieldToBindTo { get { return _name; } set { SetProperty(ref _name, value); } }
    }
}
