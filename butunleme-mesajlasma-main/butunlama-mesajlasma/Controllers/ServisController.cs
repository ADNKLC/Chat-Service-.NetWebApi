using butunlama_mesajlasma.Models;
using butunlama_mesajlasma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace butunlama_mesajlasma.Controllers
{
    public class ServisController : ApiController
    {
        Entities db = new Entities();
        SonucModel sonuc = new SonucModel();

        #region Uye

        [HttpGet]
        [Route("api/uyelistele")]
        public List<UyeModel> UyeListele()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdi = x.uyeAdi,
                email = x.email,
                sifre = x.sifre,
                telefon = x.telefon
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel uye = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdi = x.uyeAdi,
                email = x.email,
                sifre = x.sifre,
                telefon = x.telefon

            }).FirstOrDefault();
            return uye;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.uyeAdi == model.uyeAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Kayıtlıdır";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.uyeAdi = model.uyeAdi;
            yeni.email = model.email;
            yeni.sifre = model.sifre;
            yeni.telefon = model.telefon;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }

            kayit.uyeAdi = model.uyeAdi;
            kayit.email = model.email;
            kayit.sifre = model.sifre;
            kayit.telefon = model.telefon;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Bilgileri Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }

            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }

        #endregion

        #region Grup

        [HttpGet]
        [Route("api/gruplistele")]
        public List<GrupModel> GrupListele()
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAdi = x.grupAdi
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupbyid/{grupId}")]
        public GrupModel GrupById(int grupId)
        {
            GrupModel grup = db.Grup.Where(s => s.grupId == grupId).Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAdi = x.grupAdi
            }).FirstOrDefault();
            return grup;
        }

        [HttpPost]
        [Route("api/grupekle")]
        public SonucModel GrupEkle(GrupModel model)
        {
            Grup yeni = new Grup();
            yeni.grupAdi = model.grupAdi;
            db.Grup.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Grup Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupduzenle")]
        public SonucModel GrupDuzenle(GrupModel model)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == model.grupId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }

            kayit.grupAdi = model.grupAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Bilgileri Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/grupsil/{grupId}")]
        public SonucModel GrupSil(int grupId)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == grupId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }

            db.Grup.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";
            return sonuc;
        }

        #endregion

        #region GrupUye

        [HttpGet]
        [Route("api/grupuyelistele")]
        public List<GrupUyeModel> GrupUyeListele()
        {
            List<GrupUyeModel> liste = db.GrupUye.Select(x => new GrupUyeModel()
            {
                grupUyeId = x.grupUyeId,
                uyeId = x.uyeId,
                grupId = x.grupId,
                uyeBilgi = db.Uye.Where(u => u.uyeId == x.uyeId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    uyeAdi = u.uyeAdi,
                    email = u.email,
                    sifre = u.sifre
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    grupAdi = g.grupAdi
                }).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupuyebyid/{grupUyeId}")]
        public GrupUyeModel GrupUyeById(int grupUyeId)
        {
            GrupUyeModel grupUye = db.GrupUye.Where(s => s.grupUyeId == grupUyeId).Select(x => new GrupUyeModel()
            {
                grupUyeId = x.grupUyeId,
                uyeId = x.uyeId,
                grupId = x.grupId,
                uyeBilgi = db.Uye.Where(u => u.uyeId == x.uyeId).Select(u => new UyeModel()
                {
                    uyeId = u.uyeId,
                    uyeAdi = u.uyeAdi,
                    email = u.email,
                    sifre = u.sifre
                }).FirstOrDefault(),
                grupBilgi = db.Grup.Where(g => g.grupId == x.grupId).Select(g => new GrupModel()
                {
                    grupId = g.grupId,
                    grupAdi = g.grupAdi
                }).FirstOrDefault()
            }).FirstOrDefault();
            return grupUye;
        }

        [HttpPost]
        [Route("api/grupuyeolustur")]
        public SonucModel GrupUyeOlustur(GrupUyeModel model)
        {
            GrupUye yeni = new GrupUye();
            yeni.uyeId = model.uyeId;
            yeni.grupId = model.grupId;
            db.GrupUye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Grup Oluşuturldu";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupuyeduzenle")]
        public SonucModel GrupUyeDuzenle(GrupUyeModel model)
        {
            GrupUye kayit = db.GrupUye.Where(s => s.grupUyeId == model.grupUyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }

            kayit.uyeId = model.uyeId;
            kayit.grupId = model.grupId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/grupuyesil/{grupUyeId}")]
        public SonucModel GrupUyeSil(int grupUyeId)
        {
            GrupUye kayit = db.GrupUye.Where(s => s.grupUyeId == grupUyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunamadı";
                return sonuc;
            }

            db.GrupUye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";
            return sonuc;
        }

        #endregion

        #region GrupMesaj

        [HttpGet]
        [Route("api/grupmesajlistele")]
        public List<GrupMesajModel> GrupMesajListele()
        {
            List<GrupMesajModel> liste = db.GrupMesaj.Select(x => new GrupMesajModel()
            {
                grupMesajId = x.grupMesajId,
                mesajId = x.mesajId,
                grupId = x.grupId,
                grupAdi = x.Grup.grupAdi,
                mesaj = x.Mesaj.icerik
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupmesajbyid/{grupMesajId}")]
        public GrupMesajModel GrupMesajById(int grupMesajId)
        {
            GrupMesajModel grupMesaj = db.GrupMesaj.Where(s => s.grupMesajId == grupMesajId).Select(x => new GrupMesajModel()
            {
                grupMesajId = x.grupMesajId,
                mesajId = x.mesajId,
                grupId = x.grupId,
                grupAdi = x.Grup.grupAdi,
                mesaj = x.Mesaj.icerik
            }).FirstOrDefault();
            return grupMesaj;
        }

        [HttpPost]
        [Route("api/grupmesajekle")]
        public SonucModel GrupMesajEkle(GrupMesajModel model)
        {
            GrupMesaj yeni = new GrupMesaj();
            yeni.mesajId = model.mesajId;
            yeni.grupId = model.grupId;
            db.GrupMesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesajı Gönderildi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupmesajduzenle")]
        public SonucModel GrupMesajDuzenle(GrupMesajModel model)
        {
            GrupMesaj kayit = db.GrupMesaj.Where(s => s.grupMesajId == model.grupMesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            kayit.mesajId = model.mesajId;
            kayit.grupId = model.grupId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesajı Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/grupmesajsil/{grupMesajId}")]
        public SonucModel GrupMesajSil(int grupMesajId)
        {
            GrupMesaj kayit = db.GrupMesaj.Where(s => s.grupMesajId == grupMesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            db.GrupMesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesajı Silindi";
            return sonuc;
        }

        #endregion

        #region Mesaj

        [HttpGet]
        [Route("api/mesajlistele")]
        public List<MesajModel> MesajListele()
        {
            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                gonderenId = x.gonderenId,
                gonderenAdi = db.Uye.Where(u => u.uyeId == x.gonderenId).Select(u => u.uyeAdi).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/mesajbyid/{mesajId}")]
        public MesajModel MesajById(int mesajId)
        {
            MesajModel mesaj = db.Mesaj.Where(s => s.mesajId == mesajId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                gonderenId = x.gonderenId,
                gonderenAdi = db.Uye.Where(u => u.uyeId == x.gonderenId).Select(u => u.uyeAdi).FirstOrDefault()
            }).FirstOrDefault();
            return mesaj;
        }

        [HttpGet]
        [Route("api/mesajlistelebygonderen/{gonderenId}")]
        public List<MesajModel> MesajListeleByGonderen(int gonderenId)
        {
            List<MesajModel> liste = db.Mesaj.Where(s => s.gonderenId == gonderenId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                icerik = x.icerik,
                mesajTarihi = x.mesajTarihi,
                gonderenId = x.gonderenId,
                gonderenAdi = db.Uye.Where(u => u.uyeId == x.gonderenId).Select(u => u.uyeAdi).FirstOrDefault()
            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {
            Mesaj yeni = new Mesaj();
            yeni.icerik = model.icerik;
            yeni.mesajTarihi = model.mesajTarihi;
            yeni.gonderenId = model.gonderenId;
            db.Mesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Mesaj Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/mesajduzenle")]
        public SonucModel MesajDuzenle(MesajModel model)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == model.mesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            kayit.icerik = model.icerik;
            kayit.mesajTarihi = model.mesajTarihi;
            kayit.gonderenId = model.gonderenId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int mesajId)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == mesajId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunamadı";
                return sonuc;
            }

            db.Mesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }

        #endregion

        #region Alici

        [HttpGet]
        [Route("api/alicilistele")]
        public List<AliciModel> AliciListele()
        {
            List<AliciModel> liste = db.Alici.Select(x => new AliciModel()
            {
                aliciId = x.aliciId,
                uyeId = x.uyeId,
                uyeIds = db.Alici.Where(a => a.aliciId == x.aliciId).Select(a => a.uyeId).ToList(),
                mesajId = x.mesajId,
                uyeAdi = x.Uye.uyeAdi,
                mesaj = x.Mesaj.icerik
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/alicibyid/{aliciId}")]
        public AliciModel AliciById(int aliciId)
        {
            AliciModel alici = db.Alici.Where(s => s.aliciId == aliciId).Select(x => new AliciModel()
            {
                aliciId = x.aliciId,
                uyeId = x.uyeId,
                uyeIds = db.Alici.Where(a => a.aliciId == x.aliciId).Select(a => a.uyeId).ToList(),
                mesajId = x.mesajId,
                uyeAdi = x.Uye.uyeAdi,
                mesaj = x.Mesaj.icerik
            }).FirstOrDefault();
            return alici;
        }

        [HttpPost]
        [Route("api/aliciekle")]
        public SonucModel AliciEkle(AliciModel model)
        {
            foreach (int uyeId in model.uyeIds)
            {
                Alici yeni = new Alici();
                yeni.uyeId = uyeId;
                yeni.mesajId = model.mesajId;
                db.Alici.Add(yeni);
            }

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Gönderildi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/aliciduzenle")]
        public SonucModel AliciDuzenle(AliciModel model)
        {
            Alici kayit = db.Alici.Where(s => s.aliciId == model.aliciId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Alıcı Bulunamadı";
                return sonuc;
            }

            kayit.uyeId = model.uyeId;
            kayit.mesajId = model.mesajId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/alicisil/{aliciId}")]
        public SonucModel AliciSil(int aliciId)
        {
            Alici kayit = db.Alici.Where(s => s.aliciId == aliciId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Alıcı Bulunamadı";
                return sonuc;
            }

            db.Alici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }

        #endregion


    }
}
