using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using IBLL;
using BLL;

namespace Controller
{
    public class BaseController : System.Web.Mvc.Controller
    {
        protected IBaseBLL baseBLL = new BaseBLL();
    }
}
