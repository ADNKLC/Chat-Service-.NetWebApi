using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace butunlama_mesajlasma.ViewModels
{
    public class GrupUyeModel
    {
        public int grupUyeId { get; set; }
        public int uyeId { get; set; }
        public int grupId { get; set; }

        public UyeModel uyeBilgi { get; set; }
        public GrupModel grupBilgi { get; set; }
    }
}