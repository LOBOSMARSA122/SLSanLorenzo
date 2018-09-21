using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace NetPdf
{
    public class MergeExPDF
    {

        #region Fields
        private string sourceFolder;
        private string destinationFile;
        private IList fileList = new ArrayList();
        #endregion

        #region Public Methods
        ///
        /// Add a new file, together with a given docname to the fileList and namelist collection
        ///
        public void AddFile(string pathName)
        {
            fileList.Add(pathName);
        }

        ///
        /// Generate the merged PDF
        ///
        public void Execute()
        {
            //MergeDocs();
            CombineMultiplePDFs();
        }

        public void RunFile()
        {
            Process proceso = Process.Start(destinationFile);
            proceso.WaitForExit();
            proceso.Close();
           
        }

        #endregion

        #region Private Methods

        private void CombineMultiplePDFs()
        {
            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(destinationFile, FileMode.Create));
            if (writer == null)
            {
                return;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in FilesName)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                //PRAcroForm form = reader.AcroForm;
                //if (form != null)
                //{
                //    writer.CopyAcroForm(reader);
                //}

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();

            // Eliminar PDFs temp
            //foreach (string filePath in FilesName)
            //    File.Delete(filePath);

        }

        ///
        /// Merges the Docs and renders the destinationFile
        ///
        private void MergeDocs()
        {

            //Step 1: Create a Docuement-Object
            Document document = new Document();

            PdfContentByte cb = null;
            PdfImportedPage page = null;
            //PdfReader reader = null;

            try
            {
                //Step 2: we create a writer that listens to the document
                //var fs = new FileStream(destinationFile, FileMode.Create);

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
               
                    //Step 3: Open the document
                    document.Open();

                    cb = writer.DirectContent;

                    int n = 0;
                    int rotation = 0;

                    //Loops for each file that has been listed
                    foreach (string filename in fileList)
                    {
                        //The current file path
                        string filePath = sourceFolder + filename;

                        // we create a reader for the document
                        //using (reader = new PdfReader(filePath))
                        //{
                        var reader = new PdfReader(filePath);
                     
                            ////Gets the number of pages to process
                            n = reader.NumberOfPages;

                            int i = 0;

                            while (i < n)
                            {
                                i++;
                                document.SetPageSize(reader.GetPageSizeWithRotation(1));
                                document.NewPage();

                                //Insert to Destination on the first page
                                if (i == 1)
                                {
                                    Chunk fileRef = new Chunk(" ");
                                    fileRef.SetLocalDestination(filename);
                                    document.Add(fileRef);

                                }

                                page = writer.GetImportedPage(reader, i);
                                rotation = reader.GetPageRotation(i);

                                if (rotation == 90 || rotation == 270)
                                {
                                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                                }
                                else
                                {
                                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                                }

                                //reader.Close();
                            }

                            //reader.Close();
                            //reader.Dispose();
                        //};
                    }
                //};

                    //if (document.PageNumber != 0)
                    //{
                        document.Close();
                        document.Dispose();

                        writer.Close();
                        writer.Dispose();
                    //}
                   
              
                    //fs.Close();
                    //fs.Dispose();
                   
                //reader.Close();
                //reader.Dispose();

                    //System.Threading.Thread.Sleep(5000);
               
              //var r = @"F:\RepSIGSO_v1.0\dev\src\node\winclient\ui\bin\Debug\TempMerge\312.pdf";
              //File.Delete(r);

                //string[] files = Directory.GetFiles(SourceFolder);
                //foreach (string filePath in files)
                //    File.Delete(filePath);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if (document.IsOpen())
                //    document.Close();
            }
        }
        #endregion

        #region Properties
        ///
        /// Gets or Sets the SourceFolder
        ///
        public string SourceFolder
        {
            get { return sourceFolder; }
            set { sourceFolder = value; }
        }

        ///
        /// Gets or Sets the DestinationFile
        ///
        public string DestinationFile
        {
            get { return destinationFile; }
            set { destinationFile = value; }
        }

        public List<string> FilesName { get; set; }

        #endregion
    }
}
