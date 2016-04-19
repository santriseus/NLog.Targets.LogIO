using NLog.Common;
using NLog.Layouts;

namespace NLog.Targets.LogIO
{
    internal sealed class InternalNetworkTarget : NetworkTarget
    {
        public Layout Node { get; set; }

        public Layout Stream { get; set; }

        protected override byte[] GetBytesToWrite(LogEventInfo logEvent)
        {
            string message = "+log|" + Stream.Render(logEvent) + "|" + Node.Render(logEvent) + "|info|";
            
            var header = Encoding.GetBytes(message);

            var main = base.GetBytesToWrite(logEvent);

            var result = new byte[header.Length + main.Length];
            System.Buffer.BlockCopy(header, 0, result, 0, header.Length);
            System.Buffer.BlockCopy(main, 0, result, header.Length, main.Length);
            return result;
        }

        public void FlushAsync(AsyncContinuation asyncContinuation)
        {
            base.FlushAsync(asyncContinuation);
        }

        public void CloseTarget()
        {
            base.CloseTarget();
        }

        public void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
        }
    }
}
