using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AddTech.Infraestrutura.Helpers.Export.Excel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using System.ComponentModel.DataAnnotations;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using NPOI;


namespace AddTech.Infraestrutura.Helpers.Export
{
    public class ExportHelper<T> where T : class
    {


        public void ExportExcel(ExcelExportContent<T> content)
        {

            if (content.DataSource == null)
            {
                throw new Exception("DataSource não informado.");
            }

            Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                throw new Exception(
                    "EXCEL não pode ser iniciado. Verifique se o Aplicativo está instalado ou se a DLL da Referência encontra-se disponível no diretório de instalação designado.");
            }


            xlApp.Visible = false;

            var wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            var ws = (Worksheet)wb.Worksheets[1];
            try
            {
                if (ws == null)
                {
                    throw new Exception(
                        "Planilha virtual não pode ser cirada. Verifique se o Aplicativo está instalado ou se a DLL da Referência encontra-se disponível no diretório de instalação designado.");
                }

                var l = 1;
                var c = 1;

                //iniciando construção do Cabeçalho.
                if (content.Header != null)
                {


                    //Verificando se a aplicação possui um logo
                    if (!string.IsNullOrWhiteSpace(content.Header.Logo))
                    {

                        if (content.Header.alturaImagem == 0)
                            content.Header.alturaImagem = 100;

                        if (content.Header.larguraImagem == 0)
                            content.Header.larguraImagem = 150;

                        var range = (Range)ws.Cells[l, c];
                        var left = (float)((double)range.Left);
                        var top = (float)((double)range.Top);


                        ws.Shapes.AddPicture(content.Header.Logo, MsoTriState.msoFalse, MsoTriState.msoTrue,
                            left, top, content.Header.larguraImagem, content.Header.alturaImagem);

                        range.RowHeight = content.Header.alturaImagem + 2;
                        range.ColumnWidth = content.Header.larguraImagem / 2;

                        c++;
                    }

                    //Adicionando os Descritivos;
                    var sb = new StringBuilder();
                    foreach (var desc in content.Header.Descritivos)
                        sb.Append(string.Concat(desc, Environment.NewLine));


                    ws.Cells[l, c] = sb.ToString();
                    GC.SuppressFinalize(sb.ToString());

                    var r = (Range)ws.Cells[l, c];
                    r.RowHeight = content.Header.alturaImagem + 2;
                    r.ColumnWidth = content.Header.larguraImagem / 2;

                    l++;

                    if (!string.IsNullOrWhiteSpace(content.Header.Titulo))
                    {
                        c = 1;
                        ws.Cells[l, c].Style.Font.Size = 18;
                        ws.Cells[l, c] = content.Header.Titulo;
                        ws.Cells[l, c].Style.Font.Size = 10;
                        l++;
                    }

                }


                //Alimentando o corpo da Planilha
                c = 1;


                foreach (var property in content.DataSource.FirstOrDefault().GetType().GetProperties())
                {
                    var nomeColuna =
                        AnnotationsAttributes.GetPropertyDisplayName(property);

                    ws.Cells[l, c] = string.IsNullOrWhiteSpace(nomeColuna) ? property.Name : nomeColuna;
                    c++;
                }

                c = 1;
                l++;

                foreach (var data in content.DataSource)
                {
                    foreach (var property in data.GetType().GetProperties())
                    {
                        var celula = data.GetType().GetProperty(property.Name).GetValue(data);
                        ws.Cells[l, c] = celula;
                        c++;
                    }
                    c = 1;
                    l++;
                }

                //Alimentando o Rodapé.
                if (content.Footer != null)
                {
                    if (content.Footer.DescritivosHorizontais != null)
                    {
                        c = 1;
                        foreach (var dc in content.Footer.DescritivosHorizontais)
                        {
                            ws.Cells[l, c] = dc;
                            c++;
                        }

                        l++;
                    }

                    if (content.Footer.DescritivosVerticais != null)
                    {
                        c = 1;


                        foreach (var dv in content.Footer.DescritivosVerticais)
                        {
                            ws.Cells[l, c] = dv;
                            l++;
                        }

                    }

                }

                wb.SaveAs(content.NomeArquivo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                wb.Close();
                ws = null;
                wb = null;


                int pID = 0;

                var process = ProcessHelper.GetProcessId(xlApp.Hwnd);


                if (xlApp != null)
                    xlApp.Quit();
                xlApp = null;


                process.Kill();
                process.Dispose();
            }
        }

        private static void releaseObject(ref object obj) // note ref!
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
        }

        public string ExcelExportHTML(ExcelExportContent<T> content)
        {
            if (content.DataSource == null)
            {
                throw new Exception("DataSource não informado.");
            }

            if (!content.DataSource.Any())
                return string.Empty;


            StringBuilder sb = new StringBuilder();


            sb.Append("<body><table>");


            //Adicionando o Header do documento
            if (content.Header != null)
            {
                if (!string.IsNullOrWhiteSpace(content.Header.Titulo))
                {
                    sb.Append(string.Concat("<tr><td style='font-weight: bolder; font-size: x-large;' colspan=",
                        typeof(T).GetProperties().Count().ToString(), ">", content.Header.Titulo,
                        "</td></tr><tr><td colspan=", typeof(T).GetProperties().Count().ToString(), ">&nbsp</td></tr>"));
                }


                sb.Append("<tr>");


                if (!string.IsNullOrWhiteSpace(content.Header.Logo))
                {
                    sb.Append(string.Concat("<td><img src='", content.Header.Logo, "' width='",
                        content.Header.larguraImagem, "' height='", content.Header.alturaImagem, "'/></td>"));
                }

                if (content.Header.Descritivos.Any())
                {
                    sb.Append(string.Concat("<td style='text-align: justify; vert-align: middle;' colspan='>",
                        (typeof(T).GetProperties().Count() - 1).ToString(), "'>"));

                    sb.Append("<ul>");
                    foreach (var desc in content.Header.Descritivos)

                        sb.Append(string.Concat("<li>", desc, "</li>"));

                    sb.Append("</ul>");
                    sb.Append("</td>");
                }


                sb.Append("</tr></table><p /><table>");

                //sb.Append(string.Concat("<tr><td colspan=", typeof(T).GetProperties().Count().ToString(),
                //    ">&nbsp</td></tr>"));

            }


            //Adcionando o Título das Colunas
            sb.Append("<tr>");
            foreach (var property in typeof(T).GetProperties())
            {
                var nomeColuna =
                    AnnotationsAttributes.GetPropertyDisplayName(property);

                sb.Append(string.Concat("<th>", string.IsNullOrWhiteSpace(nomeColuna) ? property.Name : nomeColuna,
                    "</th>"));

            }

            sb.Append("</tr>");

            //Adicionando os dados
            foreach (var data in content.DataSource)
            {
                sb.Append("<tr>");
                foreach (var property in data.GetType().GetProperties())
                {
                    var celula = data.GetType().GetProperty(property.Name).GetValue(data);
                    sb.Append(string.Concat("<td>", celula, "</td>"));

                }

                sb.Append("</tr>");
            }

            //Adicionando o rodapé
            if (content.Footer != null)
            {
                if (content.Footer.DescritivosHorizontais != null)
                {
                    sb.Append("<tr>");
                    foreach (var dh in content.Footer.DescritivosHorizontais)
                    {
                        sb.Append(string.Concat("<td>", dh, "</td>"));

                    }
                    sb.Append("<tr>");

                }

                if (content.Footer.DescritivosVerticais != null)
                {

                    foreach (var dv in content.Footer.DescritivosVerticais)
                    {
                        sb.Append(string.Concat("<tr><td>", dv, "</td></tr>"));
                    }

                }

            }

            sb.Append("</table></body>");

            return sb.ToString();

        }

        public Stream ExcelExportComTemplate(ExcelExportContent<T> content)
        {
            if (content.DataSource == null)
            {
                throw new Exception("DataSource não informado.");
            }


            var fs = new FileStream(content.NomeArquivoTemplate, FileMode.Open, FileAccess.Read);


            var wb = new XSSFWorkbook(fs);

            var ws = wb.GetSheet("Plan1") ?? wb.CreateSheet("Plan1");

            content.Coordenadas.Linha = content.Coordenadas.Linha > 0
                ? content.Coordenadas.Linha - 1
                : content.Coordenadas.Linha;

            content.Coordenadas.Coluna = content.Coordenadas.Coluna > 0
                ? content.Coordenadas.Coluna - 1
                : content.Coordenadas.Coluna;

            var l = content.Coordenadas.Linha;
            var c = content.Coordenadas.Coluna;

            //Alimentando o corpo da Planilha

            var linha = ws.GetRow(l) ?? ws.CreateRow(l);
            var Coluna = linha.GetCell(c) ?? linha.CreateCell(c);

            foreach (var property in typeof(T).GetProperties())
            {
                var nomeColuna =
                    AnnotationsAttributes.GetPropertyDisplayName(property);

                Coluna = linha.GetCell(c) ?? linha.CreateCell(c);

                Coluna.SetCellValue(string.IsNullOrWhiteSpace(nomeColuna) ? property.Name : nomeColuna);
                c++;
            }

            c = content.Coordenadas.Coluna;
            l++;

            foreach (var data in content.DataSource)
            {
                linha = ws.GetRow(l) ?? ws.CreateRow(l);
                foreach (var property in data.GetType().GetProperties())
                {
                    var celula = data.GetType().GetProperty(property.Name).GetValue(data);
                    Coluna = linha.GetCell(c) ?? linha.CreateCell(c);
                    Coluna.SetCellValue(celula.ToString());
                    c++;
                }
                c = content.Coordenadas.Coluna;
                l++;
            }

            //Alimentando o Rodapé.
            if (content.Footer != null)
            {
                if (content.Footer.DescritivosHorizontais != null)
                {
                    c = content.Coordenadas.Coluna;
                    linha = ws.GetRow(l) ?? ws.CreateRow(l);
                    foreach (var dc in content.Footer.DescritivosHorizontais)
                    {

                        Coluna = linha.GetCell(c) ?? linha.CreateCell(c);
                        Coluna.SetCellValue(dc);
                        c++;
                    }

                    l++;
                }

                if (content.Footer.DescritivosVerticais != null)
                {
                    c = content.Coordenadas.Coluna;


                    foreach (var dv in content.Footer.DescritivosVerticais)
                    {
                        linha = ws.GetRow(l) ?? ws.CreateRow(l);
                        Coluna = linha.GetCell(c) ?? linha.CreateCell(c);
                        Coluna.SetCellValue(dv);
                        l++;
                    }

                }

            }

            ws.ForceFormulaRecalculation = true;

            var ms = new MemoryStream();
            wb.Write(ms);

            var ff = new StreamReader(ms);
            

            fs.Close();


            return ms;
        }

    }
}





