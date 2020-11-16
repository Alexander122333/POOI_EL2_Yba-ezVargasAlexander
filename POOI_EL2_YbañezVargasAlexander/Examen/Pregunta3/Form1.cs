using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Pregunta3
{
    public partial class Form1 : Form
    {               
        private string archivo = "";

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] key = new byte[] { 70, 71, 72, 73, 74, 75, 76, 77 };
        byte[] iv = new byte[] { 50, 51, 52, 53, 54, 55, 56, 57 };

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivo de texto (*.txt)|*.txt";

            if(dialogo.ShowDialog() == DialogResult.OK)
            {
                MemoryStream ms = new MemoryStream();
                StreamWriter escritor = new StreamWriter(ms);
                escritor.Write(txtEnc.Text);
                escritor.Flush();

                FileStream fs = new FileStream(dialogo.FileName, FileMode.Create);

                CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(ms.ToArray(), 0, ms.ToArray().Length);

                cs.Close();
                fs.Close();
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEnc.Clear();
        }

        private void btnDesencriptar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivo de texto (*.txt)|*.txt";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(dialogo.FileName, FileMode.Open);
                CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                txtEnc.Text = new StreamReader(cs).ReadToEnd();

                cs.Close();
                fs.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivo binario (*.bin)|*.bin";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                MemoryStream ms = new MemoryStream();
                StreamWriter escritor = new StreamWriter(ms);
                escritor.Write(txtEnc.Text);
                escritor.Flush();

                FileStream fs = new FileStream(dialogo.FileName, FileMode.Create);

                CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(ms.ToArray(), 0, ms.ToArray().Length);

                cs.Close();
                fs.Close();
            }
        }
    }
}
