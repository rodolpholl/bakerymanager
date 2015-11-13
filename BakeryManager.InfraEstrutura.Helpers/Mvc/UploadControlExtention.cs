using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BakeryManager.InfraEstrutura.Helpers.Mvc
{
    public static class UploadControlExtention
    {

        public static MvcHtmlString UploadControl(string Nome, string swfPath, string cancelButtonPath, string[] FileExtentionsAllowed, string UploadActionUrl, string UploadSuccessFunction, 
                                                  string UploadDialogCloseFunction)
        {
            return UploadControl(Nome, swfPath, cancelButtonPath, FileExtentionsAllowed, UploadActionUrl, UploadSuccessFunction, UploadDialogCloseFunction, "");
        }

        public static MvcHtmlString UploadControl(string Nome, string swfPath, string cancelButtonPath, string UploadActionUrl, string UploadSuccessFunction)
        {
            return UploadControl(Nome, swfPath, cancelButtonPath, null, UploadActionUrl, UploadSuccessFunction, "", "");
        }

        public static MvcHtmlString UploadControl(string Nome, string swfPath, string cancelButtonPath, string[] FileExtentionsAllowed, string UploadActionUrl, string UploadSuccessFunction)
        {
            return UploadControl(Nome, swfPath, cancelButtonPath, FileExtentionsAllowed, UploadActionUrl, UploadSuccessFunction,"","");
        }

        public static MvcHtmlString UploadControl(string Nome,string swfPath,string cancelButtonPath, string[] FileExtentionsAllowed, string UploadActionUrl,
                                                  string UploadSuccessFunction, string UploadDialogCloseFunction,  string UploadErrorFunction)
        {


            /*<input type="file" name="fileLoad" id="fileLoad" class="form-control"  />

                    <script type=\"text//javascript\">
                            $(function () {
                                $('#fileLoad').uploadify({
                                    'swf': \"@Url.Content(\"~//Content//UploadfyContent//uploadify.swf\")\",
                                    'cancelImg': \"@Url.Content(\"~//Content//UploadfyContent//uploadify-cancel.png\")\",
                                    'fileTypeExts' : \"*.xlsx\",
                                    'uploader': \"@Url.Action(\"CarregarArquivo\")\",
                                    'onDialogClose': UploadDialogClose,
                                    'onUploadSuccess': UploadSuccess,
                                    'onUploadError': UploadError
                                });
                                
                            });

                    </script>*/

            //Validações

            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentNullException("Nome", "O Nome do Controle é obrigatório.");

            if (string.IsNullOrWhiteSpace(swfPath))
                throw new ArgumentNullException("swfPath", "O Caminho do arquivo SWF é obrigatório.");

            if (string.IsNullOrWhiteSpace(cancelButtonPath))
                throw new ArgumentNullException("cancelButtonPath", "O Caminho do ícone para o botão 'Cancelar' é obrigatório.");

            if (string.IsNullOrWhiteSpace(UploadActionUrl))
                throw new ArgumentNullException("UploadActionUrl", "A URL da Action para o upload é obrigatória.");

            if (string.IsNullOrWhiteSpace(UploadSuccessFunction))
                throw new ArgumentNullException("UploadSuccessFunction", "O nome da Função de retorno com sucesso do upload é obrigatória.");

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<input type=\"file\" name=\"{0}\" id=\"{0}\" class=\"form-control\"  />",Nome));
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("$(function () {");
            sb.Append("$(\"#"+Nome+"\").uploadify({");
            sb.Append(string.Format("'swf': \"{0}\",", swfPath));
            sb.Append(string.Format("'cancelImg': \"{0}\",", cancelButtonPath));

            if (FileExtentionsAllowed != null)
            {
                string fileExt = "";
                foreach (var ext in FileExtentionsAllowed)
                    fileExt += string.Concat(ext, ";");
                

                if (fileExt.Length > 0)
                    fileExt = fileExt.Substring(0, fileExt.Length - 1);

                sb.Append(string.Format("'fileTypeExts' : \"{0}\",", fileExt));
            }

            sb.Append("'successTimeout' : 3600,");
            sb.Append("'method' : 'post',");
            sb.Append(string.Format("'uploader': \"{0}\",",UploadActionUrl));
            sb.Append(string.Format("'onUploadSuccess': {0}", UploadSuccessFunction));

            if(!string.IsNullOrWhiteSpace(UploadDialogCloseFunction))
                sb.Append(string.Format(",'onDialogClose': {0}", UploadDialogCloseFunction));

            if (!string.IsNullOrWhiteSpace(UploadErrorFunction))
                sb.Append(string.Format(",'onUploadError': {0}", UploadErrorFunction));

            sb.Append("}); }); </script>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}
