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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EmpleadoConexion empleado = new EmpleadoConexion();
            dgvEmpleados.DataSource = empleado.ListarEmpleados();
            dgvEmpleados.Columns[(0)].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgregarEmpleadoForm NuevoEmpleado = new AgregarEmpleadoForm();
            NuevoEmpleado.FormPrincipal = this;
            DialogResult = NuevoEmpleado.ShowDialog();
        }
        public void UpdateData()
        {
            EmpleadoConexion empleado = new EmpleadoConexion();
            dgvEmpleados.DataSource = empleado.ListarEmpleados();
            dgvEmpleados.Columns[(0)].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            EmpleadoConexion empleado = new EmpleadoConexion();
            List<Empleado> busqueda = empleado.BuscarPorNombre(texto);
            
            if(busqueda.Count > 0)
            {
                dgvEmpleados.DataSource = empleado.BuscarPorNombre(texto);
                return;
            }
            MessageBox.Show("Nombre no encontrado. Intente otra vez");
            this.UpdateData();
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgvEmpleados.SelectedRows[0];
                // Obtener los valores de las celdas necesarias
                string nombre = selectedRow.Cells["Nombre"].Value.ToString();
                string apellido = selectedRow.Cells["Apellido"].Value.ToString();
                string dni = selectedRow.Cells["DNI"].Value.ToString();
                int edad = int.Parse(selectedRow.Cells["Edad"].Value.ToString());
                bool casado = bool.Parse(selectedRow.Cells["Casado"].Value.ToString());
                decimal salario = decimal.Parse(selectedRow.Cells["Salario"].Value.ToString());
                // Envío la información del empleado con los datos obtenidos
                EditarEmpleadoForm EditarEmpleado = new EditarEmpleadoForm(nombre, apellido, dni, edad, casado, salario);
                EditarEmpleado.FormPrincipal = this;
                DialogResult = EditarEmpleado.ShowDialog();
            } catch
            {
                MessageBox.Show("Debe seleccionar el registro completo");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dgvEmpleados.SelectedRows[0];
                // Obtener los valores de las celdas necesarias
                string dni = selectedRow.Cells["DNI"].Value.ToString();
                // Preguntamos si está seguro
                DialogResult result = MessageBox.Show("¿Estás seguro que deseas eliminar este empleado?", "Eliminar empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    // Envío la información del empleado con los datos obtenidos
                    EmpleadoConexion empleado = new EmpleadoConexion();
                    empleado.EliminarEmpleado(dni);
                    dgvEmpleados.DataSource = empleado.ListarEmpleados();
                }
            }
            catch
            {
                MessageBox.Show("Debe seleccionar el registro completo");
            }
        }
    }
}
