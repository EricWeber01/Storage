using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    class Program
    {
        static void Main(string[] args)
        {
            PC pc = new PC(565);
            pc.AddDevice(new HDD(10, 50));
            pc.AddDevice(new USB(64));
            pc.AddDevice(new DVD(1));
            pc.GetInfo();
            pc.StartCopy();
            pc.GetInfo();
        }
    }
}
