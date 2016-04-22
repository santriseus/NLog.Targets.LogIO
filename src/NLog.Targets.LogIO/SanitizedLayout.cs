using System;
using System.Text;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets.LogIO
{
    internal class SanitizedLayout : Layout
    {
        private readonly Layout _layout;

        public SanitizedLayout(Layout layout)
        {
            _layout = layout;
        }

        protected override string GetFormattedMessage(LogEventInfo logEvent)
        {
            var builder = new StringBuilder(_layout.Render(logEvent));
            builder.Replace(Environment.NewLine, " ");
            builder.Replace("\r\n", " ");
            return builder.ToString();
        }
    }
}