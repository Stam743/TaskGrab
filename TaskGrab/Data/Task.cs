using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrab.Data
{
    public class Task
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string posted { get; set; }
        public string poster { get; set; }
        public string payment { get; set; }
        public string location { get; set; }
    }
}
