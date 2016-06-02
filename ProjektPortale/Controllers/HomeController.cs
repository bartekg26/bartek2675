using ProjektPortale.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjektPortale.Controllers
{

    public class HomeController : Controller
    {
        static List<Produkt> towary = new List<Produkt>();
        static List<Produkt> kupionyTowar = new List<Produkt>();

        public ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser currentUser = null;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            ViewBag.EnterToken = 0;
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //requestContext.HttpContext.User.Identity.Name
                ApplicationUser obecny = db.Users.SingleOrDefault(i => i.UserName == requestContext.HttpContext.User.Identity.Name);
                if(obecny != null)
                {
                    currentUser = obecny;
                   
                    //tu globalna
                }

            }
            else
            {
                //niezalogowany
            }

            base.Initialize(requestContext);
        }

        public ActionResult KoszykAdd(int id)
        {
            Produkt pr = db.Produkty.Find(id);//usuwanie z bazy
            db.Produkty.Remove(pr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {

            /*
            Produkt prs1 = new Produkt();
            prs1.idProduct = 1;
            prs1.cena = 10;
            prs1.nazwaProduktu = "Auto";
            prs1.opisProduktu = "duży samochód";
            //pr1.stylObrazka = "leftImg1";
            prs1.sciezkaObrazka = "~/Content/images/zdjecie1.jpg";
            prs1.userid = 
            //db.Produkty.Add(prs1);
            //db.SaveChanges();
            enable-migrations -Force
            add-migration dupacycki
            update-database
            */

            //List<Produkt> listaproduktow = db.Produkty.ToList();

            //foreach(var pro in listaproduktow)
            //{
            //    System.Diagnostics.Debug.WriteLine(pro.nazwaProduktu);
            //}


            //ApplicationUser user = db.Users.SingleOrDefault(i => i.UserName == "kanion92@gmail.com");

            //List<Produkt> koszyk = user.Koszyk.ToList();

            //kod do wyciagania uzytkownikow
            List<Produkt> lista_produktow = db.Produkty.ToList();
            return View(lista_produktow);
        }

        [HttpPost]
        public ActionResult Index(Produkt produkt)
        {
            kupionyTowar.Add(produkt);
            return View("Koszyk",kupionyTowar);
        }

        [HttpGet]
        public ActionResult MyProducts()
        {
            return View();
        }



        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Koszyk()
        {
            return View(kupionyTowar);
        }

        //[HttpGet]
        //public ActionResult DodajProdukt()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProduktVM model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.ImageUpload == null)
            {
                ModelState.AddModelError("ImageUpload", "This field asd is required");
            }
            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }


            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(currentUser.Email + "_" + DateTime.Now)).Select(s => s.ToString("x2")));

                    var uploadDir = "~/Content/uploads/products";
                    var imagename = hashValue + Path.GetExtension(model.ImageUpload.FileName);
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), imagename);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    model.Produkt.sciezkaObrazka = imagename;
                    model.Produkt.Produkty_UserId = currentUser.Id;
                    db.Produkty.Add(model.Produkt);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              
            }
            return View(model);

            /*
            if (this.ModelState.IsValid != false)
            {
                string currentusername = currentUser.UserName;
                //Initialize(Request.RequestContext);
                //pr.idProduct = 10;
                
                    //if (file != null)
                    //{
                    //    var fileName = Path.GetFileName(file.FileName);
                    //    var path = Path.Combine(Server.MapPath("~/Content/Files/"), fileName);
                    //    file.SaveAs(path);
                    //    pr. = Url.Content("~/Content/Files/" + file);
                    //}
                    

                //    pr.Produkty_UserId = currentUser.Id;
                //pr.sciezkaObrazka = "~/Content/images/zdjecie1.jpg";
                //towary.Add(pr);
                //db.Produkty.Add(pr);
                //List<Produkt> produkty = new List<Produkt>();
                //ApplicationUser user = db.Users.SingleOrDefault(i => i.UserName == "bartek@wp.pl");

                //foreach(var users in db.Users)
                //    foreach (var prod in users.Koszyk.ToList())
                //        produkty.Add(prod);

                List<Produkt> produktt = new List<Produkt>();
                foreach (var a in db.Produkty.ToList())
                    produktt.Add(a);
                //List<Produkt> koszyk = user.Koszyk.ToList();
                return RedirectToAction("Index", produktt);
            }
                
            else
                return View();
            //if (file!=null)
            //if (file.ContentLength > 0)
            //    //{
            //    //    var fileName = Path.GetFileName(file.FileName);
            //    //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            //    //    file.SaveAs(path);
            //    //}

            //    pr.idProduct = 3;
            //pr.sciezkaObrazka = "~/Content/images/zdjecie1.jpg";
            ////pr.stylObrazka = "leftImg1";
            //if (pr!=null)
            //if (pr.nazwaProduktu != null && pr.opisProduktu != null && pr.sciezkaObrazka != null)
            //{
            //    towary.Add(pr);
            //    return RedirectToAction("Index");
            //}
            //else
            //    return RedirectToAction("AddProduct");
            
    */
        }

        //[HttpPost]
        //public ActionResult DodajProdukt(Produkt pr, HttpPostedFileBase file)
        //{
        //    //if (file.ContentLength > 0)
        //    //{
        //    //    var fileName = Path.GetFileName(file.FileName);
        //    //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
        //    //    file.SaveAs(path);
        //    //}
            
        //    pr.idProduct = 3;
        //    pr.sciezkaObrazka = "~/Content/images/zdjecie1.jpg";
        //    //pr.stylObrazka = "leftImg1";
        //    if (pr.nazwaProduktu != null && pr.opisProduktu != null && pr.sciezkaObrazka != null)
        //    {
        //        towary.Add(pr);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //        return RedirectToAction("DodajProdukt");

        //}

    }
}