using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets.LogIO
{
    [Target("LogIO")]
    public class LogIOTarget : TargetWithLayout
    {
        private readonly InternalNetworkTarget _networkTarget;

        /// <summary>
        /// Gets or sets the layout used to format log messages.
        /// </summary>
        [RequiredParameter]
        [DefaultValue("[${longdate}] ${message}")]
        public override Layout Layout { get; set; }
        
        public LogIOTarget()
        {
            this.Host = "127.0.0.1";
            this.Port = 28777;
            this.MaxQueueSize = 0;
            this.OnOverflow = NetworkTargetOverflowAction.Split;
            this.MaxMessageSize = 65000;
            this.Layout = "[${longdate}] ${message}";
            _networkTarget = new InternalNetworkTarget();
        }

        /// <summary>
        /// Gets or sets the Host name for tcp connection to log.io
        /// </summary>
        [DefaultValue("127.0.0.1")]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port for tcp connection to log.io
        /// </summary>
        [DefaultValue(28777)]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the log.io node value. The first rendered Layout will be cached.
        /// </summary>
        public Layout Node { get; set; }

        /// <summary>
        /// Gets or sets the log.io stream value
        /// </summary>
        public Layout Stream { get; set; }

        /// <summary>
        /// Gets or sets the maximum queue size.
        /// </summary>
        [DefaultValue(0)]
        public int MaxQueueSize { get; set; }

        /// <summary>
        /// Gets or sets the action that should be taken if the message is larger than
        /// maxMessageSize.
        /// </summary>
        public NetworkTargetOverflowAction OnOverflow { get; set; }

        /// <summary>
        /// Gets or sets the maximum message size in bytes.
        /// </summary>
        [DefaultValue(65000)]
        public int MaxMessageSize { get; set; }

        protected override void CloseTarget()
        {
            base.CloseTarget();
            _networkTarget.CloseTarget();

        }

        protected override void FlushAsync(AsyncContinuation asyncContinuation)
        {
            _networkTarget.FlushAsync(asyncContinuation);
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            _networkTarget.Write(logEvent);
        }

        protected override void InitializeTarget()
        {
            _networkTarget.Address = string.Format("tcp://{0}:{1}", Host, Port);
            _networkTarget.NewLine = true;
            _networkTarget.Encoding = Encoding.ASCII;
            _networkTarget.Node = Node;
            _networkTarget.Stream = Stream;
            _networkTarget.MaxQueueSize = MaxQueueSize;
            _networkTarget.OnOverflow = OnOverflow;
            _networkTarget.Layout = new SanitizedLayout(Layout);
            base.InitializeTarget();
        }


    }
}
