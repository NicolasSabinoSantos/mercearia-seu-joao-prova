using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class CaixaDeMensagens
{
    // CAIXAS DE MENSAGENS DO GERENCIAR USUARIOS

    public static void ExibirMensagemIdCampoPreenchido()
    {
        MessageBoxResult result2 = MessageBox.Show(
                        "Limpe o campo Id para poder cadastrar o usuario!",
                        "Atenção",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
    }

    public static void ExibirMensagemPreencherCampos()
    {
        MessageBoxResult result = MessageBox.Show(
                    "Preencha todos os campos!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemUsuarioCadastrado()
    {
        MessageBoxResult result = MessageBox.Show(
                    "Novo usuario Cadastrado!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroCadastro()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível cadastrar o usuario, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemExclusao()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Usuario excluido!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroExclusao()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível excluir o usuario, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemErroConfirmacaoSenha()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Confirmação de senha não correspondente!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemAtualizacaoUsuario()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Usuario Atualizado!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroAtualizarUsuario()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível atualizar o usuario, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    // CAIXAS DE MENSAGENS DO LOGIN

    public static void ExibirMensagemErroLogin()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Email ou senha incorretos!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemEsqueceuSenha()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Contate o seu Gerente!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    //CAIXAS DE MENSAGENS DO GERENCIAR PRODUTOS

    public static void ExibirMensagemIdProdutoCampoPreenchido()
    {
        MessageBoxResult result2 = MessageBox.Show(
                        "Limpe o campo Id para poder cadastrar o produto!",
                        "Atenção",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
    }

    public static void ExibirMensagemProdutoCadastrado()
    {
        MessageBoxResult result = MessageBox.Show(
                    "Novo produto Cadastrado!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroCadastroProduto()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível cadastrar o produto, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemExclusaoProduto()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Produto excluido!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroExclusaoProduto()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível excluir o produto, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemAtualizacaoProduto()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Produto Atualizado!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
    }

    public static void ExibirMensagemErroAtualizarProduto()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Não foi possível atualizar o usuario, tente novamente mais tarde!",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    //MENSAGENS MENU

    public static void ExibirMensagemSemPermissaoUsuarios()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Apenas Gerentes podem gerenciar os usuarios!",
                    "Aviso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }

    public static void ExibirMensagemSemPermissaoProdutos()
    {
        MessageBoxResult result2 = MessageBox.Show(
                    "Apenas Gerentes podem gerenciar os Produtos!",
                    "Aviso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
    }









}
