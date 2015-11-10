using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.InfraEstrutura.Helpers.Mvc;

namespace BakeryManager.BackOffice.Helpers
{
    public static class BMWebHelper
    {

        private const string  swkPath = "/Content/UploadfyContent/uploadify.swf";
        private const string cancelButtonPath = "/Content/UploadfyContent/uploadify-cancel.png";

        public static MvcHtmlString UploadControl(this HtmlHelper html,string Nome,string[] fileExtentions, string ActionUpdateUrl, string UpdateSuccessFunction, string DialogCloseFunction, string UploadErrorFunction)
        {

            var strUpload = UploadControlExtention.UploadControl(Nome,
                                                                 swkPath,
                                                                 cancelButtonPath,
                                                                 fileExtentions,
                                                                 ActionUpdateUrl,
                                                                 UpdateSuccessFunction,
                                                                 DialogCloseFunction,
                                                                 UploadErrorFunction);

            return strUpload;

        }

        public static MvcHtmlString UploadControl(this HtmlHelper html, string Nome, string[] fileExtentions, string ActionUpdateUrl, string UpdateSuccessFunction)
        {

            var strUpload = UploadControl(html,Nome,
                                        fileExtentions,
                                        ActionUpdateUrl,
                                        UpdateSuccessFunction,
                                        "UploadDialogClose",
                                        "UploadError");

            return strUpload;

        }

        public static MvcHtmlString UploadControl(this HtmlHelper html, string Nome, string ActionUpdateUrl, string UpdateSuccessFunction, string DialogCloseFunction, string UploadErrorFunction)
        {

            var strUpload = UploadControlExtention.UploadControl(Nome,
                                                                 swkPath,
                                                                 cancelButtonPath,
                                                                 null,
                                                                 ActionUpdateUrl,
                                                                 UpdateSuccessFunction,
                                                                 DialogCloseFunction, 
                                                                 UploadErrorFunction);

            return strUpload;

        }
    }
}