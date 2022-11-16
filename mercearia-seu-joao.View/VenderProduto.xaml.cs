using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mercearia_seu_joao.View
{
    /// <summary>
    /// Lógica interna para VenderProduto.xaml
    /// </summary>
    public partial class VenderProduto : Window
    {
        Usuario usuarioLogado = null;

        double valorFinal = 0;
        double PrecoProduto;
        bool idValido = false;
        //Base de dados da memória
        List<Produto> listaDeProdutos = new List<Produto>(); //Lista de produtos
        List<Venda> listaDeVendas = new List<Venda>(); //Lista das vendas
        public VenderProduto(Usuario _usuarioLogado)
        {
            InitializeComponent();
            usuarioLogado = _usuarioLogado;

        }


        private void Buscar(object sender, RoutedEventArgs e)
        {
            RetornandoValorPreco();

            if (Tbx_ID.Text != "")
            {
                int IdConsulta = int.Parse(Tbx_ID.Text);
                ConsultasProdutoVenda.BuscaProduto(IdConsulta);
                Tbx_Nome.Text = ConsultasProdutoVenda.RetornaNomePeloId(IdConsulta);
                if(Tbx_Nome.Text == "")
                {
                    MessageBoxResult result = MessageBox.Show("Não foi possível encontrar o produto, selecione um ID válido!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                    Tbx_Qtd.Text = "";
                    Tbx_ID.Text = "";
                }
                else
                {
       
                    Tbx_Qtd.IsReadOnly = false;  
                    idValido = true;
                }

            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Não foi possível encontrar o produto, selecione um ID válido!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Adicionar(object sender, RoutedEventArgs e)
        {
            if(Tbx_Preco.Text == "" || Tbx_ID.Text == "" || Tbx_Nome.Text == "" || Tbx_Qtd.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Não foi possível realizar a operação, preencha os campos corretamente!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (int.Parse(Tbx_Qtd.Text) > ConsultasProdutoVenda.ObterQuantidadePeloId(int.Parse(Tbx_ID.Text)))
                {
                    MessageBoxResult result = MessageBox.Show("Quantidade indisponível!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Tbx_Qtd.Text == "0")
                {
                    MessageBoxResult result = MessageBox.Show("Informe uma quantidade acima de 0!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Produto produto = new Produto();
                    produto.id = int.Parse(Tbx_ID.Text);
                    produto.nome = Tbx_Nome.Text;
                    produto.precoUni = ConsultasProdutoVenda.RetornaPrecoPeloId(int.Parse(Tbx_ID.Text));
                    produto.precoTotal = PrecoProduto;
                    produto.quantidade = int.Parse(Tbx_Qtd.Text);

                    listaDeProdutos.Add(produto);
                    dgvListaCompra.ItemsSource = listaDeProdutos;
                    dgvListaCompra.Items.Refresh();

                    valorFinal += produto.precoTotal;
                    Tb_ValorFinal.Text = $"Valor Final = R${valorFinal}";

                    //Se chegou até aqui, adiciona o produto à lista de vendas
                    Venda venda = new Venda();
                    venda.nome = produto.nome;
                    venda.idProduto = produto.id;
                    venda.idUsuario = usuarioLogado.id;
                    venda.quantidade = produto.quantidade;
                    venda.valorTotal = produto.precoTotal;

                    listaDeVendas.Add(venda);
                }
                
            }

        }


        private void Vender(object sender, RoutedEventArgs e)
        {
            DateTime dataAtual = DateTime.Now;

            if (dgvListaCompra.ItemsSource != null)
            {
                for (int i = 0; i < listaDeVendas.Count; i++)
                {
                    //Insere a venda na tabela de vendas
                    listaDeVendas[i].dataVenda = dataAtual;
                    ConsultasProdutoVenda.InserirVenda(listaDeVendas[i].nome, listaDeVendas[i].idProduto, listaDeVendas[i].idUsuario, listaDeVendas[i].dataVenda, listaDeVendas[i].quantidade, listaDeVendas[i].valorTotal);

                    //Retira a quantidade dos produtos no estoque
                    int qtdFinal = ConsultasProdutoVenda.ObterQuantidadePeloId(listaDeVendas[i].idProduto) - listaDeVendas[i].quantidade;
                    ConsultasProdutoVenda.RetirarQuantidadeDoSistemaPeloId(listaDeVendas[i].idProduto, qtdFinal);

                }
                //Mostra um pop-up de venda concluída
                MessageBoxResult result = MessageBox.Show("Venda realizada", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);

                listaDeProdutos.Clear();
                listaDeVendas.Clear();
                dgvListaCompra.ItemsSource = null;
                dgvListaCompra.Items.Refresh();
                LimparVendas();
                Tbx_Qtd.IsReadOnly = true;
                valorFinal = 0;
                Tb_ValorFinal.Text = $"Valor Final = R${valorFinal,0}";

            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Não foi possível realizar a venda, verifique as informações corretamente!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RetornaPreco(object sender, KeyEventArgs e)
        {
            RetornandoValorPreco();
        }

        private void RetornandoValorPreco()
        {
            if (idValido && Tbx_Qtd.Text != "" && Tbx_Nome.Text != "" && Tbx_ID.Text != "")
            {
                int IdConsulta = int.Parse(Tbx_ID.Text);
                double Preco = ConsultasProdutoVenda.RetornaPrecoPeloId(IdConsulta);

                PrecoProduto = int.Parse(Tbx_Qtd.Text) * Preco;
                Tbx_Preco.Text = $"R${PrecoProduto}";

            }
            else
            {
                Tbx_Preco.Text = "";
            }
        }

        private bool confereCampos()
        {
            if(idValido && Tbx_Qtd.Text != "" && Tbx_Nome.Text != ""  && Tbx_ID.Text != "" && Tbx_Preco.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Fechar(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Método para permitir apenas números no TextBox
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Limpar(object sender, RoutedEventArgs e)
        {
            LimparVendas();
        }

        private void LimparVendas()
        {
            Tbx_ID.Text = "";
            Tbx_Nome.Text = "";
            Tbx_Qtd.Text = "";
            Tbx_Preco.Text = "";
        }
  
        private void AlterarProduto(object sender, MouseButtonEventArgs e)
        {
            //Obter o index da linha que o usuário escolheu
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                var index = dataGrid.SelectedIndex;
                
                for(int i = -1; i < listaDeProdutos.Count; i++)
                {
                    if (index == i)
                    {
                        //Abrindo a pagina wpf ItemDaLista
                        var itemDaLista = new ItemDaLista();
                        itemDaLista.Tbx_id.Text = listaDeProdutos[i].id.ToString();
                        itemDaLista.Tbx_nome.Text = listaDeProdutos[i].nome;
                        itemDaLista.Tbx_qtd.Text = listaDeProdutos[i].quantidade.ToString();
                        itemDaLista.Btn_Alterar.Click += Alterar;
                        itemDaLista.Btn_Excluir.Click += Excluir;
                        itemDaLista.Show();

                        //Evento clicked do botão Alterar do ItemDaLista
                        void Alterar(object sender, RoutedEventArgs e)
                        {
                            //Removendo o valor errado no valor final da compra
                            valorFinal -= listaDeProdutos[index].precoTotal; 
                            //Mudando a quantidade e preço total do produto ao clicar no botão alterar
                            listaDeProdutos[index].quantidade = int.Parse(itemDaLista.Tbx_qtd.Text);
                            listaDeProdutos[index].precoTotal = listaDeProdutos[index].quantidade * listaDeProdutos[index].precoUni;
                            //Mudando a quantidade e preço total na lista de venda
                            listaDeVendas[index].quantidade = int.Parse(itemDaLista.Tbx_qtd.Text);
                            listaDeVendas[index].valorTotal = listaDeProdutos[index].quantidade * listaDeProdutos[index].precoUni;

                            //Atualizando o datagrid
                            dgvListaCompra.Items.Refresh();

                            //Adicionando o valor certo no valor final da compra
                            valorFinal += listaDeProdutos[index].precoTotal;
                            //Mostrando o valor certo na tela
                            Tb_ValorFinal.Text = $"Valor Final = R${valorFinal}";
                            //Fechando a tela da alteração
                            itemDaLista.Close();
                        }

                        //Evento clicked do botão Excluir do ItemDaLista
                        void Excluir(object sender, RoutedEventArgs e)
                        {
                            //Removendo o valor errado no valor final da compra
                            valorFinal -= listaDeProdutos[index].precoTotal;
                            //Mostrando o valor certo na tela
                            Tb_ValorFinal.Text = $"Valor Final = R${valorFinal}";
                            //Remove o item tanto da lista de produtos quanto da lista de vendas
                            listaDeProdutos.RemoveAt(index);
                            listaDeVendas.RemoveAt(index);

                            //Atualiza o datagrid
                            dgvListaCompra.ItemsSource = listaDeProdutos;
                            dgvListaCompra.Items.Refresh();
                            //Fechando a tela da alteração
                            itemDaLista.Close();
                        }

                    }
                }
                
            }

        }

       


    }
}
