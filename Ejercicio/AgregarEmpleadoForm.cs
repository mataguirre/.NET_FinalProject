using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio
{
    public partial class AgregarEmpleadoForm : Form
    {
        public Form1 FormPrincipal { get; set; }
        public AgregarEmpleadoForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmpleadoConexion conexion = new EmpleadoConexion();
            string[] fields = new string[5];
            fields[0] = textName.Text;
            fields[1] = textLastName.Text;
            fields[2] = textDocument.Text;
            fields[3] = textEdad.Text;
            fields[4] = textSalario.Text;
            bool notNull = true;
            int i = 0;

            while (notNull && i < 4)
            {
                if (fields[i].Length == 0)
                {
                    notNull = false;
                };
                i++;
            }
            try
            {
                if (notNull)
                {
                    conexion.AgregarEmpleado(fields[0], fields[1], fields[2], int.Parse(fields[3]),checkBox1.Checked, decimal.Parse(fields[4]));
                    FormPrincipal.UpdateData();
                    Close();
                }
                else
                {
                    MessageBox.Show("Campos inválidos, vuelva a intentarlo...");
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textName.Text = "";
            textLastName.Text = "";
            textDocument.Text = "";
            textEdad.Text = "";
            textSalario.Text = "";
            checkBox1.Checked = false;
        }
    }
}
