using Prism.Modularity;
using WpfPrismSO.Services;

namespace WpfPrismSO.Modules
{
    public class Module1 : IModule
    {
        private readonly IPoller _poller;

        public Module1(IPoller poller)
        {
            _poller = poller;
        }
        public void Initialize()
        {
            _poller.BeginPolling();
        }

        ~Module1()
        {
            _poller.EndPolling();
        }
    }
}
