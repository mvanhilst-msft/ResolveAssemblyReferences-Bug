using Microsoft.Diagnostics.Tracing;

namespace ProjectA
{
    public class ClassA
    {
        public static void DoThing()
        {
            DemoEventSource.Log.AppStarted("Hello World!", 12);
        }
    }

    [EventSource(Name = "Demo")]
    sealed class DemoEventSource : EventSource
    {
        public static DemoEventSource Log { get; } = new DemoEventSource();

        [Event(1)]
        public void AppStarted(string message, int favoriteNumber) => WriteEvent(1, message, favoriteNumber);
    }
}
