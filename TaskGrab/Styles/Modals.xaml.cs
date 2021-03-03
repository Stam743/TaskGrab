using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskGrab.Styles
{
    public partial class Modals : ResourceDictionary
    {
        public void BtnCloseModal_Click(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).ModalPopup.IsOpen = false;
                }
            }
        }

    }
}
