using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Inmobiliaria_Benito.Models

{
    public class RepositorioInquilino : RepositorioBase, IRepositorio<Inquilino>
    {
        public RepositorioInquilino(IConfiguration configuration) : base(configuration) { }

        public int Alta(Inquilino entidad)
        {
            using var connection = ObtenerConexion();
            var sql = @"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email)
                        VALUES (@Nombre, @Apellido, @Dni, @Telefono, @Email);
                        SELECT LAST_INSERT_ID();";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            command.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            command.Parameters.AddWithValue("@Dni", entidad.Dni);
            command.Parameters.AddWithValue("@Telefono", entidad.Telefono);
            command.Parameters.AddWithValue("@Email", entidad.Email);

            connection.Open();
            var id = Convert.ToInt32(command.ExecuteScalar());
            entidad.InquilinoId = id;
            return id;
        }

        public int Baja(int id)
        {
            using var connection = ObtenerConexion();
            var sql = "DELETE FROM Inquilinos WHERE InquilinoId = @id";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        public int Modificacion(Inquilino entidad)
        {
            using var connection = ObtenerConexion();
            var sql = @"UPDATE Inquilinos 
                        SET Nombre = @Nombre, Apellido = @Apellido, Dni = @Dni, Telefono = @Telefono, Email = @Email
                        WHERE InquilinoId = @InquilinoId";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            command.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            command.Parameters.AddWithValue("@Dni", entidad.Dni);
            command.Parameters.AddWithValue("@Telefono", entidad.Telefono);
            command.Parameters.AddWithValue("@Email", entidad.Email);
            command.Parameters.AddWithValue("@InquilinoId", entidad.InquilinoId);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        public IList<Inquilino> ObtenerTodos()
        {
            var lista = new List<Inquilino>();
            using var connection = ObtenerConexion();
            var sql = "SELECT * FROM Inquilinos";
            using var command = new MySqlCommand(sql, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Inquilino
                {
                    InquilinoId = reader.GetInt32("InquilinoId"),
                    Nombre = reader.GetString("Nombre"),
                    Apellido = reader.GetString("Apellido"),
                    Dni = reader.GetString("Dni"),
                    Telefono = reader.GetString("Telefono"),
                    Email = reader.GetString("Email"),
                });
            }
            return lista;
        }

        public Inquilino ObtenerPorId(int id)
        {
            Inquilino entidad = null;
            using var connection = ObtenerConexion();
            var sql = "SELECT * FROM Inquilinos WHERE InquilinoId = @id";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entidad = new Inquilino
                {
                    InquilinoId = reader.GetInt32("InquilinoId"),
                    Nombre = reader.GetString("Nombre"),
                    Apellido = reader.GetString("Apellido"),
                    Dni = reader.GetString("Dni"),
                    Telefono = reader.GetString("Telefono"),
                    Email = reader.GetString("Email"),
                };
            }
            return entidad;
        }
    }
}
