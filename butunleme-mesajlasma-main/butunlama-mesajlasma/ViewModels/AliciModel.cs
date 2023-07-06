using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace butunlama_mesajlasma.ViewModels
{
    public class AliciModel
    {
        public int aliciId { get; set; }
        public int uyeId { get; set; }
        public List<int> uyeIds { get; set; }
        public int mesajId { get; set; }

        public string uyeAdi { get; set; }
        public string mesaj { get; set; }
    }
}