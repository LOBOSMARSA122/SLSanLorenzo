using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetPdf
{
    public class HandlingItextSharp
    {
        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue)
        {
            return GenerateTable(columnWidths, columnValues, fontColumnValue, null, null, null, null, null);
        }

        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue, int? borderCell)
        {
            return GenerateTable(columnWidths, columnValues, fontColumnValue, borderCell, null, null, null, null);
        }

        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue, string title, Font fontTitle)
        {
            return GenerateTable(columnWidths, columnValues, fontColumnValue, null, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue, string title, Font fontTitle, string rowspanField, int? rowspan)
        {
            return GenerateTable(columnWidths, columnValues, fontColumnValue, null, title, fontTitle, rowspanField, rowspan);
        }

        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue, int? borderCell, string title, Font fontTitle)
        {
            return GenerateTable(columnWidths, columnValues, fontColumnValue, borderCell, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTable(float[] columnWidths, string[] columnValues, Font fontColumnValue, int? borderCell, string title, Font fontTitle, string rowspanField, int? rowspan)
        {
            PdfPCell cell = null;
            var columnValueCount = columnValues.Length;
            int columnCount = columnWidths.Length;

            PdfPTable table = new PdfPTable(columnCount);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.WidthPercentage = 100;
            //companyData.TotalWidth = 500;
            //companyData.LockedWidth = true;    // Esto funciona con TotalWidth           
            table.SetWidths(columnWidths);

            // Agregar Titulo a la tabla
            if (title != null)
            {
                cell = new PdfPCell(new Paragraph(title, fontTitle));
                //cell.Border = PdfPCell.NO_BORDER;
                cell.Colspan = columnCount;
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.Black);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
            }

            // Generar Tabla
            for (int j = 0; j < columnValueCount; j++)
            {
                if (columnValues[j] != rowspanField)
                {
                    cell = new PdfPCell(new Paragraph(columnValues[j], fontColumnValue));
                }
                else
                {
                    cell = new PdfPCell(GetImage(rowspanField, 40F));
                }
                

                if (borderCell != null)
                    cell.Border = borderCell.Value;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                if (rowspanField != null)
                {
                    if (columnValues[j] == rowspanField)
                    {
                        cell.Rowspan = rowspan.Value;
                       
                       
                        //cell.AddElement(new Chunk(GetImage(rowspanField, 0.2F), 5, -15));
                        //cell.Image = GetImage(rowspanField, 0.2F);                      
                    }
                }

                table.AddCell(cell);
            }

            return table;
        }

        // Alejandro
        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths, string title, Font fontTitle)
        {
            return GenerateTableFromCells(cells, columnWidths, null, title, fontTitle, null);
        }

        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths, string title, Font fontTitle, string[] columnHeaders)
        {
            return GenerateTableFromCells(cells, columnWidths, null, title, fontTitle, columnHeaders);
        }

        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths)
        {
            return GenerateTableFromCells(cells, columnWidths, null, null, null, null);
        }

        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths, int? borderCell)
        {
            return GenerateTableFromCells(cells, columnWidths, borderCell, null, null,null);
        }

        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths, int? borderCell, string title, Font fontTitle)
        {
            return GenerateTableFromCells(cells, columnWidths, borderCell, title, fontTitle, null);
        }

        public static PdfPTable GenerateTableFromCells_(List<PdfPCell> cells, float[] columnWidths, int? borderCell, string title, Font fontTitle, string[] columnHeaders)
        {
            PdfPCell cell = null;

            int numColumns = columnWidths.Length;

            PdfPTable table = new PdfPTable(numColumns);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.WidthPercentage = 100;
            //table.TotalWidth = 500;
            //table.LockedWidth = true;    // Esto funciona con TotalWidth           
            table.SetWidths(columnWidths);
            
            
            //table.DefaultCell.Phrase = new Phrase() { Font = fontColumnValue };

            // Agregar Titulo a la tabla
            if (title != null)
            {
                cell = new PdfPCell(new Paragraph(title, fontTitle));
                //cell.Border = PdfPCell.NO_BORDER;
                cell.Colspan = numColumns;
                //cell.BackgroundColor = new BaseColor(252, 252, 252);
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }

            // Establecer Cabecera con nombres personalizados

            if (columnHeaders != null)
            {
                foreach (string ch in columnHeaders)
                {
                    cell = new PdfPCell(new Paragraph(ch, fontTitle));
                    //cell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
            }

            foreach (PdfPCell ce in cells)
            {
                if (borderCell != null)
                    ce.Border = borderCell.Value;

                table.AddCell(ce);
            }

            return table;
        }

        public static PdfPTable GenerateTableFromCells(List<PdfPCell> cells, float[] columnWidths, int? borderCell, string title, Font fontTitle, string[] columnHeaders)
        {
            PdfPCell cell = null;

            int numColumns = columnWidths.Length;

            PdfPTable table = new PdfPTable(numColumns);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.WidthPercentage = 100;
            //table.TotalWidth = 500;
            //table.LockedWidth = true;    // Esto funciona con TotalWidth           
            table.SetWidths(columnWidths);
            //table.DefaultCell.Phrase = new Phrase() { Font = fontColumnValue };

            // Agregar Titulo a la tabla
            if (title != null)
            {
                cell = new PdfPCell(new Paragraph(title, fontTitle));
                //cell.Border = PdfPCell.NO_BORDER;
                cell.Colspan = numColumns;
                //cell.BackgroundColor = new BaseColor(252, 252, 252);
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
            }

            // Establecer Cabecera con nombres personalizados

            if (columnHeaders != null)
            {
                foreach (string ch in columnHeaders)
                {
                    cell = new PdfPCell(new Paragraph(ch, fontTitle));
                    //cell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
            }

            foreach (PdfPCell ce in cells)
            {
                if (borderCell != null)
                    ce.Border = borderCell.Value;

                table.AddCell(ce);
            }

            return table;
        }





        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, Font fontColumnValue, string title, Font fontTitle)
        {
            return GenerateTableFromList(list, columnWidths, null, null, null, false, fontColumnValue, null, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string include, Font fontColumnValue, string title, Font fontTitle)
        {
            return GenerateTableFromList(list, columnWidths, null, include, null, false, fontColumnValue, null, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string include, Font fontColumnValue)
        {
            return GenerateTableFromList(list, columnWidths, null, include, null, false, fontColumnValue, null, null, null, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string include, Font fontColumnValue, int? borderCell, string title, Font fontTitle)
        {
            return GenerateTableFromList(list, columnWidths, null, include, null, false, fontColumnValue, borderCell, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string include, Font fontColumnValue, int? borderCell)
        {
            return GenerateTableFromList(list, columnWidths, null, include, null, false, fontColumnValue, borderCell, null, null, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string[] columnHeaders, bool hazSeparatorBetweenTitleAndHeader, Font fontColumnValue, string title, Font fontTitle, string rowspanField, int? rowspan)
        {
            return GenerateTableFromList(list, columnWidths, columnHeaders, null, null, hazSeparatorBetweenTitleAndHeader, fontColumnValue, null, title, fontTitle, rowspanField, rowspan);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string[] columnHeaders, Font fontColumnValue, string title, Font fontTitle, string rowspanField, int? rowspan)
        {
            return GenerateTableFromList(list, columnWidths, columnHeaders, null, null, false, fontColumnValue, null, title, fontTitle, rowspanField, rowspan);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string[] columnHeaders, string include, Font fontColumnValue, string title, Font fontTitle)
        {
            return GenerateTableFromList(list, columnWidths, columnHeaders, include, null, false, fontColumnValue, null, title, fontTitle, null, null);
        }

        public static PdfPTable GenerateTableFromList<T>(List<T> list, float[] columnWidths, string[] columnHeaders, string include, string exclude, bool hazSeparatorBetweenTitleAndHeader, Font fontColumnValue, int? borderCell, string title, Font fontTitle, string rowspanField, int? rowspan)
        {
            PdfPCell cell = null;
          
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

            PdfPTable table = new PdfPTable(propList.Count);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.WidthPercentage = 100;
            //table.TotalWidth = 500;
            //table.LockedWidth = true;    // Esto funciona con TotalWidth           
            table.SetWidths(columnWidths);

            // Agregar Titulo
            if (title != null)
            {
                cell = new PdfPCell(new Paragraph(title, fontTitle));
                //cell.Border = PdfPCell.NO_BORDER;
                cell.Colspan = propList.Count;
                //cell.BackgroundColor = new BaseColor(252, 252, 252);
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
            }

            // Insertar fila vacia en blanco como separación entre el titulo y la cabecera de la tabla
            if (hazSeparatorBetweenTitleAndHeader)
            {
                Font fontEmptyRow = FontFactory.GetFont(FontFactory.HELVETICA, 1, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

                for (int i = 0; i < propList.Count; i++)
                {
                    cell = new PdfPCell(new Paragraph(" ", fontEmptyRow));
                    cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(cell);
                }
            }


            // Establecer Cabecera con nombres personalizados

            if (columnHeaders != null)
            {
                foreach (string ch in columnHeaders)
                {
                    cell = new PdfPCell(new Paragraph(ch, fontTitle));
                    cell.BackgroundColor = new BaseColor(System.Drawing.Color.Black);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
            }

            // Construir tabla
            foreach (T item in list)
            {
                for (int i = 0; i < propList.Count; i++)
                {
                    cell = new PdfPCell(new Paragraph(propList[i].GetValue(item, null).ToString(), fontColumnValue));

                    if (borderCell != null)
                        cell.Border = borderCell.Value;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;

                    if (rowspanField != null)
                    {
                        if (propList[i].GetValue(item, null).ToString() == rowspanField)
                            cell.Rowspan = rowspan.Value;
                    }

                    table.AddCell(cell);
                }
            }

            return table;
        }

        public static Image GetImage(byte[] imgb)
        {
            return GetImage(imgb, null, null, null, null);
        }

        public static Image GetImage(byte[] imgb, float? scalePercent)
        {
            return GetImage(imgb, scalePercent, null, null, null);
        }

        public static Image GetImage(byte[] imgb, int? width, int? height)
        {
            return GetImage(imgb, width, height, null, null);
        }

        public static Image GetImage(byte[] imgb, float? scalePercent, int? alignment, int? width, int? height)
        {
            Image gif = null;

            if (imgb == null)          
                return null;
           
            //Insertar Imagen
            gif = Image.GetInstance(imgb);

            if (alignment != null)
                gif.Alignment = alignment.Value;
            else
                gif.Alignment = Image.ALIGN_LEFT;

            // downsize the image by specified percentage  

            if (scalePercent != null)
            {
                gif.ScalePercent(scalePercent.Value);
            }
            else
            {
                if (width == null)
                    width = 10;

                if (height == null)
                    height = 10;

                gif.ScaleAbsolute(width.Value, height.Value);
            }

            return gif;
                
        }

        public static Image GetImage(string imgFileName)
        {
            return GetImage(imgFileName, null, null, null, null);
        }

        public static Image GetImage(string imgFileName, float? scalePercent)
        {
            return GetImage(imgFileName, scalePercent, null, null, null);
        }

        public static Image GetImage(string imgFileName, int? width, int? height)
        {
            return GetImage(imgFileName, null, null, width, height);
        }

        public static Image GetImage(string imgFileName, float? scalePercent, int? alignment, int? width, int? height)
        {
            //Insertar Imagen
            //if (imgFileName == null)
            //    return null;
            Image gif = Image.GetInstance(imgFileName);

            if (alignment != null)
                gif.Alignment = alignment.Value;
            else
                gif.Alignment = Image.ALIGN_LEFT;        

            if (scalePercent != null)
            {
                gif.ScalePercent(scalePercent.Value);                     
            }
            else
            {
                gif.ScaleAbsolute(width.Value, height.Value);     
            }
         
            return gif;

        }

        public static PdfPCell GetColumn(string text)
        {
            return GetColumn(text, null, null, null);
        }

        public static PdfPCell GetColumn(string text, Font font)
        {
            return GetColumn(text, font, null, null);
        }  

        public static PdfPCell GetColumn(string text, int? rowspan)
        {
            return GetColumn(text, null, null, rowspan);
        }

        public static PdfPCell GetColumn(Image img, int? rowspan)
        {
            return GetColumn(null, null, img, rowspan);
        }

        private static PdfPCell GetColumn(string text, Font font, Image img, int? rowspan)
        {
            PdfPCell cell = null;

            if (img == null)
            {
                cell = new PdfPCell(new Phrase(text, font));
                if (rowspan != null)
                    cell.Rowspan = rowspan.Value;
            }
            else
            {
                cell = new PdfPCell(img);
                if (rowspan != null)
                    cell.Rowspan = rowspan.Value;
            }
           
            return cell;
        }

        public static Image CreateRectangle(PdfWriter writer, float x, float y, float width, float height, BaseColor backColor)
        {
            PdfTemplate template = writer.DirectContent.CreateTemplate(width, height);
            template.SetColorFill(backColor);
            template.Rectangle(x, y, width, height);
            template.Fill();
            writer.ReleaseTemplate(template);

            return Image.GetInstance(template);
        }


        #region Util

        private static List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            List<PropertyInfo> propList = new List<PropertyInfo>();

            if (!string.IsNullOrEmpty(include)) //Do include first
            {
                var includeProps = include.ToLower().Split(',').ToList();

                foreach (string item in includeProps)
                {
                    var find = props.Where(a => a.Name.ToLower() == item).FirstOrDefault();
                    if (find != null)
                        propList.Add(find);
                }            
            }
            else if (!string.IsNullOrEmpty(exclude)) //Then do exclude
            {
                var excludeProps = exclude.ToLower().Split(',');

                foreach (string item in excludeProps)
                {
                    var find = props.Where(a => a.Name.ToLower() == item).FirstOrDefault();
                    if (find != null)
                        propList.Add(find);
                }              
            }
            else //Default
            {
                propList.AddRange(props.ToList());
            }
            return propList;
        }


        #endregion

    }
}
