using System.Data.SqlClient;
using Necli.Entidades.Entities;
using Necli.Persistencia.Interfaces;

namespace Necli.Persistencia.Repositorios
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly string _connectionString;

        public CuentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool CrearCuenta(Cuenta cuenta)
        {
            using var conn = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Cuentas (UsuarioId, Saldo, FechaCreacion)
                          VALUES (@UsuarioId, @Saldo, @FechaCreacion)";
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UsuarioId", cuenta.UsuarioId);
            cmd.Parameters.AddWithValue("@Saldo", cuenta.Saldo);
            cmd.Parameters.AddWithValue("@FechaCreacion", cuenta.FechaCreacion);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public Cuenta? ConsultarCuenta(int numeroCuenta)
        {
            using var conn = new SqlConnection(_connectionString);
            var query = "SELECT Numero, UsuarioId, Saldo, FechaCreacion FROM Cuentas WHERE Numero = @Numero";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Numero", numeroCuenta);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cuenta
                {
                    Numero = Convert.ToInt32(reader["Numero"]),
                    UsuarioId = reader["UsuarioId"].ToString(),
                    Saldo = Convert.ToDecimal(reader["Saldo"]),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                };
            }

            return null;
        }

        public bool EliminarCuenta(int numeroCuenta)
        {
            using var conn = new SqlConnection(_connectionString);
            var query = "DELETE FROM Cuentas WHERE Numero = @Numero";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Numero", numeroCuenta);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Actualizar(Cuenta cuenta)
        {
            using var conn = new SqlConnection(_connectionString);
            var query = "UPDATE Cuentas SET Saldo = @Saldo WHERE Numero = @Numero";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Saldo", cuenta.Saldo);
            cmd.Parameters.AddWithValue("@Numero", cuenta.Numero);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public IEnumerable<object> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}
