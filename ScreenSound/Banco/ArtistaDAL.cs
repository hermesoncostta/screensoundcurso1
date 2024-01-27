using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
namespace ScreenSound.Banco;

internal class ArtistaDAL
{

    public IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Artistas";

        SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string nomeArtista = Convert.ToString(reader["Nome"]);
            string bioArtista = Convert.ToString(reader["Bio"]);
            int idArtista = Convert.ToInt32(reader["Id"]);

            Artista artista = new Artista(nomeArtista, bioArtista) { Id = idArtista };
            lista.Add(artista);

        }
        return lista;

    }

    public void Adicionar(Artista artista)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine(retorno);
    }

    public void Atualizar(Artista artista)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = $"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@bio", artista.Bio);
        command.Parameters.AddWithValue("@id", artista.Id);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");

    }

    public void Deletar(Artista artista)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();
            
        string sql = $"DELETE FROM Artistas WHERE Id = @id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", artista.Id);

        int retorno = command.ExecuteNonQuery();

        Console.WriteLine($"Linhas afetadas: {retorno}");


    }

}
