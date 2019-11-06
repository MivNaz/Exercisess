using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_141019.Models;

namespace CRUD_141019.Controllers
{
    public class KisilersController : Controller
    {
        private KisilerEntities db = new KisilerEntities();

        // GET: Kisilers
        public ActionResult Index()
        {
            return View(db.Table.ToList());  //Index Action’ı kişilerin listesini çekiyor. Bu örnek için konuşacak olursa veritabanında                                      kayıtlı olan kişilerin listesini ekrana getirir.
        }

        // GET: Kisilers/Details/5
        public ActionResult Details(int? id)  //Details Action’ı ID ye göre kişinin detayını getiriyor. Yani Index ile gelen listede herhangi                                     bir satırın yanından Detail butonuna tıkladığımız zaman o kişinin detayına gider.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Table.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Kisilers/Create
        public ActionResult Create()  //Create Action’ u yeni kişi oluşturuyor.
        {
            return View();
        }

        // POST: Kisilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Soyadi,Telefon,Yas")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Table.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }

        // GET: Kisilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Table.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);//Viewlere gelecek olursak Controllerda ki Actionların karşılığı olan Viewlarda burada oluşturulmuş durumda.
        }

        // POST: Kisilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Soyadi,Telefon,Yas")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        //Gelelim Edit Action’una, gördüğünüz gibi iki tane Edit Action u  var . Bunlardan ilki Edit butonuna tıklandığında açılan sayfanın Controller’ıdır.  Aldığı ID ye ait kişinin bilgilerinin düzenleme modunda açılmasını sağlar. Bu pencerede istediğimiz düzenlemeleri yaptıktan sonra Kaydet butonuna tıklarız. İşte diğer Edit Action u burada kullanılan Actiondur. Düzenleme modunda yapılan değişiklikleri veritabanına kaydeder. Yani ilki varsayılan düzenleme sayfası için kullanılırken diğeri bu sayfanın Post olmasından sonra kullanılır.



        // GET: Kisilers/Delete/5
        public ActionResult Delete(int? id)  // Silme Action’u kullanıcıyı silmek istediğimizde bir sayfaya yönlendirir burada size ‘Silmek                             istediğinize emin misiniz? ’ diye sorar tekrar Sil derseniz kaydı silersiniz ya da Index sayfasına geri                            dönebilirsiniz.Controllerlar bu şekilde.
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Table.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Kisilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.Table.Find(id);
            db.Table.Remove(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


//Modelimiz oluştuktan sonra projeyi Build etmemiz gerekiyor.Eğer Build etmeden Controller eklemeye çalışırsak bize projeyi Build etmemiz gerektiği yönünde hata verecektir.

//Bu noktadan sonra bir tablo daha eklersek modele sağ tık Update Model from Database deyip yeni eklediğimiz alanları seçerek ekle yapabiliriz.


//Sonra ki adımımız Controller eklemek.Controller ‘a sağ tıklayıp Controllar > Add > Controller a tıklıyoruz.
//Burada boş bir Controller oluşturabilirsiniz. Okuma ve yazma Actionlarını hazır olarak getireceğimiz Controller oluşturlabiliriz. Ya da modeli göstererek modeldeki tablolara göre bizim için hem Controllerda Actionları hem de bu Actionlar için Viewlerı oluşturan MVC5 controller with views. Biz bu seçeneği kullanacağız. Seçip ekle diyoruz. Model classını soracak bize.

//Bu alandan tablomuzu seçiyoruz. Data context classımızı secıyoruz.  Generate views seçmezsek sadece Controller’ları oluşturur kendimiz oluşan Controllerlara Add View diyerek viewlerini olustururuz. Seçersek Viewlerımız hazır gelir. Reference script libraries seçeneğini deişretli hale getirerek Script kütüphanelerini referance et dıyoruz. Layout oluşturuyor bizim için. Controller ismi veriyoruz ve Add diyerek devam ediyoruz. Ekledikten sonra bizim için Controller ve Viewler hazırlanıyor. İşlem bittiğinde Controller klasörü altında KişilersController oluştuğunu görüyoruz. Controller içinde kodlar ile birlikte oluştu. Bu seçeneği seçmiş olmasaydık bu kodları biz elle oluşturacaktık.