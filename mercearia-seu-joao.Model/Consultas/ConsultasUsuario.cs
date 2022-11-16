using MySqlConnector;
using System;
using System.Collections.Generic;

public class ConsultasUsuario
{
    public static bool InserirUsuario(string nome, string email, string senha, string senhaC, string tipo, string dataAdicionado)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiInserido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Usuario (tipoUsuario, nome, email, senha, dataAdicionado) 
                VALUES (@tipo,@nome,@email,@senha,@dataAdicionado)";
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);
            comando.Parameters.AddWithValue("@dataAdicionado",dataAdicionado);
            var leitura = comando.ExecuteReader();
            foiInserido = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if(conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiInserido;
    }

    public static bool ExcluirUsuario(int id, string dataExclusao)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiExcluido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Usuario 
                SET dataExclusao = @dataExclusao
                WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@dataExclusao", dataExclusao);

            var leitura = comando.ExecuteReader();
            foiExcluido = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiExcluido;
    }

    public static bool AtualizarUsuario(int id, string nome, string email, string senha, string tipo, string dataAlteracao)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiAtualizado = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Usuario 
                SET tipoUsuario = @tipo, nome = @nome, dataAlteracao = @dataAlteracao, email = @email, senha = @senha 
                WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@dataAlteracao", dataAlteracao);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);
            var leitura = comando.ExecuteReader();
            foiAtualizado = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiAtualizado;
    }

    public static List<Usuario> ObterTodosUsuarios()
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        List<Usuario> listaDeUsuarios = new List<Usuario>();

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT * FROM Usuario WHERE dataExclusao is null;";
            var leitura = comando.ExecuteReader();
            while (leitura.Read())
            {
                Usuario usuario = new Usuario();
                usuario.id = leitura.GetInt32("id");
                usuario.tipo = leitura.GetString("tipoUsuario");
                usuario.nome = leitura.GetString("nome");
                usuario.email = leitura.GetString("email");
                usuario.senha = leitura.GetString("senha");
                
                listaDeUsuarios.Add(usuario);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return listaDeUsuarios;

    }

    public static Usuario InserirLogin(string email, string senha)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        Usuario usuario = null;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT * FROM Usuario WHERE email = @email and senha = @senha";
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);
            var leitura = comando.ExecuteReader();
            while (leitura.Read())
            {
                usuario = new Usuario();
                usuario.id = leitura.GetInt32("id");
                usuario.nome = leitura.GetString("nome");
                usuario.tipo = leitura.GetString("tipoUsuario");
                usuario.email = leitura.GetString("email");
                usuario.senha = leitura.GetString("senha");

                return usuario;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return usuario;
    }


}

