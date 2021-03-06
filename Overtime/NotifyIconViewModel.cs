﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Overtime
{
    class NotifyIconViewModel
    {
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand StartTimer { get { return new DelegateCommand
        {
            CommandAction = () => ((App) Application.Current).Wmi.StartTimer(),
            CanExecFunc = () => ((App)Application.Current)?.Wmi.Started == false
        };} }
        public ICommand StopTimer
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => ((App)Application.Current).Wmi.StopTimer(),
                    CanExecFunc = () => ((App)Application.Current)?.Wmi.Started == true
                };
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }

        public string GetGameTime
        {
            get { return "Time: " + DateTime.Now.Minute; }
        }
    }

    class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecFunc == null || CanExecFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
