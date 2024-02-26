using Empleados.Entities.Empleados;
using Empleados.Services.Empleados;
using Empleados.Views.Empleados;

namespace Empleados;

public partial class EmpleadosForm : Form
{
    public EmpleadosForm()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        EmpleadoService empleado = new EmpleadoService();
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
        EmpleadoService empleado = new EmpleadoService();
        dgvEmpleados.DataSource = empleado.ListarEmpleados();
        dgvEmpleados.Columns[(0)].Visible = false;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string texto = textBox1.Text;
        EmpleadoService empleado = new EmpleadoService();
        List<Empleado> busqueda = empleado.BuscarPorNombre(texto);

        if (busqueda.Count > 0)
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
        }
        catch
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
            string dni = selectedRow.Cells["DNI"].Value.ToString()!;
            // Preguntamos si está seguro
            DialogResult result = MessageBox.Show("¿Estás seguro que deseas eliminar este empleado?", "Eliminar empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Envío la información del empleado con los datos obtenidos
                EmpleadoService empleado = new EmpleadoService();
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
