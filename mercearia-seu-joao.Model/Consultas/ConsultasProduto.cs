using MySqlConnector;
using System;
using System.Collections.Generic;

public class ConsultasProduto
{
    public static bool InserirProduto(string nome, string qntd, string preco, string fornecedor, string dataAdicionado)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiInserido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Produto (nome, quantidade, precoUnitario, fornecedor, dataAdicionado) 
                VALUES (@nome,@qntd,@preco,@fornecedor,@dataAdicionado)";
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@qntd", qntd);
            comando.Parameters.AddWithValue("@preco", preco);
            comando.Parameters.AddWithValue("@fornecedor", fornecedor);
            comando.Parameters.AddWithValue("@dataAdicionado", dataAdicionado);
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

    public static bool ExcluirProduto(int id, string dataExclusao)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiExcluido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Produto
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

    public static bool AtualizarProduto(int id, string nome, string qntd, string preco, string fornecedor, string dataAlteracao)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiAtualizado = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Produto 
                SET nome = @nome, quantidade = @qntd, dataAlteracao = @dataAlteracao, precoUnitario = @preco, fornecedor = @fornecedor 
                WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@dataAlteracao", dataAlteracao);
            comando.Parameters.AddWithValue("@qntd", qntd);
            comando.Parameters.AddWithValue("@preco", preco);
            comando.Parameters.AddWithValue("@fornecedor", fornecedor);
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

    public static List<Produto> ObterTodosProdutos()
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        List<Produto> listaDeProdutos = new List<Produto>();

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT * FROM Produto WHERE dataExclusao is null;";
            var leitura = comando.ExecuteReader();
            while (leitura.Read())
            {
                Produto produto = new Produto();
                produto.id = leitura.GetInt32("id");
                produto.nome = leitura.GetString("nome");
                produto.quantidade = leitura.GetInt32("quantidade");
                produto.precoUni = leitura.GetDouble("precoUnitario");
                produto.fornecedor = leitura.GetString("fornecedor");

                listaDeProdutos.Add(produto);

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

        return listaDeProdutos;

    }

}

