using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFDotNetCoreMysqlCRUD
{
    public partial class Form1 : Form
    {

        //aqui vamos criar as informações para conectar nosso banco
        private MySqlConnection Conexao;
        private string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                // crias a conexão com mysql
                Conexao = new MySqlConnection(data_source);

                //executar o comando insert
                string sql = $"INSERT INTO contato (nome,telefone,email) VALUE"+"('"+txtNome.Text+" ','"+txtTelefone.Text+" ','"+txtEmail.Text+" ')";


                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                //executar comando sql
                comando.ExecuteReader();
                //informar mensagem caso tenha dado certo
                MessageBox.Show("Cadastro inserido com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string q = " '%" + txt_buscar.Text + "%'";
                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * FROM contato WHERE nome LIKE"+q+" OR email LIKE"+q;
                //SELECT * FROM CONTATO WHERE NOME LIKE %JOSE%

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();

                //usando o mysqldatareader para executar
                MySqlDataReader reader = comando.ExecuteReader();

                //após armazenar as informações de busca, vamos limpar o campo
                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    //criando elemento que será a linha
                    var linhaListView = new ListViewItem(row);
                    //pegando o listview e acrescentando a linha que acabamos de criar
                    lst_contatos.Items.Add(linhaListView);

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}
