using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace QRCodePro.Controllers
{
    public class TestController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetBaseUrl()
        {
            string basePath = HttpContext.Current.Request.Url.Host;

//            string basePath = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority +
//#pragma warning disable S1075 // URIs should not be hardcoded
//                                                                          Request.ApplicationPath.TrimEnd('/') + "/";
//#pragma warning restore S1075 // URIs should not be hardcoded


            //Get request from Current HttpContext
            var request = HttpContext.Current.Request;


            var baseUrl = request.Url.Scheme + "://" + request.Url.Authority;


            var _baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);



          var result = string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                    );

            return Ok();
        }
    }
}
