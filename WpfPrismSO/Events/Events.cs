using Prism.Events;

namespace WpfPrismSO.Events
{
    public class PollingEvent : PubSubEvent<string> { }
    internal class OrderSelectedEvent : PubSubEvent<object> { }
}
