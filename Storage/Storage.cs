using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    abstract class Storage
    {
        public string Name;
        protected string Model;

        public double sec = 0;
        public abstract void GetInfo();
        public abstract int CopyInfo(int _GB);
    }
    class USB : Storage
    {
        int Mem;
        int freeMem;
        int Speed;
        public USB(int _m)
        {
            Name = "USB flash";
            Model = "Kingston";
            Speed = 5000;
            Mem = _m * 1000;
            freeMem = _m * 1000;
        }
        public override int CopyInfo(int _GB)
        {

            if (_GB >= freeMem)
            {
                sec = (double)(Mem / Speed);
                _GB -= Mem;
                freeMem = 0;
            }
            else if (freeMem != 0 && _GB < freeMem)
            {
                freeMem -= _GB;

                sec = (double)(_GB / Speed);
                _GB = 0;
            }
            return _GB;
        }

        public override void GetInfo()
        {
            Console.WriteLine($"Type: {Name}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Memmory Size: {Mem / 1000} Gb.");
            Console.WriteLine($"Free Memmory: {freeMem / 1000} Gb.");
            Console.WriteLine($"Speed(USB 3.0): {Speed} Mb/Sec.");
        }
    }
    class DVD : Storage
    {
        int Mem;
        int freeMem;
        int Speed;
        public DVD(int t)
        {
            Name = "DVD disk";
            Model = "Verbatim";
            Speed = 7;
            if (t == 1)
                Mem = 4700;
            else if (t == 2)
                Mem = 9000;

            freeMem = Mem;

        }
        public override int CopyInfo(int _GB)
        {

            if (_GB >= freeMem)
            {
                sec = (double)(Mem / Speed);
                _GB -= Mem;
                freeMem = 0;
            }
            else if (freeMem != 0 && _GB < freeMem)
            {
                freeMem -= _GB;
                sec = (double)(_GB / Speed);
                _GB = 0;
            }

            return _GB;
        }
        public override void GetInfo()
        {
            Console.WriteLine($"Type: {Name}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Memmory Size: {Mem / 1000} Gb.");
            Console.WriteLine($"Free Memmory: {freeMem / 1000} Gb.");
            Console.WriteLine($"Speed(DVD): {Speed} Mb/Sec.");

        }
    }
    class HDD : Storage
    {
        int Mem;
        int freeMem;
        int Speed;
        int part, partMem;
        public HDD(int _p, int _pm)
        {
            Name = "HDD disk";
            Model = "Western Digital";
            Speed = 480;
            part = _p;
            partMem = _pm * 1000;
            Mem = part * partMem;
            freeMem = Mem;

        }
        public override int CopyInfo(int _GB)
        {

            if (_GB >= freeMem)
            {
                sec = (double)(Mem / Speed);
                _GB -= Mem;
                freeMem = 0;
                part = 0;
            }
            else if (freeMem != 0 && _GB < freeMem)
            {
                freeMem -= _GB;
                part = _GB / partMem;
                sec = (double)(_GB / Speed);
                _GB = 0;
            }

            return _GB;
        }
        public override void GetInfo()
        {
            Console.WriteLine($"Type: {Name}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Memmory Size: {Mem / 1000} Gb.");
            Console.WriteLine($"Free Memmory: {freeMem / 1000} Gb.");
            Console.WriteLine($"Number of free parts: {part}.");
            Console.WriteLine($"Part memory: {partMem / 1000} Gb.");
            Console.WriteLine($"Speed(DVD): {Speed} Mb/Sec.");
        }
    }
    class PC
    {
        int File;
        double Time = 0;
        List<Storage> storage;
        public PC(int _f)
        {
            storage = new List<Storage>();
            File = _f * 1000;
        }
        public void AddDevice(Storage s)
        {
            storage.Add(s);
        }
        public void StartCopy()
        {
            Console.WriteLine("\n----------------------\n");
            foreach (var item in storage)
            {
                if (File > 0)
                {
                    File = item.CopyInfo(File);
                    Time += item.sec;
                    Console.WriteLine($"{item.Name} write files for {item.sec} sec.");
                }
                else
                    break;
            }
            if (File <= 0)
            {
                Console.WriteLine($"All files has been copied!\nTime spent: {Time} sec.\n");
            }
            else
            {
                Console.WriteLine($"NOT all files has been copied!\nTime spent: {Time} sec.\nFiles left: {File / 1000} Gb.");
            }
            Console.WriteLine("\n----------------------\n");
        }
        public void GetInfo()
        {
            Console.WriteLine($"PC Files: {File / 1000} Gb.\nYour Devices");
            foreach (var item in storage)
            {
                Console.WriteLine("\n----------------------\n");
                item.GetInfo();
            }
        }
    }
}