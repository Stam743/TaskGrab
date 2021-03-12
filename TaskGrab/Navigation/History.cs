using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TaskGrab.Navigation
{
    public class History
    {
        Stack<string> history;
        Frame frame;
        public History(Frame frame, String initial)
        {
            history = new Stack<string>();
            history.Push(initial);
            this.frame = frame;
            this.frame.Source = new Uri(initial, UriKind.RelativeOrAbsolute);
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
                frame.Source = uri;
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
