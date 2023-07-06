using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace butunlama_mesajlasma.ViewModels
{
    public class GrupMesajModel
    {
        public int grupMesajId { get; set; }
        public int mesajId { get; set; }
        public int grupId { get; set; }

        public string mesaj { get; set; }
        public string grupAdi { get; set; }
    }
}