using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public class TaskInfo
    {

        private int _hilo;
        public int hilo
        {
            get { return _hilo; }
            set
            {
                if (_hilo != value)
                {
                    _hilo = value;
                }
            }
        }

        private int _max;
        public int max
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                }
            }
        }

        private int _mal;
        public int mal
        {
            get { return _mal; }
            set
            {
                if (_mal != value)
                {
                    _mal = value;
                }
            }
        }

        private string _mensaje;
        public string mensaje
        {
            get { return _mensaje; }
            set
            {
                if (_mensaje != value)
                {
                    _mensaje = value;
                }
            }
        }

    }
}
