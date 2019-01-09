using MyWebApp.OA.IBLL;
using MyWebApp.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.OA.UI.Controllers
{
    public class BillController : Controller
    {
        //
        // GET: /Bill/

        public IBillService  BillService { get; set; }
        public IPayTypeService PayTypeService { get; set; }
        public IBillTypeService BillTypeService { get; set; }
        public ICurrentUnitService CurrentUnitService { get; set; }
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GetBillInfo()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//显示的条数
            int totalCount = 0;
            var BillInfoList = BillService.LoadPagedEntities(pageIndex,pageSize,out totalCount,b=>true,b=>b.CreateTime,false);

            var temp = from u in BillInfoList
                       select new
                       {
                           ID = u.Id,
                           GoodMsg = u.GoodMsg,
                           OrderNumber = u.OrderNumber,
                           BillTypeName = u.BillType.BillTypeName,
                           PayTypeName = u.PayType.PayTypeName,
                           CreateTime = u.CreateTime,
                           CurrentUnit = u.CurrentUnit.UintName,
                           Money = u.Money,
                           Type = u.Type,
                           Remark=u.Remark
                           
                       };
            return Json(new { rows = temp, total =totalCount }, JsonRequestBehavior.AllowGet);
        }


        public  ActionResult GetBillTypeData()
        {
            var BillTypeList = BillTypeService.LoadEntities(b => true);
            var temp = from b in BillTypeList
                       select new
                       {
                           id = b.Id,
                           text = b.BillTypeName
                       };

            return Json(temp, JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetPayTypeData()
        {
            var payTypeList = PayTypeService.LoadEntities(p => true);
            var temp = from p in payTypeList
                       select new
                       {
                           id =p.Id,
                           text =p.PayTypeName
                       };

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUnitData()
        {
            var UnitList = CurrentUnitService.LoadEntities(p => true);
            var temp = from u in UnitList
                       select new
                       {
                           id = u.Id,
                           text = u.UintName
                       };

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddBillInfo(Bill bill)
        {
            bill.OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            bill.CreateTime = DateTime.Now;
            
            BillService.AddEntity(bill);
            return Content("ok");
        }



    }
}
