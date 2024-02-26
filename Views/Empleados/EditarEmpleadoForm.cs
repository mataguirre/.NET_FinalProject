using Empleados.Services.Empleados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Empleados.Views.Empleados
{
    public partial class EditarEmpleadoForm : Form
    {
        public EmpleadosForm FormPrincipal { get; set; }
        string oldName;
        string oldLastName;
        int oldEdad;
        bool oldCasado;
        decimal oldSalario;
        public EditarEmpleadoForm(string nombre, string apellido, string dni, int edad, bool casado, decimal salario)
        {
            InitializeComponent();
            this.oldName = nombre;
            this.oldLastName = apellido;
            this.oldEdad = edad;
            this.oldCasado = casado;
            this.oldSalario = salario;
            textName.Text = nombre;
            textLastName.Text = apellido;
            textEdad.Text = edad.ToString();
            textDocument.Text = dni;
            checkCasado.Checked = casado;
            textSalario.Text = salario.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmpleadoService conexion = new EmpleadoService();
            string[] fields = new string[4];
            fields[0] = textName.Text;
            fields[1] = textLastName.Text;
            fields[2] = textEdad.Text;
            fields[3] = textSalario.Text;

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
                    conexion.ActualizarEmpleado(fields[0], fields[1], textDocument.Text, int.Parse(fields[2]), checkCasado.Checked, decimal.Parse(fields[3]));
                    FormPrincipal.UpdateData();
                    Close();
                }
                else
                {
                    MessageBox.Show("Campos inválidos, vuelva a intentarlo...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            textName.Text = this.oldName;
            textLastName.Text = this.oldLastName;
            textEdad.Text = this.oldEdad.ToString();
            checkCasado.Checked = this.oldCasado;
            textSalario.Text = this.oldSalario.ToString();
        }
    }
}
