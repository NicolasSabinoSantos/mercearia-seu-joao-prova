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
    public partial class GerenciarUsuario : Window
    {
        List<Usuario> listaDeUsuarios = new List<Usuario>();

        public GerenciarUsuario()
        {
            InitializeComponent();
            AtualizaDataGrid();
        }

        private void AdicionaUsuario()
        {
            string dataAdicionado = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bool foiInserido = ConsultasUsuario.InserirUsuario(
                txtNome.Text,
                txtEmail.Text,
                txtSenha.Password,
                txtSenha_C.Password,
                BoxTipo.Text,
                dataAdicionado
                );
            if(foiInserido == true)
            {
                CaixaDeMensagens.ExibirMensagemUsuarioCadastrado();
                LimpaTodosCampos();
                AtualizaDataGrid();
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemErroCadastro();
            }
            
        }

        private void AtualizaDataGrid()
        {
            listaDeUsuarios.Clear();
            listaDeUsuarios = ConsultasUsuario.ObterTodosUsuarios();
            dgvUsuarios.ItemsSource = listaDeUsuarios;
            dgvUsuarios.Items.Refresh();
        }

        private bool VerificaCampos()
        {
            if(txtNome.Text != "" && txtEmail.Text != "" && txtSenha.Password != "" && txtSenha_C.Password != "" && BoxTipo.Text != "")
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
            if (txtID.Text != "")
            {
                int id = int.Parse(txtID.Text);
                string dataExclusao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja excluir o usuario id:{id} ?",
                    "Excluir Usuario",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);


                if(result == MessageBoxResult.Yes)
                {
                    bool foiExcluido = ConsultasUsuario.ExcluirUsuario(id, dataExclusao);

                    if(foiExcluido == true)
                    {
                        CaixaDeMensagens.ExibirMensagemExclusao();
                        AtualizaDataGrid();
                        LimpaTodosCampos();
                    }
                    else
                    {
                        CaixaDeMensagens.ExibirMensagemErroExclusao();
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
                if (txtID.Text == "")
                {
                    if (txtSenha.Password != txtSenha_C.Password)
                    {
                        CaixaDeMensagens.ExibirMensagemErroConfirmacaoSenha();
                    }
                    else
                    {
                        AdicionaUsuario();
                    }
                }
                else
                {
                    CaixaDeMensagens.ExibirMensagemIdCampoPreenchido();
                }
            }
            else
            {
                CaixaDeMensagens.ExibirMensagemPreencherCampos();             
            }
        }

        private void LimpaTodosCampos()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Password = "";
            txtSenha_C.Password = "";
            BoxTipo.Text = "";
        }

        private void ClickAlterar(object sender, RoutedEventArgs e)
        {
            if (txtID.Text != "")
            {
                int id = int.Parse(txtID.Text);
                string dataAlteracao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja alterar o usuario id:{id} ?",
                    "Alterar o Usuario",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    bool foiAtualizado = ConsultasUsuario.AtualizarUsuario(
                        id,
                        txtNome.Text,
                        txtEmail.Text,
                        txtSenha.Password,
                        BoxTipo.Text,
                        dataAlteracao
                    );

                    if(foiAtualizado == true)
                    {
                        CaixaDeMensagens.ExibirMensagemAtualizacaoUsuario();

                        LimpaTodosCampos();
                        AtualizaDataGrid();
                    }
                    else
                    {
                        CaixaDeMensagens.ExibirMensagemErroAtualizarUsuario();
                    }

                    LimpaTodosCampos();
                }
            }
        }

        private void PegarItemNoGrid(object sender, MouseButtonEventArgs e)
        {
            Usuario usuario = (Usuario)dgvUsuarios.SelectedItem;
            txtID.Text = usuario.id.ToString();
            txtNome.Text = usuario.nome;
            txtEmail.Text = usuario.email;
            txtSenha.Password = usuario.senha;
            txtSenha_C.Password = usuario.senha;
            BoxTipo.Text = usuario.tipo;
        }   
    }
}
