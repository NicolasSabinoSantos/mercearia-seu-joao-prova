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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mercearia_seu_joao.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
       
        }

        private void ClickEntrar(object sender, RoutedEventArgs e)
        {
            string email = String.Empty;
            string senha = String.Empty;

            if (txtEmail.Text != "" && txtSenha.Password != "")
            {
                email = txtEmail.Text;
                senha = txtSenha.Password;

                Usuario usuarioLogado = ConsultasUsuario.InserirLogin(
                    email,
                    senha
                );
                if (usuarioLogado != null)
                {
                    
                    Menu FrmMenu = new Menu(usuarioLogado);
                    FrmMenu.Show();
                    Close();

                }
                else
                {
                    CaixaDeMensagens.ExibirMensagemErroLogin();
                }

            }
            else
            {
                CaixaDeMensagens.ExibirMensagemPreencherCampos();
            }
        }

        private void ClickSair(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClickMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ClickEsqueceu(object sender, MouseButtonEventArgs e)
        {
            CaixaDeMensagens.ExibirMensagemEsqueceuSenha();
        }
    }
}
