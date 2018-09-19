using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Data.Entity;
using Controller;

using Comm;
namespace ShopInfo.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = "商品首页";
            List<Entity.ShopInfo> list = baseBLL.GetList<Entity.ShopInfo>(item=>item.ID>0).ToList();
            ViewBag.list = list;

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            Entity.ShopInfo model = new Entity.ShopInfo();

            EntityesContext.GetObjByForm<Entity.ShopInfo, FormCollection>(model, frm, "");
            baseBLL.AddObject<Entity.ShopInfo>(model);
            return RedirectToAction("Index");
        }

    }
}
