using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Sigesoft.Common;

namespace LoadingClass
{
    public class PleaseWait : IDisposable
    {
        private Form _splash;
        private Point _location;
        private string _message;

        public PleaseWait(Point location, string message)
        {
            _location = location;
            _message = message;
            Thread t = new Thread(new ThreadStart(workerThread));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        public void Dispose()
        {
            _splash.Invoke(new MethodInvoker(stopThread));
        }

        private void stopThread()
        {
            _splash.Close();
        }

        private void workerThread()
        {
            _splash = new LoadingForm(_message);  // Substitute this with your own
            _splash.StartPosition = FormStartPosition.CenterScreen;
            //mSplash.Location = mLocation;
            //_splash.TopMost = true;
            Application.Run(_splash);
        }
    }
}
