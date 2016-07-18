#region Usings

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security;
using System.ServiceProcess;
using System.Timers;
using System.Xml;
using EobotService.Properties;
using NotificationsExtensions.Tiles;

#endregion

namespace EobotService
{
    public partial class EobotService : ServiceBase
    {
        private readonly Timer _timer = new Timer();

        public EobotService()
        {
            InitializeComponent();
            eventLog1 = new EventLog {Source = "EobotLog", Log = "EobotLogging"};
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry(String.Format(Resources.Eobot_Service_Started, DateTime.Now));
            // Set up a timer to trigger every minute.

            _timer.Interval = 60000; // 60 seconds
            _timer.Elapsed += OnTimer;
            _timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var bindingContent = new TileBindingContentAdaptive()
            {
                PeekImage = new TilePeekImage()
                {
                    Source = Images.Images.Ukyuu.ToString()
                },

                Children =
    {
        new TileText()
        {
            Text = "Notifications Extensions",
            Style = TileTextStyle.Body
        },

        new TileText()
        {
            Text = "Generate notifications easily!",
            Wrap = true,
            Style = TileTextStyle.CaptionSubtle
        }
    }
            };
        }
        protected override void OnStop()
        {
            _timer.Stop();
        }
    }
}