using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Pregunta2
{
    public partial class Form1 : Form

    {
        List<Serie> series = new List<Serie>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)

        {
            Serie temp = series.Find(x => x.Nombre == txtNombre.Text);
            if (temp != null)
            {
                MessageBox.Show("Serie ya se encuentra registrada!!!");
                return;
            }

            Serie obj = new Serie();
            obj.Codigo = txtCod.Text;
            obj.Nombre = txtNombre.Text;
            obj.Categoria = cboCat.Text;
            obj.Estreno = dtpEstreno.Text;
            obj.Duracion = Convert.ToInt32(txtDuracion.Text);
            obj.ImporteProd = Convert.ToDouble(txtImporte.Text);

            series.Add(obj);
            dgvLista.DataSource = null;
            dgvLista.DataSource = series;
            GenerarCodigo();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GenerarCodigo();
        }
        void GenerarCodigo()
        {
            int codigo = 0;
            if (series.Count == 0)
            {
                codigo = 1;
            }
            else
            {
                Serie obj = series[series.Count - 1];

                string aux = obj.Codigo.Substring(3, 3);
                codigo = Convert.ToInt32(aux) + 1;
            }
            txtCod.Text = "SE" + codigo.ToString("D4");
        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivo Json(*.json)|*.json";
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
               
                FileStream fs = new FileStream(dialogo.FileName, FileMode.Create);
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Serie>));
                js.WriteObject(fs,series);
                fs.Close();
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivo Json(*.json)|*.json";
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(dialogo.FileName, FileMode.Open);
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Serie>));
                series = (List<Serie>)js.ReadObject(fs);
                dgvLista.DataSource = series;
                fs.Close();
            }
        }
    }
}
