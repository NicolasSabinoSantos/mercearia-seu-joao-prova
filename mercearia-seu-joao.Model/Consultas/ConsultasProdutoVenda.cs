using MySqlConnector;
using System;
using System.Collections.Generic;

public class ConsultasProdutoVenda
{
    //Retorna os dados de um produto - READ
    public static List<Produto> BuscaProduto(int id)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        List<Produto> DadosDoProduto = new List<Produto>();

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    SELECT * FROM Produto WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();

            while (leitura.Read())
            {
                Produto produto = new Produto();
                produto.id = leitura.GetInt32("id");
                produto.nome = leitura.GetString("nome");
                produto.quantidade = leitura.GetInt32("quantidade");
                produto.precoUni = leitura.GetDouble("precoUnitario");

                DadosDoProduto.Add(produto);
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
        return DadosDoProduto;
    }

    public static string RetornaNomePeloId(int id)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        string nome = String.Empty;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    SELECT nome FROM Produto WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();

            while (leitura.Read())
            {
                nome = leitura.GetString("nome");
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
        return nome;
    }

    public static double RetornaPrecoPeloId(int id)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        double preco = 0;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    SELECT precoUnitario FROM Produto WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();

            while (leitura.Read())
            {
                preco = leitura.GetDouble("precoUnitario");
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
        return preco;
    }
    //Obtém quantidade de cada produto pelo id
    public static int ObterQuantidadePeloId(int id)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        int quantidade = 0;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    SELECT quantidade FROM Produto WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();

            while (leitura.Read())
            {
                quantidade = leitura.GetInt32("quantidade");
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
        return quantidade;
    }
    //Retirar a quantidade do sistema
    public static bool RetirarQuantidadeDoSistemaPeloId(int id, int quantidadeFinal)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiRetirado = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    UPDATE  Produto 
                    SET quantidade = @quantidadeFinal 
                    WHERE id = @id";
            comando.Parameters.AddWithValue("@quantidadeFinal", quantidadeFinal);
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();
            foiRetirado = true;
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
        return foiRetirado;
    }

    //Cadastrar venda no banco de dados
    public static bool InserirVenda(string nome, int idProduto, int idUsuario, DateTime dataVenda, int quantidade, double valorTotal)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiInserido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    INSERT INTO Venda (nome, idProduto, idUsuario, dataVenda, quantidade, valorTotal)
                    VALUES (@nome, @idProduto, @idUsuario, @dataVenda, @quantidade, @valorTotal)
                    ";
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@idProduto", idProduto);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.Parameters.AddWithValue("@dataVenda", dataVenda);
            comando.Parameters.AddWithValue("@quantidade", quantidade);
            comando.Parameters.AddWithValue("@valorTotal", valorTotal);

            var leitura = comando.ExecuteReader();
            foiInserido = true;

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
        return foiInserido;


    }

    public static List<Venda> InformacoesUsuarioLogado(string email)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        List<Venda> DadosDoUsuarioLogado = new List<Venda>();

        int id = 0;
        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                    SELECT * FROM Usuario WHERE email = @email";
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();

            while (leitura.Read())
            {
                Venda venda = new Venda();
                venda.idUsuario = leitura.GetInt32("id");

                DadosDoUsuarioLogado.Add(venda);
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
        return DadosDoUsuarioLogado;
    }

}



