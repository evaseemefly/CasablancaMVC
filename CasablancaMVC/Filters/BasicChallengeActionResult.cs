using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CasablancaMVC.Filters
{
    public class BasicChallengeActionResult:ActionResult
    {
        public ActionResult CurrentResult { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            CurrentResult.ExecuteResult(context);

            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                response.AddHeader("", "");
        }
    }
}
