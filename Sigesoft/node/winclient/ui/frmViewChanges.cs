using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmViewChanges : Form
    {
        private string _commentary;
        public frmViewChanges(string commentary)
        {
            _commentary = commentary;
            InitializeComponent();
        }

        private void frmViewChanges_Load(object sender, EventArgs e)
        {

            List<string> oldArrayCommentary = _commentary.Split('<').ToList();
            List<string> newArrayCommentary = oldArrayCommentary.FindAll(x => x != "").ToList();

            string finalText = "";
            var sb = new StringBuilder();
            sb.Append(@"{\rtf1\ansi");
            foreach (var commentary in newArrayCommentary)
            {
                List<string> oldCampos = commentary.Split('|').ToList();
                List<string> newCampos = oldCampos.FindAll(x => x != "").ToList();

                foreach (var campo in newCampos)
                {
                    var split = campo.Split(':');
                    string nombre = split[0];
                    string valor = split[1];
                    if (split.Length == 4) // si es un formato de fecha
                    {
                        valor += ":" + split[2] + ":" + split[3];
                    }


                    if (valor == "") valor = "----";

                    sb.Append(@"\b " + nombre + @": \b0 ");
                    sb.Append((valor));
                    sb.Append(@" \line ");
                }

                sb.Append(@" \line ");
                sb.Append(("----------------------------------------------------"));
                sb.Append(@" \line \line");

            }
            sb.Append(@"}");
            richComentarios.Rtf = sb.ToString();
        }
    }
}
