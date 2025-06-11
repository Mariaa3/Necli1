using System.Data.SqlClient;
using Necli.Entidades.Entities;
using Necli.Persistencia.Interfaces;

namespace Necli.Persistencia.Repositorios
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly string _connectionString;

        public TransaccionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Registrar(Transaccion transaccion)
        {
            using var conn = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Transacciones (Fecha, CuentaOrigenId, CuentaDestinoId, Monto, Tipo)
                          VALUES (@Fecha, @CuentaOrigenId, @CuentaDestinoId, @Monto, @Tipo)";
            using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
            cmd.Parameters.AddWithValue("@CuentaOrigenId", transaccion.CuentaOrigenId);
            cmd.Parameters.AddWithValue("@CuentaDestinoId", transaccion.CuentaDestinoId);
            cmd.Parameters.AddWithValue("@Monto", transaccion.Monto);
            cmd.Parameters.AddWithValue("@Tipo", transaccion.Tipo);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<Transaccion> Listar()
        {
            var transacciones = new List<Transaccion>();
            using var conn = new SqlConnection(_connectionString);
            var query = "SELECT Numero, Fecha, CuentaOrigenId, CuentaDestinoId, Monto, Tipo FROM Transacciones";
            using var cmd = new SqlCommand(query, conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                transacciones.Add(new Transaccion
                {
                    Numero = Convert.ToInt32(reader["Numero"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    CuentaOrigenId = Convert.ToInt32(reader["CuentaOrigenId"]),
                    CuentaDestinoId = Convert.ToInt32(reader["CuentaDestinoId"]),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Tipo = reader["Tipo"].ToString()
                });
            }

            return transacciones;
        }

        public IEnumerable<object> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}
