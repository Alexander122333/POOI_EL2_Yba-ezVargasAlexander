using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pregunta1
{
    public partial class Form1 : Form
    {
        List<Asistente> ArregloAsistentes = new List<Asistente>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Asistente temp = ArregloAsistentes.Find(x => x.Nombre == txtNombre.Text);
            if (temp != null)
            {
                MessageBox.Show("Asistente ya se encuentra registrado!!!");
                return;
            }

            Asistente obj = new Asistente()
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Importe = Convert.ToDouble(txtImporte.Text),
                Categoria = cboCat.Text

            };
            ArregloAsistentes.Add(obj);
            Listar();
            GenerarCodigo();
            GuardarArchivos();

        }

        void CalcularTotales()
        {
            txtImporte.Text = ArregloAsistentes.Sum(x => x.Costo()).ToString();
           
        }

        void Listar()
        {
            lvwEvento.Items.Clear();
            foreach (Asistente obj in ArregloAsistentes)
            {
                ListViewItem item = new ListViewItem(obj.Codigo);
                item.SubItems.Add(obj.Nombre);
                item.SubItems.Add(obj.Categoria.ToString());
                item.SubItems.Add(obj.Importe.ToString());
                lvwEvento.Items.Add(item);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerarCodigo();
        }
        void GenerarCodigo()
        {
            int codigo = 0;
            if (ArregloAsistentes.Count == 0)
            {
                codigo = 1;
            }
            else
            {
                Asistente obj = ArregloAsistentes[ArregloAsistentes.Count - 1];

                string aux = obj.Codigo.Substring(3, 3);
                codigo = Convert.ToInt32(aux) + 1;
            }
            txtCodigo.Text = "AEV" + codigo.ToString("D3");
        }

        void GuardarArchivos()
        {
            // Obtener el contenido a guardar
            StringBuilder sb = new StringBuilder();
            foreach (Asistente obj in ArregloAsistentes)
            {
                sb.AppendLine(obj.Codigo + "|" + obj.Nombre + "|" + obj.Categoria + "|" +
                                obj.Importe);
            }

            if (ArregloAsistentes.Count > 0)
            {
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                string ruta = "D:\\Asistentes_" + fecha;

                // archivo txt
                StreamWriter escritor1 = new StreamWriter(ruta + ".txt");
                escritor1.Write(sb.ToString());
                escritor1.Close();

                // archivo zip
                MemoryStream ms = new MemoryStream();
                StreamWriter escritor2 = new StreamWriter(ms);
                escritor2.Write(sb.ToString());
                escritor2.Flush();
                FileStream fs = new FileStream(ruta + ".zip", FileMode.Create);
                GZipStream zip = new GZipStream(fs, CompressionMode.Compress);
                zip.Write(ms.ToArray(), 0, ms.ToArray().Length);
                zip.Close();
                fs.Close();
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            Asistente obj = new Asistente();

            if (txtCantidad.Text == string.Empty)
            { obj.Cantidad = 1; }
            else
            { obj.Cantidad = int.Parse(txtCantidad.Text); }

            obj.Categoria = cboCat.Text;
            txtImporte.Text = obj.Costo().ToString("n2");
            
           

        }
    }
}

    