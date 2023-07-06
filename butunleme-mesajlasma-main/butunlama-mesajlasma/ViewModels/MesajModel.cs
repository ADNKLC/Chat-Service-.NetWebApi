using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace butunlama_mesajlasma.ViewModels
{
    public class MesajModel
    {
        public int mesajId { get; set; }
        public string icerik { get; set; }
        public System.DateTime mesajTarihi { get; set; }
        public int gonderenId { get; set; }
        public string gonderenAdi { get; set; }
    }
}