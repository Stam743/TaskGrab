using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaskGrab.Navigation
{
    public class History : INotifyPropertyChanged
    { 
        Stack<Uri> history;
        public Uri current;
        private Frame frame;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private Visibility isBackVisible = Visibility.Hidden;

        public Visibility IsBackVisible {
            get { return this.isBackVisible; }
            set
            {
                this.isBackVisible= value;
                this.OnPropertyChanged();
            }
        }


        private Visibility isSwitchVisible = Visibility.Visible;
        public Visibility IsSwitchVisible
        {
            get { return this.isSwitchVisible; }
            set
            {
                this.isSwitchVisible = value;
                this.OnPropertyChanged();
            }
        }

        private string backBtnIcon = "\uf015;";
        public string BackBtnIcon
        {
            get { return this.backBtnIcon; }
            set
            {
                this.backBtnIcon = value;
                this.OnPropertyChanged();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public History(Frame frame, String initial)
        {
            history = new Stack<Uri>();
            Uri init = new Uri(initial, UriKind.Relative);
            current = init;
            this.frame = frame;
            this.frame.Source = init;
        }

    
        public bool GoTo(string path)
        {
            Uri uri = new(path, UriKind.Relative);
            return GoTo(uri);
        }

        public void updateVisibility()
        {
            IsBackVisible = CanGoBack() ? Visibility.Visible : Visibility.Hidden;
            IsSwitchVisible = Regex.Match(current.OriginalString,@".*MainView/.*").Success ? Visibility.Visible : Visibility.Hidden;
            if (history.Count > 0)
                BackBtnIcon = Regex.Match(history.Peek().OriginalString, @".*MainView/.*").Success ? "\uf015" : "\uf104";
        }

        public bool Replace(String path)
        {
            Uri uri = new(path, UriKind.Relative);
            return Replace(uri);
        }
        public bool Replace(Uri uri)
        {
            bool success = true;
            try
            {
                current = uri;
                frame.Source = current;
                updateVisibility();
            }
            catch (FileNotFoundException)
            {
                success = false;
            }
            return success;
        }
        public bool GoTo(Uri uri)
        {
            bool success = true;
            try
            {
                if ( history.Count() > 0 && uri.OriginalString == history.Peek().OriginalString )
                {
                    GoBack();
                    updateVisibility();
                    return true;
                }

                history.Push(current);
                current = uri;
                frame.Source = current;
                updateVisibility();
                ((MainWindow)Application.Current.MainWindow).closeMenu();
            }
            catch (FileNotFoundException)
            {
                success = false;
            }
            return success;
        }

        public bool CanGoBack() {
            return history.Count > 0;
        }

        public void GoBack()
        {
            if (CanGoBack()) {
                Uri previous = history.Pop();
                frame.Source = previous;
                current = previous;
                updateVisibility();
            }
        }
    }
}
