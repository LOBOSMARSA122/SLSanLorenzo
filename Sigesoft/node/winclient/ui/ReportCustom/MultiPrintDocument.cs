using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Reflection;

namespace Sigesoft.Node.WinClient.UI.ReportsCutom
{
    public class MultiPrintDocument : PrintDocument
    {
        private PrintDocument[] _documents;
        private int _docIndex;
        private PrintEventArgs _args;

        // constructors
        public MultiPrintDocument(PrintDocument document1, PrintDocument document2)
        {
            _documents = new PrintDocument[] { document1, document2 };
        }

        public MultiPrintDocument(PrintDocument[] documents)
        {
            _documents = documents;
        }

        // overidden methods
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            if (_documents.Length == 0)
                e.Cancel = true;

            if (e.Cancel) return;

            _args = e;
            _docIndex = 0;  // reset current document index
            CallMethod(_documents[_docIndex], "OnBeginPrint", e);
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            e.PageSettings = _documents[_docIndex].DefaultPageSettings;
            CallMethod(_documents[_docIndex], "OnQueryPageSettings", e);
            base.OnQueryPageSettings(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            CallMethod(_documents[_docIndex], "OnPrintPage", e);
            base.OnPrintPage(e);
            if (e.Cancel) return;
            if (!e.HasMorePages)
            {
                CallMethod(_documents[_docIndex], "OnEndPrint", _args);
                if (_args.Cancel) return;
                _docIndex++;  // increments the current document index

                if (_docIndex < _documents.Length)
                {
                    // says that it has more pages (in others documents)
                    e.HasMorePages = true;
                    CallMethod(_documents[_docIndex], "OnBeginPrint", _args);
                }
            }
        }

        // use reflection to call protected methods of child documents
        private void CallMethod(PrintDocument document, string methodName, object args)
        {
            typeof(PrintDocument).InvokeMember(methodName,
              BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
              null, document, new object[] { args });
        }
    }
}
