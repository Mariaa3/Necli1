using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Necli.Entidades.Entities;
using Necli.Persistencia.Interfaces;

namespace Necli.Persistencia.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Usuario> ObtenerTodos()
        {
            var usuarios = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Identificacion = reader["Identificacion"].ToString()!,
                        Tipo = reader["Tipo"].ToString()!,
                        Nombres = reader["Nombres"].ToString()!,
                        Apellidos = reader["Apellidos"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        NumeroTelefono = reader["NumeroTelefono"].ToString()!,
                        ContraseñaHash = reader["ContraseñaHash"].ToString()!,
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                    });
                }

                conn.Close();
            }

            return usuarios;
        }

        // Otros métodos necesarios también implementados...
        public Usuario? Consultar(string identificacion)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM Usuarios WHERE Identificacion = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", identificacion);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Identificacion = reader["Identificacion"].ToString()!,
                    Tipo = reader["Tipo"].ToString()!,
                    Nombres = reader["Nombres"].ToString()!,
                    Apellidos = reader["Apellidos"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    NumeroTelefono = reader["NumeroTelefono"].ToString()!,
                    ContraseñaHash = reader["ContraseñaHash"].ToString()!,
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                };
            }

            return null;
        }

        public Usuario? ConsultarPorEmail(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM Usuarios WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Identificacion = reader["Identificacion"].ToString()!,
                    Tipo = reader["Tipo"].ToString()!,
                    Nombres = reader["Nombres"].ToString()!,
                    Apellidos = reader["Apellidos"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    NumeroTelefono = reader["NumeroTelefono"].ToString()!,
                    ContraseñaHash = reader["ContraseñaHash"].ToString()!,
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                };
            }

            return null;
        }

        public bool Actualizar(Usuario usuario)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(
                "UPDATE Usuarios SET Nombres = @Nombres, Apellidos = @Apellidos, Email = @Email, NumeroTelefono = @Telefono, ContraseñaHash = @Hash WHERE Identificacion = @Id",
                conn);

            cmd.Parameters.AddWithValue("@Nombres", usuario.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Telefono", usuario.NumeroTelefono);
            cmd.Parameters.AddWithValue("@Hash", usuario.ContraseñaHash);
            cmd.Parameters.AddWithValue("@Id", usuario.Identificacion);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExisteEmail(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }

        public bool ExisteTelefono(string telefono)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE NumeroTelefono = @Tel", conn);
            cmd.Parameters.AddWithValue("@Tel", telefono);
            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
