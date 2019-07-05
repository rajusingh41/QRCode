using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace QRCodePro.Controllers
{
    public class QRCodeController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GenerateQrCode()
        {
            var qrPayload = new QRModel
            {
                ServerConfig = "104.215.180.104",
                DomainCode = "dgn5",
                UserId = "utl009"
            };

            var jsonString = JsonConvert.SerializeObject(qrPayload);
            using (var qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.Q,forceUtf8:true))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (Bitmap bitMap = qrCode.GetGraphic(20, Color.Black, Color.White, new Bitmap(HttpContext.Current.Server.MapPath(@"~/App_Data/HR-One-logo.png")), 16))
                        {
                                var folderPath = HttpContext.Current.Server.MapPath(@"~/QRImage/");
                            var fileName = "myQr1"+DateTime.Now.ToString("yyyy-mm-dd")+".png" ;
                            var imagePath = folderPath + fileName;
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                          //  bitMap.Save(imagePath);

                            using (var ms = new MemoryStream())
                            {
                                bitMap.Save(ms, ImageFormat.Png);
                                File.WriteAllBytes(imagePath, ms.ToArray());
                            }

                        }
                    }
                }
            }
            return Ok();
        }
    }
}
