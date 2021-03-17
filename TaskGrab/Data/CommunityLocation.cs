using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGrab.Data
{
    public class CommunityLocation
    {
        public string Community { get; set; }
        public int ID { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
