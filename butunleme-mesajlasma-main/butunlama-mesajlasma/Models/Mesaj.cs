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
    
    public partial class Mesaj
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mesaj()
        {
            this.Alici = new HashSet<Alici>();
            this.GrupMesaj = new HashSet<GrupMesaj>();
        }
    
        public int mesajId { get; set; }
        public string icerik { get; set; }
        public System.DateTime mesajTarihi { get; set; }
        public int gonderenId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alici> Alici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GrupMesaj> GrupMesaj { get; set; }
        public virtual Uye Uye { get; set; }
    }
}
