﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;

namespace Game_Time
{
    public class WmiEvent
    {
        private bool running = false;
        public ManagementEventWatcher StartWatcher { get; set; }
        public ManagementEventWatcher StopWatcher { get; set; }

        public event EventHandler GameStarted;
        public event EventHandler GameStopped;

        public WmiEvent()
        {
            StartWatcher = null;
            StopWatcher = null;
            try
            {
                WqlEventQuery startQuery = new WqlEventQuery {EventClassName = "Win32_ProcessStartTrace"};
                WqlEventQuery stopQuery = new WqlEventQuery {EventClassName = "Win32_ProcessStopTrace"};
                StartWatcher = new ManagementEventWatcher(startQuery);
                StopWatcher = new ManagementEventWatcher(stopQuery);
                StartWatcher.EventArrived += ProcessStartEventArrived;
                StopWatcher.EventArrived += ProcessStopEventArrived;
            }
            catch (Exception)
            {
                //todo
            }
        }

        public void Start()
        {
            if (!running)
            {
                StartWatcher.Start();
                StopWatcher.Start();
                running = true;
            }
        }

        public void Stop()
        {
            if (StartWatcher != null && StopWatcher != null && running)
            {
                StartWatcher.Stop();
                StopWatcher.Stop();
                running = false;
            }
        }

        public void ProcessStartEventArrived(object sender, EventArrivedEventArgs e)
        {
            string name = (string) e.NewEvent.Properties["ProcessName"].Value;
            if (name.Equals("Overwatch.exe"))
            {
                OnGameStarted();
                Console.WriteLine("Launched!");
            }
            //foreach (PropertyData property in e.NewEvent.Properties)
            //{
            //    Console.WriteLine("{0},{1},{2}", property.Name, property.Type, property.Value);
            //}
        }

        public void ProcessStopEventArrived(object sender, EventArrivedEventArgs e)
        {
            string name = (string) e.NewEvent.Properties["ProcessName"].Value;
            if (name.Equals("Overwatch.exe"))
            {
                OnGameStopped();
                Console.WriteLine("Closed!");
            }
        }

        protected virtual void OnGameStarted()
        {
            GameStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGameStopped()
        {
            GameStopped?.Invoke(this, EventArgs.Empty);
        }
    }
}