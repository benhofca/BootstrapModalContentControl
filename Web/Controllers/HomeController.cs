using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : ControllerExtensions
    {
        public ActionResult Index()
        {
            DatabaseEntities db = new DatabaseEntities();
            return View(HomeViewModel.GetIndex(db));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public virtual JsonResult ModalAction(int MAId, string MAAction)
        {
            DynamicModalDisplay display = new DynamicModalDisplay();
            switch (MAAction)
            {
                case "EditProduct":
                    DatabaseEntities db = new DatabaseEntities();
                    display.Title = Resources.Common.EDIT;
                    Product product = Product.Get(MAId, db);
                    if(product == null)
                    {
                        display.Body = "";
                        display.ModalMessage = Resources.Models.Product.NOT_FOUND;
                    }
                    else
                    {
                        display.Body = RenderPartialViewToString("_Modal_DynamicContent_EditProduct", product);
                        display.Wide = true;
                    }
                    break;
                default:
                    display.Title = "Missing Handler";
                    display.Body = "";
                    display.ModalMessage = "The ModalAction '" + MAAction + "' does not have a handler.";
                    break;
            }
            return Json(display);
        }
        
        //[Authorize]
        [HttpPost]
        public JsonResult ModalAction_Basic(BasicModel model)
        {
            DynamicModalDisplay response = new DynamicModalDisplay();

            if (ModelState.IsValid)
            {
                response.CloseModal = true;
                response.PageMessage = Resources.Models.Basic.BASIC_ACTION_COMPLETE;
            }
            return Json(response);
        }

        //[Authorize]
        [HttpPost]
        public JsonResult ModalAction_SubmitDemo(SubmitDemoModel model)
        {
            DynamicModalDisplay response = new DynamicModalDisplay();
            Enum.TryParse(model.UpdateCaller, out BootstrapContext btnContext);
            if (ModelState.IsValid)
            {
                response.ReloadPage = model.ReloadPage;
                response.Redirect = model.Redirect;
                response.UpdateCaller = ModalContentControls.StaticModalButton(null, "0", "SubmitDemo", Resources.Models.SubmitDemo.SINGULAR, ButtonType.Button, btnContext).ToString();
                response.CloseModal = model.CloseModal;
                response.PageMessage = model.PageMessage;
                response.Title = model.Title;
                response.Body = model.Body;
                response.ModalMessage = model.ModalMessage;
            }
            else
            {
                response.ModalMessage = Resources.Models.SubmitDemo.SAVE_ERROR;
            }
            return Json(response);
        }
        
        //[Authorize]
        [HttpPost]
        public JsonResult ModalAction_EditProduct(Product model)
        {
            DynamicModalDisplay response = new DynamicModalDisplay();
            if (ModelState.IsValid)
            {
                DatabaseEntities db = new DatabaseEntities();
                if(Product.Save(model, db) == 0)
                {
                    response.ModalMessage = Resources.Models.Product.SAVE_ERROR;
                    response.Body = RenderPartialViewToString("_Modal_DynamicContent_EditProduct", model);
                }
                else
                {
                    response.PageMessage = Resources.Models.Product.SAVED;
                    response.CloseModal = true;
                    response.TargetContainer = RenderPartialViewToString("_ProductTable", Product.GetList(db));
                }
            }
            else
            {
                response.Body = RenderPartialViewToString("_Modal_DynamicContent_EditProduct", model);
            }
            return Json(response);
        }


        //[Authorize]
        [HttpPost]
        public JsonResult ModalAction_DeleteProduct(int Id)
        {
            DynamicModalDisplay response = new DynamicModalDisplay();
            
            DatabaseEntities db = new DatabaseEntities();
            if (Product.Delete(Id, db) == 0)
            {
                response.ModalMessage = Resources.Models.Product.DELETE_ERROR;
            }
            else
            {
                response.PageMessage = Resources.Models.Product.DELETED;
                response.CloseModal = true;
                response.TargetContainer = RenderPartialViewToString("_ProductTable", Product.GetList(db));
            }

            return Json(response);
        }
    }
}