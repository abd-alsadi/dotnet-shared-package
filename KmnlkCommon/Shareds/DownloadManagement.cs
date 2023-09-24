using KmnlkCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace KmnlkCommon.Shareds
{
    public class DownloadManagement
    {
        public static HttpResponseMessage Download(object file, string nameFile,string fileType,string ext)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            byte[] arr=null;
            string contentType = "";
            switch (fileType.ToLower())
            {
                case "image":
                    Image img = (Image)file;
                    arr = ImageHelper.imageToByteArray(img);
                    contentType = "application/"+ext;
                    break;
                default:
                    arr = (byte[])file;
                    contentType = "application/" + ext;
                    break;
            }
            if (arr != null)
            {
                var fileMemoryStram = new MemoryStream(arr);
                result.Content = new StreamContent(fileMemoryStram);
                var headers = result.Content.Headers;
                headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = nameFile;
                headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                headers.ContentLength = fileMemoryStram.Length;
            }
            return result;
        }
    }
}
