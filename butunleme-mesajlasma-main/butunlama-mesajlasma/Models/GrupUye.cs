//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace butunlama_mesajlasma.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GrupUye
    {
        public int grupUyeId { get; set; }
        public int uyeId { get; set; }
        public int grupId { get; set; }
    
        public virtual Grup Grup { get; set; }
        public virtual Uye Uye { get; set; }
    }
}
