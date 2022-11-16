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
    /// Lógica interna para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        Usuario usuarioLogado = null;
        public Menu(Usuario _usuarioLogado)
        {
            InitializeComponent();
            usuarioLogado = _usuarioLogado;
            Data();

        }

        private void btnProduto(object sender, RoutedEventArgs e)
        {
            if(usuarioLogado.tipo == "Gerente")
            {
                GerenciarProduto gerenciarProduto = new GerenciarProduto();
                gerenciarProduto.ShowDialog();
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemSemPermissaoProdutos();
            }

        }
        private void btnUsuario(object sender, RoutedEventArgs e)
        {
            if (usuarioLogado.tipo == "Gerente")
            {
                GerenciarUsuario gerenciarUsuario = new GerenciarUsuario();
                gerenciarUsuario.ShowDialog();
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemSemPermissaoUsuarios();
            }
        }

        private void btnVenda(object sender, RoutedEventArgs e)
        {
            VenderProduto venderproduto = new VenderProduto(usuarioLogado);
            venderproduto.ShowDialog();
        }
        
        private void btnSair(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }
        //Tá faltando só arrumar o nome que tem que puxar do login
        public void Data()
        {
            string data = DateTime.Now.ToString(" d MMMM 'de' yyyy");
            txt1.Text = $"Olá {usuarioLogado.nome}, hoje é dia {data}"; 
        }

        private void ClickMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
