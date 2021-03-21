using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskGrab.Pages.Task
{
    /// <summary>
    /// Interaction logic for TaskInfo.xaml
    /// </summary>
    public partial class TaskInfo : Page
    {
        int id = -1;
        int Id {
            get => id;
            set
            {
                IdHolder.Text = "The Id of this task right here is: " + value;
                id = value;
            }
        }
            
        public TaskInfo()
        {
            InitializeComponent();
            string current_path = ((MainWindow)Application.Current.MainWindow).GetHistory().current.OriginalString;;
            Match matches = Regex.Match(current_path, @".*\.xaml\?id=(.*)");
            if (matches.Success)
            {
                
                try
                { 
                    Id = int.Parse(matches.Groups[1].Captures[0].Value);

                } catch
                {
                    
                }

            } 
            

        }
    }
}
