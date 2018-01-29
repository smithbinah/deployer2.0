using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace Deployer2._0.Models
{
    public class VirtualMachineModel
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public string OperatingSystem { get; set; }
        public int Memory { get; set; }
        public int CPUs { get; set; }


        public override string ToString()
        {
            return 
                "Name: " + Name +
                "\nDescription: " + Description +
                "\nIP: " + IPAddress +
                "\nOS: " + OperatingSystem +
                "\nMemory: " + Memory + "GB" +
                "\nCPUs: " + CPUs
                ;
        }
    }
}