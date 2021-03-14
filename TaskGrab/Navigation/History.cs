using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaskGrab.Navigation
{
    public class History : INotifyPropertyChanged
    { 
        Stack<string> history;

        public String Current;

        public event PropertyChangedEventHandler PropertyChanged;

        public History(String initial)
        {
            history = new Stack<string>();
            history.Push(initial);
            this.Current = initial;
        }

    
        public bool GoTo(string path)
        {
            Uri uri = new(path, UriKind.Relative);
            return GoTo(uri);
        }

        public bool GoTo(Uri uri)
        {
            bool success = true;
            try
            {
                if ( uri.OriginalString != history.Peek())
                    history.Push(uri.OriginalString);  
            }
            catch (FileNotFoundException)
            {
                success = false;
            }
            return success;
        }

        public bool CanGoBack() {
            return history.Count > 1;
        }

        public void GoBack()
        {
            if (CanGoBack()) { 
                history.Pop();
                GoTo(history.Peek()); 
            }
        }
    }
}
