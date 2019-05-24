using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using Ionic.Zip;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Globalization;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using NCalc;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Sigesoft.Common
{
    public class Utils
    {
        #region FileAndFolderHandling
        public static void SaveTextToFile(string pstrFile, string pstrTextToSave)
        {
            using (StreamWriter outfile = new StreamWriter(pstrFile))
            {
                outfile.Write(pstrTextToSave);
            }

        }

        public static void DeleteFiles(List<string> pFilesToDelete)
        {
            foreach (string item in pFilesToDelete)
            {
                try
                {
                    System.IO.File.Delete(item);
                }
                catch (Exception)
                {
                }
            }
        }

        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _BinaryReader.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return _Buffer;
        }

        public static void ByteArrayToFile(string pFileName, byte[] pByteArray)
        {
            // Open file for reading
            System.IO.FileStream _FileStream = new System.IO.FileStream(pFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(pByteArray, 0, pByteArray.Length);

            // close file stream
            _FileStream.Close();
        }

        public static byte[] GetBytesOfFile(string pFileName)
        {
            FileStream fs = new FileStream(pFileName, FileMode.Open, FileAccess.Read);
            byte[] myData = new byte[fs.Length];
            fs.Read(myData, 0, (int)fs.Length);
            fs.Close();
            return myData;
        }

        public static string GetDataFromUTF8TextFile(string pTextFile)
        {
            // Reading contents using FileStream
            //using (FileStream stream = File.OpenRead(pTextFile))
            //{
            //    // reading data
            //    StringBuilder strb = new StringBuilder();
            //    byte[] b = new byte[stream.Length];
            //    UTF8Encoding temp = new UTF8Encoding(true);

            //    while (stream.Read(b, 0, b.Length) > 0)
            //    {
            //        strb.Append(temp.GetString(b, 0, b.Length));
            //    }

            //    return strb.ToString();
            //}

            //Creating a new stream-reader and opening the file.
            TextReader trs = new StreamReader(pTextFile);

            //Reading all the text of the file.
            string data = trs.ReadToEnd();

            //Close the file.
            trs.Close();

            return data;
        }

        public static string GetFileSize(long Bytes)
        {
            if (Bytes >= 1073741824)
            {
                Decimal size = Decimal.Divide(Bytes, 1073741824);
                return String.Format("{0:##.##} GB", size);
            }
            else if (Bytes >= 1048576)
            {
                Decimal size = Decimal.Divide(Bytes, 1048576);
                return String.Format("{0:##.##} MB", size);
            }
            else if (Bytes >= 1024)
            {
                Decimal size = Decimal.Divide(Bytes, 1024);
                return String.Format("{0:##.##} KB", size);
            }
            else if (Bytes > 0 & Bytes < 1024)
            {
                Decimal size = Bytes;
                return String.Format("{0:##.##} Bytes", size);
            }
            else
            {
                return "0 Bytes";
            }
        }

        public static string GetFileSize(string pFile)
        {
            FileInfo f = new FileInfo(pFile);
            return GetFileSize(f.Length);
        }

        public static string GetFileSizeInMegabytes(string pFile)
        {
            FileInfo f = new FileInfo(pFile);
            CultureInfo ingles = new CultureInfo("en-US");
            float size = ((float)f.Length) / 1048576;
            return String.Format(ingles, "{0:0.###}", size);
        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            // TODO: Pasar a estático
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public static string GetApplicationExecutingFolder()
        {
            string strExecutingFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strFolder = System.IO.Path.GetDirectoryName(strExecutingFile);
            return strFolder;
        }

        public static string GetFileNameInApplicationExecutingFolder(string pstrFileName)
        {
            return Path.Combine(GetApplicationExecutingFolder(), pstrFileName);
        }

        public static string GetFileVersion(string pstrFileName)
        {
            return FileVersionInfo.GetVersionInfo(pstrFileName).FileVersion;
        }

        public static string FormatVersionString(Version v)
        {
            string temp = string.Format("{0:00}.{1:00}.{2:00}.{3:00}", v.Major, v.Minor, v.Build, v.Revision);
            return temp;
        }

        public static int CompareVersionStrings(string pstrVersion1, string pstrVersion2)
        {
            Version v1 = new Version(pstrVersion1);
            Version v2 = new Version(pstrVersion2);

            int res = v1.CompareTo(v2);
            
            // Si res < 1 : V1 es menor que V2
            // Si res = 0 : V1 es igual a V2
            // Si res > 1 : V1 es mayor que V2

            return res;
        }

        public static List<string> GetFolderFiles(string pstrFolder, string pstrFileExtensions)
        {
            List<string> files = Directory.GetFiles(pstrFolder, "*.*", SearchOption.TopDirectoryOnly).Where(s => pstrFileExtensions.Contains(Path.GetExtension(s).ToLower())).ToList();
            return files;
        }
        
        public static byte[] ImageToBytesArray(Image pImg)
        {
            string sTemp = Path.GetTempFileName();
            var formatImage = pImg.RawFormat;
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            pImg.Save(fs, formatImage);
            fs.Position = 0;
            //
            int iByteSize = 0;
            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            iByteSize = fs.Read(bytes, 0, imgLength);
            fs.Close();
            pImg.Dispose();
            fs.Dispose();
            return bytes;
        }

        // Alejandro
        public static byte[] byteArrayToByteArrayImageJpg(byte[] byteArrayIn)
        {
            Image imgOriginal = byteArrayToImage(byteArrayIn);

            using (var ms = new MemoryStream())
            {
                imgOriginal.Save(ms, ImageFormat.Jpeg);
                byte[] jpgImage = ms.ToArray();
                return jpgImage;
            }
        }

        // Alejandro
        public static byte[] ImageToByteArrayImageJpg(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        // Alejandro
        public static byte[] ImageToByteArrayImageJpg1(System.Drawing.Image imageIn)
        {
            //MemoryStream ms = new MemoryStream();
            //imageIn.Save(ms, ImageFormat.Jpeg);
            //return ms.ToArray();

            using (var ms = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(imageIn);
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static byte[] imageToByteArray1(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static byte[] ResizeUploadedImage(Stream streamToResize)
        {
            byte[] resizedImage;
            using (Image orginalImage = Image.FromStream(streamToResize))
            {
                ImageFormat orginalImageFormat = orginalImage.RawFormat;
                int orginalImageWidth = orginalImage.Width;
                int orginalImageHeight = orginalImage.Height;
                int resizedImageWidth = 383; // Type here the width you want
                int resizedImageHeight = Convert.ToInt32(resizedImageWidth * orginalImageHeight / orginalImageWidth);
                using (Bitmap bitmapResized = new Bitmap(orginalImage, resizedImageWidth, resizedImageHeight))
                {
                    using (MemoryStream streamResized = new MemoryStream())
                    {
                        bitmapResized.Save(streamResized, orginalImageFormat);
                        resizedImage = streamResized.ToArray();
                    }
                }
            }

            return resizedImage;
        }

        public static Image BytesArrayToImage(byte[] pBytes,PictureBox pb)
        {
            System.Drawing.Image newImage = null;
            if (pBytes == null) return null;
            //            
            MemoryStream ms = new MemoryStream(pBytes);
            Bitmap bm = null;
            try
            {
                //newImage = System.Drawing.Image.FromStream(ms);
                //Decimal Hv = 280;
                //Decimal Wv = 383;

                //Decimal k = -1;

                //Decimal Hi = newImage.Height;
                //Decimal Wi = newImage.Width;

                //Decimal Dh = -1;
                //Decimal Dw = -1;

                //Dh = Hi - Hv;
                //Dw = Wi - Wv;

                //if (Dh > Dw)
                //{
                //    k = Hv / Hi;
                //}
                //else
                //{
                //    k = Wv / Wi;
                //}

                //pb.Height = (int)(k * Hi);
                //pb.Width = (int)(k * Wi);

               bm = new Bitmap(ms);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }

        public static Image BytesArrayToImageOficce(byte[] pBytes, PictureBox pb)
        {
            System.Drawing.Image newImage = null;
            if (pBytes == null) return null;
            //            
            MemoryStream ms = new MemoryStream(pBytes);
            Bitmap bm = null;
            try
            {
                //newImage = System.Drawing.Image.FromStream(ms);
                //Decimal Hv = 270;
                //Decimal Wv = 120;

                //Decimal k = -1;

                //Decimal Hi = newImage.Height;
                //Decimal Wi = newImage.Width;

                //Decimal Dh = -1;
                //Decimal Dw = -1;

                //Dh = Hi - Hv;
                //Dw = Wi - Wv;

                //if (Dh > Dw)
                //{
                //    k = Hv / Hi;
                //}
                //else
                //{
                //    k = Wv / Wi;
                //}

                //pb.Height = (int)(k * Hi);
                //pb.Width = (int)(k * Wi);

                bm = new Bitmap(ms);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static Image ThumbnailImage(Image imagenOriginal, int? imagenWidth = null)
        {
            int imagenAncho = 50;
            int imagenAlto = 50;

            if (imagenWidth == null)
            {
                imagenAncho = imagenOriginal.Width;
            }
            else
            {
                imagenAncho = imagenWidth.Value;
            }

            //'Formula de 3 simple ;)
            imagenAlto = imagenOriginal.Height * imagenAncho / imagenOriginal.Width;

            //'Para miniatura
            Image miniatura;

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            miniatura = imagenOriginal.GetThumbnailImage(imagenAncho, imagenAlto, myCallback, IntPtr.Zero);

            return miniatura;

        }
        
        #endregion

        #region Serialization
        public static void Serialize(object data, string file)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            using (StreamWriter writer = new StreamWriter(file))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static object DeSerialize(Type dataType, string file)
        {
            XmlSerializer serializer = new XmlSerializer(dataType);
            object dataToReturn;

            using (StreamReader reader = new StreamReader(file))
            {
                dataToReturn = serializer.Deserialize(reader);
            }

            return dataToReturn;
        }
        #endregion

        #region Mail
        public static void SendMessage(string emailHost, int port, bool enableSsl, bool EsHTML, string Desde, string DesdePassword, string Para, string CopiarA, string Asunto, string Mensaje, List<string> AttachedFiles)
        {
            // Obtener los correos de los destinatarios en un array
            string[] ArrayPara = Para.Split(';');
            string[] ArrayCopiaA = CopiarA.Split(';');

            // Creacion del Objeto Mail
            MailMessage oMail = new MailMessage();

            // Remitente
            oMail.From = new MailAddress(Desde);

            // Destinatarios del email
            foreach (string strPara in ArrayPara)
                if (strPara != "") oMail.To.Add(strPara.Trim());

            // Destinatarios en copia del email
            foreach (string strCopiarA in ArrayCopiaA)
                if (strCopiarA != "") oMail.CC.Add(strCopiarA.Trim());

            // Definición del Asunto
            oMail.Subject = Asunto;

            // Es un email html?
            oMail.IsBodyHtml = EsHTML;

            // Definición del Cuerpo del Email
            oMail.Body = Mensaje;

            // Agregar archivos adjuntos
            if (AttachedFiles != null)
            {
                foreach (var item in AttachedFiles)
                {
                    oMail.Attachments.Add(new Attachment(item));
                }
            }

            // Configuración para enviar el Email
            SmtpClient oSmtp = new SmtpClient();
            oSmtp.Host = emailHost;
            oSmtp.Port = port;
            oSmtp.EnableSsl = enableSsl;
            oSmtp.Credentials = new System.Net.NetworkCredential(Desde, DesdePassword);
         
            // Enviar el Email
            oSmtp.Send(oMail);
            oMail.Dispose();
                
        }

        public static bool CheckEmailExists(string address)
        {
            string[] host = (address.Split('@'));
            string hostname = host[1];

            try
            {
                IPHostEntry IPhst = Dns.Resolve(hostname);
                IPEndPoint endPt = new IPEndPoint(IPhst.AddressList[0], 25);
                Socket s = new Socket(endPt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);                      
                s.Connect(endPt);

                return true;
            }
            catch
            {
                return false;
            }
           

           
        }

        public static Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Networking

        [DllImport("wininet.dll", CharSet = CharSet.Auto)] 
        static extern bool InternetGetConnectedState(ref ConnectionState lpdwFlags, int dwReserved);

        public static bool IsConnectedToInternet(ref ConnectionState lpdwFlags)
        {        
            return InternetGetConnectedState(ref lpdwFlags, 0);
        }

        public static bool IsConnectedToInternetPing()
        {
            string host = Constants.GOOGLE_IP_ADDRESS;
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }

        public static bool PingNetwork(string hostNameOrAddress)
        {
            bool pingStatus = false;

            using (Ping p = new Ping())
            {
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                try
                {
                    PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                    pingStatus = (reply.Status == IPStatus.Success);
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }

        public static bool InternetConnectivityAvailable(string strServer)
        {
            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(strServer);
                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();

                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Internet connection available, server online
                    rspFP.Close();
                    return true;
                }
                else
                {
                    // Other status - Server or connection not available
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                // Exception - connection not available
                return false;
            }
        }

        public static bool chk_con()
        {
            try
            {
                using (var client = new WebClient())
                
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region DateTime
        public static string DateTimeFormat()
        {
            return "yyyy-MM-dd HH:mm:ss";
        }
        #endregion

        #region ZIP Files
        public static void CompressFile(string pFileName, List<string> pFilesToCompress)
        {
            // Generating the ZIP File
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                foreach (string item in pFilesToCompress)
                {
                    zip.AddFile(item, @"\");
                }

                zip.Save(pFileName);
            }
        }

        public static List<string> DecompressFile(string pCompressedFile, string pTargetFolder)
        {
            List<string> UnzippedFilePaths = new List<string>();
            using (Ionic.Zip.ZipFile ZipFile = Ionic.Zip.ZipFile.Read(pCompressedFile))
            {
                foreach (Ionic.Zip.ZipEntry Entry in ZipFile)
                {
                    Entry.Extract(pTargetFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                    UnzippedFilePaths.Add(System.IO.Path.Combine(pTargetFolder, Entry.FileName));
                }
            }
            return UnzippedFilePaths;
        }
        #endregion

        #region Config Files
        public static string GetConnectionString(string nombreCadena)
        {
            return ConfigurationManager.ConnectionStrings[nombreCadena].ConnectionString;
        }

        public static string GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }
        #endregion

        #region Encription
        public static string Encrypt(string pData)
        {
            UnicodeEncoding parser = new UnicodeEncoding();
            byte[] _original = parser.GetBytes(pData);
            MD5CryptoServiceProvider Hash = new MD5CryptoServiceProvider();
            byte[] _encrypt = Hash.ComputeHash(_original);
            return Convert.ToBase64String(_encrypt);
        }
        #endregion

        #region Exception Handling
        public static string ExceptionFormatter(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MESSAGE: " + ex.Message);
            sb.AppendLine("STACK TRACE: " + ex.StackTrace);
            sb.AppendLine("SOURCE: " + ex.Source);
            if (ex.InnerException != null)
            {
                sb.AppendLine("INNER EXCEPTION MESSAGE: " + ex.InnerException.Message);
                sb.AppendLine("INNER EXCEPTION STACK TRACE: " + ex.InnerException.StackTrace);
            }
            return sb.ToString();
        }
        #endregion

        #region Expression Evaluation
        public static double EvaluateExpression(string pstrExpression, Dictionary<string, object> pdicParams)
        {
            // COMO USAR LA LIBRERÍA NCALC:
            //static void Main(string[] args)
            //{
            //    // Formula del IMC (peso / talla^2)
            //    //string strIMC = "[N002-CAMPO0001] / ([N002-CAMPO0002]*[N002-CAMPO0002])";
            //    string strIMC = "[N002-MF000000040] / Pow([N002-MF000000039],2)";
            //    Dictionary<string, object> Params = new Dictionary<string, object>();

            //    // Ejemplo 1
            //    Params["N002-CAMPO0001"] = 95.5;   // Peso (kg)
            //    Params["N002-CAMPO0002"] = 1.83;   // Talla (m)
            //    Console.WriteLine(EvaluateExpression(strIMC, Params));

            //    // Ejemplo 2
            //    Params["N002-CAMPO0001"] = 85;   // Peso (kg)
            //    Params["N002-CAMPO0002"] = 1.83;   // Talla (m)
            //    Console.WriteLine(EvaluateExpression(strIMC, Params));

            //    // Ejemplo 3
            //    Console.WriteLine(EvaluateExpression("3+8/2", null));

            //    Console.ReadLine();
            //}

            Expression e = new Expression(pstrExpression);

                if (pdicParams != null) e.Parameters = pdicParams;

            // Si se utiliza PI en alguna fórmula
            e.EvaluateParameter += delegate(string name, ParameterArgs args)
            {
                if (name == "PI")
                    args.Result = Math.PI;
            };

            return Convert.ToDouble(e.Evaluate());
        }
        #endregion

        public static bool isFloatNumber(string _numberText)
        {
            float Result = 0;
            bool numberResult = false;

            if (float.TryParse(_numberText, out Result))
            {
                numberResult = true;
            }

            return numberResult;
        }

        public static string[] splitString(string _textString, char _character)
        {
            string[] split = null;

            if (!string.IsNullOrEmpty(_textString))
            {
                if (_textString.Contains(_character.ToString()))
                {
                    split = _textString.Split(new char[] { _character });

                    if (string.IsNullOrEmpty(split[0])) { split[0] = "0"; }

                }
                else
                {
                    split = new string[2];
                    split[0] = _textString;
                    split[1] = "00";
                }
            }

            return split;
        }

        public static string ConvertLetter(string _textNumber, string _currency)
        {
            string Words = string.Empty;
            string Number = string.Empty;
            string auxNumber = string.Empty;
            string decimalPart = string.Empty;
            string integerPart = string.Empty;
            string Fl = string.Empty;
            string Fl_II = string.Empty;
            int numberAlone = -1;

            auxNumber = _textNumber.Replace("$", "").Replace(",", "").Replace("+", "").Trim();

            if (isFloatNumber(auxNumber))
            {

                //-------Si es un número negativo
                if (auxNumber.Substring(0, 1).Equals("-"))
                {
                    Words = "Menos ";
                    auxNumber = auxNumber.Substring(1);
                }

                //-------Si tiene ceros a la izquierda

                for (int i = 0; i < auxNumber.Length; i++)
                {
                    if (auxNumber.Substring(i, 1).Equals("0"))
                    {
                        Number = auxNumber.Substring(i + 1);
                    }
                    else
                    {
                        break;
                    }
                }

                if (string.IsNullOrEmpty(Number)) { Number = auxNumber; }

                //-------Separa la parte entera de la decimal 

                string[] arrayNumber = splitString(Number, '.');

                integerPart = arrayNumber[0];

                if (arrayNumber[1].Length > 2)
                {
                    decimalPart = arrayNumber[1].Substring(0, 2);
                }
                else if (arrayNumber[1].Length == 2)
                {
                    decimalPart = arrayNumber[1];
                }
                else if (arrayNumber[1].Length == 1)
                {
                    decimalPart = arrayNumber[1] + "0";
                }

                //-------Proceso de conversión

                if (float.Parse(Number) <= 1000000)
                {
                    int sbt = 0;

                    if (int.Parse(integerPart) != 0)
                    {

                        for (int i = integerPart.Length; i > 0; i--)
                        {

                            numberAlone = int.Parse(integerPart.Substring(sbt, 1));


                            switch (i)
                            {

                                //--------Arma las centenas
                                case 6:
                                case 3:

                                    switch (numberAlone)
                                    {
                                        case 1:
                                            if (integerPart.Substring(sbt + 1, 1).Equals("0") &&
                                            integerPart.Substring(sbt + 2, 1).Equals("0"))
                                            { Words = Words + "Cien "; }
                                            else { Words = Words + "Ciento "; }
                                            break;

                                        case 2:
                                            Words = Words + "Doscientos ";
                                            break;

                                        case 3:
                                            Words = Words + "Trescientos ";
                                            break;

                                        case 4:
                                            Words = Words + "Cuatrocientos ";
                                            break;

                                        case 5:
                                            Words = Words + "Quinientos ";
                                            break;

                                        case 6:
                                            Words = Words + "SeisCientos ";
                                            break;

                                        case 7:
                                            Words = Words + "Setecientos ";
                                            break;

                                        case 8:
                                            Words = Words + "Ochocientos ";
                                            break;

                                        case 9:
                                            Words = Words + "Novecientos ";
                                            break;
                                    }

                                    break;
                                //--------Arma las decenas
                                case 5:
                                case 2:

                                    switch (numberAlone)
                                    {
                                        case 1:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Diez "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else if (integerPart.Substring(sbt + 1, 1).Equals("1"))
                                            { Words = Words + "Once "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else if (integerPart.Substring(sbt + 1, 1).Equals("2"))
                                            { Words = Words + "Doce "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else if (integerPart.Substring(sbt + 1, 1).Equals("3"))
                                            { Words = Words + "Trece "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else if (integerPart.Substring(sbt + 1, 1).Equals("4"))
                                            { Words = Words + "Catorce "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else if (integerPart.Substring(sbt + 1, 1).Equals("5"))
                                            { Words = Words + "Quince "; Fl = "D"; if (i == 2) { Fl_II = "X"; } else { Fl_II = string.Empty; } }
                                            else
                                            { Words = Words + "Dieci"; }

                                            break;

                                        case 2:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Veinte "; }
                                            else
                                            { Words = Words + "Veinti"; }

                                            break;

                                        case 3:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Treinta "; }
                                            else
                                            { Words = Words + "Treinta Y "; }

                                            break;

                                        case 4:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Cuarenta "; }
                                            else
                                            { Words = Words + "Cuarenta Y "; }

                                            break;

                                        case 5:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Cincuenta "; }
                                            else
                                            { Words = Words + "Cincuenta Y "; }

                                            break;

                                        case 6:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Sesenta "; }
                                            else
                                            { Words = Words + "Sesenta Y "; }

                                            break;

                                        case 7:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Setenta "; }
                                            else
                                            { Words = Words + "Setenta Y "; }

                                            break;

                                        case 8:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Ochenta "; }
                                            else
                                            { Words = Words + "Ochenta Y "; }

                                            break;

                                        case 9:

                                            if (integerPart.Substring(sbt + 1, 1).Equals("0"))
                                            { Words = Words + "Noventa "; }
                                            else
                                            { Words = Words + "Noventa Y "; }

                                            break;

                                    }

                                    break;


                                //--------Arma las unidades
                                case 7:
                                case 4:
                                case 1:

                                    switch (numberAlone)
                                    {
                                        case 1:

                                            if (!Fl.Equals("D"))
                                            {
                                                if (i == 4)
                                                {
                                                    Words = Words + "Un ";
                                                }
                                                else
                                                {
                                                    Words = Words + "Un "; //UNO
                                                }
                                            }
                                            else if (Fl.Equals("D") && string.IsNullOrEmpty(Fl_II))
                                            { Words = Words + "Uno "; }

                                            break;

                                        case 2:

                                            if (!Fl.Equals("D"))
                                            {
                                                Words = Words + "Dos ";
                                            }
                                            else if (Fl.Equals("D") && string.IsNullOrEmpty(Fl_II))
                                            { Words = Words + "Dos "; }

                                            break;

                                        case 3:

                                            if (!Fl.Equals("D"))
                                            {
                                                Words = Words + "Tres ";
                                            }
                                            else if (Fl.Equals("D") && string.IsNullOrEmpty(Fl_II))
                                            { Words = Words + "Tres "; }

                                            break;

                                        case 4:

                                            if (!Fl.Equals("D"))
                                            {
                                                Words = Words + "Cuatro ";
                                            }
                                            else if (Fl.Equals("D") && string.IsNullOrEmpty(Fl_II))
                                            { Words = Words + "Cuatro "; }

                                            break;

                                        case 5:

                                            if (!Fl.Equals("D"))
                                            {
                                                Words = Words + "Cinco ";
                                            }
                                            else if (Fl.Equals("D") && string.IsNullOrEmpty(Fl_II))
                                            { Words = Words + "Cinco "; }

                                            break;

                                        case 6:
                                            Words = Words + "Seis ";
                                            break;

                                        case 7:
                                            Words = Words + "Siete ";
                                            break;

                                        case 8:
                                            Words = Words + "Ocho ";
                                            break;

                                        case 9:
                                            Words = Words + "Nueve ";
                                            break;
                                    }

                                    break;

                            }
                            if (i == 4)
                            {
                                Words = Words + "Mil ";
                            }

                            if (i == 7 && integerPart.Substring(0, 1).Equals("1"))
                            {
                                Words = Words + "Millón ";
                            }
                            else if (i == 7 && !integerPart.Substring(0, 1).Equals("1"))
                            {
                                Words = Words + "Millones ";
                            }

                            sbt += 1;
                        }
                    }
                    else
                    {
                        Words = "Cero ";
                    }

                    //-------Une la parte entera con la decimal y asigna la moneda

                    if (_currency.Equals("MX"))
                    {
                        Words = Words + " Pesos " + decimalPart + "/100 M.N.";
                    }
                    else
                    {
                        Words = Words + " Con " + decimalPart + "/100";
                    }


                }
                else
                {
                    Words = "NÚMERO FUERA DE RANGO [XXXXXXX.XX]";
                }

            }
            else
            {
                Words = "DATO NO NUMÉRICO";
            }

            return Words;
        }


        public static string[] GetTextFromExpressionInCorchete(string expression)
        {           
            // \[(.*?)\]
            string pattern = Regex.Escape("[") + @"(.*?)" + @"\]";                    
            var array = Regex.Matches(expression, pattern)
                      .Cast<Match>()
                      .Select(m => m.Groups[1].Value)
                      .ToArray();
            return array;
          
        }

        public static string GetNewId(int pintNodeId, int pintSequential, string pstrPrefix)
        {
            return string.Format("N{0}-{1}{2}", pintNodeId.ToString("000"), pstrPrefix, pintSequential.ToString("000000000"));
        }

        //Validar documento de identidad (DNI o RUC)
        public static bool ValidateIdentificationDocumentPeru(string identificationDocument)
        {
            if (!string.IsNullOrEmpty(identificationDocument))
            {
                int addition = 0;
                int[] hash = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                int identificationDocumentLength = identificationDocument.Length;

                string identificationComponent = identificationDocument.Substring(0, identificationDocumentLength - 1);

                int identificationComponentLength = identificationComponent.Length;

                int diff = hash.Length - identificationComponentLength;

                for (int i = identificationComponentLength - 1; i >= 0; i--)
                {
                    addition += (identificationComponent[i] - '0') * hash[i + diff];
                }

                addition = 11 - (addition % 11);

                if (addition == 11)
                {
                    addition = 0;
                }

                char last = char.ToUpperInvariant(identificationDocument[identificationDocumentLength - 1]);

                if (identificationDocumentLength == 11)
                {
                    // The identification document corresponds to a RUC.
                    return addition.Equals(last - '0');
                }
                else if (char.IsDigit(last))
                {
                    // The identification document corresponds to a DNI with a number as verification digit.
                    char[] hashNumbers = { '6', '7', '8', '9', '0', '1', '1', '2', '3', '4', '5' };
                    return last.Equals(hashNumbers[addition]);
                }
                else if (char.IsLetter(last))
                {
                    // The identification document corresponds to a DNI with a letter as verification digit.
                    char[] hashLetters = { 'K', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
                    return last.Equals(hashLetters[addition]);
                }
            }

            return false;
        }


        public static string DevolverLetrasMayusculas(string pstrCadena)
        {

            int CantidadCaracteres = pstrCadena.Length;
            string Resultado = "";

            for (int i = 0; i < CantidadCaracteres; i++)
            {
                string letra = pstrCadena.Substring(i, 1);

                if (!char.IsLower(letra[0]))
                {
                    Resultado = Resultado + letra; 
                }
                else
                {
                    break;
                }

            }

            return Resultado;
        }

        #region Web Cam

        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern long capCreateCaptureWindow(string lpszWindowName, long dwStyle, long x, long y, long nWidth, long nHeight, long hwndParent, long nID);

        [DllImport("user32", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern long SendMessage(long hwnd, long wMsg, long wParam, object lParam);

        private const long CONNECT = 1034;

        //private long mCapHwnd;

        public static bool IsWebCamConnect(IntPtr hwndParent)
        {
            long mCapHwnd = capCreateCaptureWindow("WebCap", 0, 0, 0, 320, 240, (long)hwndParent, 0);
           
            if (SendMessage(mCapHwnd, CONNECT, 0, 0) != 0)
            {
                return true;
                //MessageBox.Show("Se ha detectado una webcam conectada y lista para usarse");
            }
            else
            {
                return false;
                //MessageBox.Show("No se ha detectado una webcam");
            }         
        }

        #endregion

        #region Convert List Entities to Html tables

        public static string GetMyTable<T>(IEnumerable<T> list, params Func<T, object>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            bool flag = true;
            sb.Append("<TABLE border= 1  CELLSPACING= 5 >\n");
            foreach (var item in list)
            {
                if (flag)
                {
                    sb.Append("<TR bgcolor= #ABAFAC align= center>\n");
                    foreach (var fxn in fxns)
                    {
                        sb.Append("<TD>");
                        sb.Append(fxn(item));
                        sb.Append("</TD>");
                    }
                    sb.Append("</TR>\n");
                    flag = false;
                }
                else
                {
                    sb.Append("<TR>\n");
                    foreach (var fxn in fxns)
                    {
                        sb.Append("<TD>");
                        sb.Append(fxn(item));
                        sb.Append("</TD>");
                    }
                    sb.Append("</TR>\n");
                }
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        #endregion

        public static int GetAge(DateTime FechaNacimiento)
        {
            return int.Parse((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString());
        }

        public static string Getmouth(int pintMes)
        {
            string Mes="";
             if (pintMes == 1)
                {
                    Mes = "Enero";
                }
                else if (pintMes == 2)
                {
                      Mes = "Febrero";
                }
                else if (pintMes == 3)
                {
                    Mes = "Marzo";
                }
                else if (pintMes == 4)
                {
                    Mes = "Abril";
                }
                else if (pintMes == 5)
                {
                    Mes = "Mayo";
                }
                else if (pintMes == 6)
                {
                    Mes = "Junio";
                }
                else if (pintMes == 7)
                {
                    Mes = "Julio";
                }
                else if (pintMes == 8)
                {
                    Mes = "Agosto";
                }
                else if (pintMes == 9)
                {
                    Mes = "Setiembre";
                }
                else if (pintMes == 10)
                {
                    Mes = "Octubre";
                }
                else if (pintMes == 11)
                {
                    Mes = "Noviembre";
                }
                else if (pintMes == 12)
                {
                    Mes = "Diciembre";
                }
             return Mes;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }

        }
        //
        public static YearsMonths YearMonthDiff(DateTime startDate, DateTime endDate)
        {
            int monthDiff = ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month);
            int years = (int)Math.Floor((decimal)(monthDiff / 12));
            int months = monthDiff % 12;

            return new YearsMonths
            {
                TotalMonths = monthDiff,
                Years = years,
                Months = months
            };
        }
        public class YearsMonths
        {
            public int TotalMonths { get; set; }
            public int Years { get; set; }
            public int Months { get; set; }
        }

        public static void SendFileFtp(string serverIp, string userName, string password, string fileName)
        {
            string ftpServerIP = serverIp;
            string ftpUserName = userName;
            string ftpPassword = password;
            string filename = fileName;

            FileInfo objFile = new FileInfo(filename);

            FtpWebRequest objFTPRequest;

            // Create FtpWebRequest object 
            objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));

            // Set Credintials
            objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // By default KeepAlive is true, where the control connection is 
            // not closed after a command is executed.
            objFTPRequest.KeepAlive = false;

            // Set the data transfer type.
            objFTPRequest.UseBinary = true;

            // Set content length
            objFTPRequest.ContentLength = objFile.Length;

            // Set request method
            objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

            // Set buffer size
            int intBufferLength = 16 * 1024;
            byte[] objBuffer = new byte[intBufferLength];

            // Opens a file to read
            FileStream objFileStream = objFile.OpenRead();

            try
            {
                // Get Stream of the file
                Stream objStream = objFTPRequest.GetRequestStream();

                int len = 0;

                while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
                {
                    // Write file Content 
                    objStream.Write(objBuffer, 0, len);

                }

                objStream.Close();
                objFileStream.Close();

            }
            catch (Exception ex)
            {
                throw ex;

            }	
        }

        public static bool AccesoInternet()       {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception es)
            {

                return false;
            }

        }



        public static string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100 SOLES";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
    }
}
