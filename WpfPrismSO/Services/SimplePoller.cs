using Prism.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using WpfPrismSO.Core;
using WpfPrismSO.Events;

namespace WpfPrismSO.Services
{
    public class SimplePoller : IPoller
    {
        private readonly IEventAggregator _eventAggregator;
        int delay = 5000;
        CancellationTokenSource cancellationTokenSource = null;

        public SimplePoller(IEventAggregator eventAggregator)
        {
            //BeginPolling();
            _eventAggregator = eventAggregator;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void BeginPolling()
        {
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    //poll HW
                    Log.Info($"Polling...");
                    _eventAggregator.GetEvent<PollingEvent>().Publish($"Polled @ {DateTime.Now}");
                    DoSomethingTricky();
                    Thread.Sleep(delay);
                    if (token.IsCancellationRequested)
                        break;
                }
                
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void DoSomethingTricky()
        {
            var ev = new OrderSelectedPayload { SelectedOrder = "Order", SelectedE32 = "Job" };
            _eventAggregator.GetEvent<OrderSelectedEvent>().Publish(ev);
        }

        public void EndPolling()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }

    public interface IPoller {
        void BeginPolling();
        void EndPolling();
    }
}
