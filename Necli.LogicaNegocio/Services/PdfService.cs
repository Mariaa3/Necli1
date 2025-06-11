using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;

namespace Necli.LogicaNegocio.Services
{
    public class PdfService
    {
        public string GenerarPdfReporte(string contenido, string cedula, int mes, int año, string nombreArchivo)
        {
            string carpeta = Path.Combine("wwwroot", "reportes", año.ToString(), mes.ToString());
            Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, nombreArchivo + ".pdf");

            using var fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            var doc = new Document();
            var writer = PdfWriter.GetInstance(doc, fs);

            writer.SetEncryption(
                Encoding.UTF8.GetBytes(cedula),
                null,
                PdfWriter.ALLOW_PRINTING,
                PdfWriter.ENCRYPTION_AES_128
            );

            doc.Open();
            doc.Add(new Paragraph(contenido));
            doc.Close();

            return ruta;
        }
    }
}
