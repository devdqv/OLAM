using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLAM.Controllers
{
    public class BaseController : Controller
    {
        // public Models.Saleman mUserInforBH;
        public System.Globalization.CultureInfo objCultureInfo = new CultureInfo("vi-VN");

        public Models.USERLOGIN mUserInfor
        {
            get
            {
                Models.USERLOGIN _mUserInfo = Session["user"] as Models.USERLOGIN;
                if (_mUserInfo == null)
                    _mUserInfo = new Models.USERLOGIN();
                return _mUserInfo;
            }
        }



        // GET: Base
        public class NotAuthorizeAttribute : FilterAttribute
        {
            // Does nothing, just used for decoration
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is NotAuthorizeAttribute)) return;
            if (Session["user"] == null)
            {
                Session["Return"] = Request.Url.PathAndQuery;
                filterContext.Result = new RedirectResult("/?Return=" + Request.Url.PathAndQuery);
            }
            //mUserInforBH = Session["user"] as Models.Saleman;
            base.OnActionExecuting(filterContext);
        }
    }
}