using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Inmobiliaria_Benito.Models
{
    public class RepositorioPropietario : RepositorioBase, IRepositorio<Propietario>
    {
        public RepositorioPropietario(IConfiguration configuration) 
            : base(configuration) 
        { }

        public int Alta(Propietario entidad)
        {
            using var connection = ObtenerConexion();
            var sql = @"
                INSERT INTO propietario (nombre, apellido, dni, telefono, email)
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
            entidad.PropietarioId = id;
            return id;
        }

        public int Baja(int id)
        {
            using var connection = ObtenerConexion();
            var sql = "DELETE FROM propietario WHERE propietarioId = @id";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        public int Modificacion(Propietario entidad)
        {
            using var connection = ObtenerConexion();
            var sql = @"
                UPDATE propietario 
                SET nombre = @Nombre,
                    apellido = @Apellido,
                    dni = @Dni,
                    telefono = @Telefono,
                    email = @Email
                WHERE propietarioId = @PropietarioId";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
            command.Parameters.AddWithValue("@Apellido", entidad.Apellido);
            command.Parameters.AddWithValue("@Dni", entidad.Dni);
            command.Parameters.AddWithValue("@Telefono", entidad.Telefono);
            command.Parameters.AddWithValue("@Email", entidad.Email);
            command.Parameters.AddWithValue("@PropietarioId", entidad.PropietarioId);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        public IList<Propietario> ObtenerTodos()
        {
            var lista = new List<Propietario>();
            using var connection = ObtenerConexion();
            var sql = "SELECT * FROM propietario";
            using var command = new MySqlCommand(sql, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Propietario
                {
                    PropietarioId = reader.GetInt32("propietarioId"),
                    Nombre        = reader.GetString("nombre"),
                    Apellido      = reader.GetString("apellido"),
                    Dni           = reader.GetString("dni"),
                    Telefono      = reader.GetString("telefono"),
                    Email         = reader.GetString("email")
                });
            }
            return lista;
        }

        public Propietario ObtenerPorId(int id)
        {
            Propietario entidad = null;
            using var connection = ObtenerConexion();
            var sql = "SELECT * FROM propietario WHERE propietarioId = @id";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                entidad = new Propietario
                {
                    PropietarioId = reader.GetInt32("propietarioId"),
                    Nombre        = reader.GetString("nombre"),
                    Apellido      = reader.GetString("apellido"),
                    Dni           = reader.GetString("dni"),
                    Telefono      = reader.GetString("telefono"),
                    Email         = reader.GetString("email")
                };
            }
            return entidad;
        }
    }
}
