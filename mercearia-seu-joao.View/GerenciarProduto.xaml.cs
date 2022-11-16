using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mercearia_seu_joao.View
{
    /// <summary>
    /// Lógica interna para GerenciarUsuario.xaml
    /// </summary>
    public partial class GerenciarProduto : Window
    {
        List<Produto> listaDeProdutos = new List<Produto>();

        public GerenciarProduto()
        {
            InitializeComponent();
            AtualizaDataGrid();
        }

        private void AdicionaProduto()
        {
            string dataAdicionado = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bool foiInserido = ConsultasProduto.InserirProduto(
                txtNome.Text,
                txtQntd.Text,
                txtPreco.Text,
                txtFornecedor.Text,
                dataAdicionado
                );
            if (foiInserido == true)
            {
                CaixaDeMensagens.ExibirMensagemProdutoCadastrado();
                LimpaTodosCampos();
                AtualizaDataGrid();
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemErroCadastroProduto();
            }

        }

        private void AtualizaDataGrid()
        {
            listaDeProdutos.Clear();
            listaDeProdutos = ConsultasProduto.ObterTodosProdutos();
            dgvProdutos.ItemsSource = listaDeProdutos;
            dgvProdutos.Items.Refresh();
        }

        private bool VerificaCampos()
        {
            if (txtNome.Text != "" && txtQntd.Text != "" && txtPreco.Text != "" && txtFornecedor.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClickSair(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void clickMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ClickExcluir(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                string dataExclusao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja excluir o produto id:{id} ?",
                    "Excluir Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    bool foiExcluido = ConsultasProduto.ExcluirProduto(id, dataExclusao);

                    if (foiExcluido == true)
                    {
                        CaixaDeMensagens.ExibirMensagemExclusaoProduto();
                        AtualizaDataGrid();
                        LimpaTodosCampos();
                    }
                    else
                    {
                        CaixaDeMensagens.ExibirMensagemErroExclusaoProduto();
                    }

                }
            }
        }

        private void ClickLimpar(object sender, RoutedEventArgs e)
        {
            LimpaTodosCampos();
        }

        private void ClickNovo(object sender, RoutedEventArgs e)
        {
            if (VerificaCampos() == true)
            {
                if (txtId.Text == "")
                {
                    AdicionaProduto();
                }
                else
                {
                    CaixaDeMensagens.ExibirMensagemIdProdutoCampoPreenchido();
                }
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemPreencherCampos();
            }
        }

        private void LimpaTodosCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtPreco.Text = "";
            txtQntd.Text = "";
            txtFornecedor.Text = "";
        }

        private void ClickAlterar(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                string dataAlteracao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja alterar o produto id:{id} ?",
                    "Alterar o Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    bool foiAtualizado = ConsultasProduto.AtualizarProduto(
                        id,
                        txtNome.Text,
                        txtQntd.Text,
                        txtPreco.Text,
                        txtFornecedor.Text,
                        dataAlteracao
                    );

                    if (foiAtualizado == true)
                    {
                        CaixaDeMensagens.ExibirMensagemAtualizacaoProduto();

                        LimpaTodosCampos();
                        AtualizaDataGrid();
                    }
                    else
                    {
                        CaixaDeMensagens.ExibirMensagemErroAtualizarProduto();
                    }

                    LimpaTodosCampos();
                }
            }
        }

        private void PegarItemNoGrid(object sender, MouseButtonEventArgs e)
        {
            Produto produto = (Produto)dgvProdutos.SelectedItem;
            txtId.Text = produto.id.ToString();
            txtNome.Text = produto.nome;
            txtQntd.Text = produto.quantidade.ToString();
            txtPreco.Text = produto.precoUni.ToString();
            txtFornecedor.Text = produto.fornecedor;
        }
    }
}
