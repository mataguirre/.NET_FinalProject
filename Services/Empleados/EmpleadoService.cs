﻿using Empleados.Entities.Empleados;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Empleados.Services.Empleados
{
    internal class EmpleadoService
    {
        public EmpleadoService()
        {
            SQLitePCL.Batteries.Init();
        }

        public List<Empleado> BuscarPorDNI(string DNI)
        {
            string query = "SELECT * FROM Empleados WHERE DNI = @Campo1";
            string connectionString = "Data Source=../../../Data/datasource.db";
            List<Empleado> busqueda = new List<Empleado>();
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Campo1", DNI);
                        using (SqliteDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Empleado empleado = new Empleado();
                                empleado.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                                empleado.Nombre = lector.IsDBNull(1) ? string.Empty : lector.GetString(1);
                                empleado.Apellido = lector.IsDBNull(2) ? string.Empty : lector.GetString(2);
                                empleado.DNI = lector.IsDBNull(3) ? string.Empty : lector.GetString(3);
                                empleado.Edad = lector.IsDBNull(4) ? 0 : lector.GetInt32(4);
                                empleado.Casado = lector.IsDBNull(5) ? false : lector.GetBoolean(5);
                                empleado.Salario = lector.IsDBNull(6) ? 0 : lector.GetDecimal(6);
                                busqueda.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al obtener al empleado: " + ex.Message);
                Clipboard.SetText("Error: " + ex.Message);
            }
            return busqueda;
        }

        public List<Empleado> BuscarPorNombre(string nombre)
        {
            string query = "SELECT * FROM Empleados WHERE Nombre LIKE @Campo1";
            string connectionString = "Data Source=../../../Data/datasource.db";
            List<Empleado> busqueda = new List<Empleado>();
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Campo1", nombre + "%");
                        using (SqliteDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Empleado empleado = new Empleado();
                                empleado.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                                empleado.Nombre = lector.IsDBNull(1) ? string.Empty : lector.GetString(1);
                                empleado.Apellido = lector.IsDBNull(2) ? string.Empty : lector.GetString(2);
                                empleado.DNI = lector.IsDBNull(3) ? string.Empty : lector.GetString(3);
                                empleado.Edad = lector.IsDBNull(4) ? 0 : lector.GetInt32(4);
                                empleado.Casado = lector.IsDBNull(5) ? false : lector.GetBoolean(5);
                                empleado.Salario = lector.IsDBNull(6) ? 0 : lector.GetDecimal(6);
                                busqueda.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al obtener al empleado: " + ex.Message);
            }
            return busqueda;
        }

        public List<Empleado> ListarEmpleados()
        {
            string query = "SELECT * FROM Empleados";
            string connectionString = "Data Source=../../../Data/datasource.db";
            List<Empleado> empleados = new List<Empleado>();
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(query, conexion))
                    {
                        using (SqliteDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Empleado empleado = new Empleado();
                                empleado.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                                empleado.Nombre = lector.IsDBNull(1) ? string.Empty : lector.GetString(1);
                                empleado.Apellido = lector.IsDBNull(2) ? string.Empty : lector.GetString(2);
                                empleado.DNI = lector.IsDBNull(3) ? string.Empty : lector.GetString(3);
                                empleado.Edad = lector.IsDBNull(4) ? 0 : lector.GetInt32(4);
                                empleado.Casado = lector.IsDBNull(5) ? false : lector.GetBoolean(5);
                                empleado.Salario = lector.IsDBNull(6) ? 0 : lector.GetDecimal(6);
                                empleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al obtener la lista de empleados: " + ex);
            }
            return empleados;
        }

        public void AgregarEmpleado(string nombre, string apellido, string dni, int edad, bool casado, decimal salario)
        {
            List<Empleado> empleados = ListarEmpleados();
            foreach (Empleado emp in empleados)
            {
                if (emp.DNI == dni)
                {
                    MessageBox.Show("Ya existe un empleado con este DNI");
                    return;
                }
            }
            string query = "INSERT INTO Empleados(Nombre,Apellido,DNI,Edad,Casado,Salario) VALUES (@Campo1,@Campo2,@Campo3,@Campo4,@Campo5,@Campo6)";
            string connectionString = "Data Source=../../../Data/datasource.db";
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Campo1", nombre);
                        comando.Parameters.AddWithValue("@Campo2", apellido);
                        comando.Parameters.AddWithValue("@Campo3", dni);
                        comando.Parameters.AddWithValue("@Campo4", edad);
                        comando.Parameters.AddWithValue("@Campo5", casado);
                        comando.Parameters.AddWithValue("@Campo6", salario);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al agregar al empleado: " + ex);
            }
        }

        public void EliminarEmpleado(string dni)
        {
            bool Exists = false;
            List<Empleado> empleados = ListarEmpleados();
            foreach (Empleado emp in empleados)
            {
                if (emp.DNI == dni)
                {
                    Exists = true;
                    break;
                }
            }
            if (!Exists)
            {
                MessageBox.Show("Este usuario no existe");
                return;
            }
            string query = "DELETE FROM Empleados WHERE DNI = @Campo1";
            string connectionString = "Data Source=../../../Data/datasource.db";
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Campo1", dni);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al eliminar al empleado: " + ex);
            }
        }

        public void ActualizarEmpleado(string nombre, string apellido, string dni, int edad, bool casado, decimal salario)
        {
            bool Exists = false;
            List<Empleado> empleados = ListarEmpleados();
            foreach (Empleado emp in empleados)
            {
                if (emp.DNI == dni)
                {
                    Exists = true;
                    break;
                }
            }
            if (!Exists)
            {
                MessageBox.Show("Este usuario no existe");
                return;
            }
            string updateQuery = "UPDATE Empleados SET Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad, Casado = @Casado, Salario = @Salario WHERE DNI = @DNI";
            string connectionString = "Data Source=../../../Data/datasource.db";
            try
            {
                using (SqliteConnection conexion = new SqliteConnection(connectionString))
                {
                    conexion.Open();
                    using (SqliteCommand comando = new SqliteCommand(updateQuery, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Apellido", apellido);
                        comando.Parameters.AddWithValue("@DNI", dni);
                        comando.Parameters.AddWithValue("@Edad", edad);
                        comando.Parameters.AddWithValue("@Casado", casado);
                        comando.Parameters.AddWithValue("@Salario", salario);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al actualizar al empleado: " + ex);
            }
        }
    }
}
