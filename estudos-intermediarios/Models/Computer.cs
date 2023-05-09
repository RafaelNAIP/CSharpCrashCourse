using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estudos_intermediarios.Models
{
    internal class Computer
    {
        public int ComputerId { get; set; }
        public string Motherboard { get; set; }
        public int CPUCores;
        public bool hasWifi;
        public bool hasLTE;
        public DateTime ReleaseDate;
        public decimal Price;
        public string VideoCard;

        public Computer()
        {
            if (VideoCard == null)
            {
                VideoCard = string.Empty;
            }
            if (Motherboard == null)
            {
                Motherboard = string.Empty;
            }
        }
    }
}
