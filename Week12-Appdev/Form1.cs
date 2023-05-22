using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Week12_Appdev
{
    public partial class Form1 : Form
    {
        MySqlDataAdapter mySqlDataAdapter;
        MySqlConnection mySqlConnection;
        MySqlDataReader mySqlDataReader;
        MySqlCommand mySqlCommand;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();
        string sqlQuery;
        string connection = "server=localhost;uid=root;pwd=;database=premier_league";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mySqlConnection = new MySqlConnection(connection);
            dt1 = new DataTable();
            sqlQuery = "SELECT nation, nationality_id FROM nationality;";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "nation";
            comboBox1.ValueMember = "nationality_id";

            dt2 = new DataTable();
            sqlQuery = "SELECT * FROM team;";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt2);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "team_name";
            comboBox2.ValueMember = "team_id";

            dt3 = new DataTable();
            sqlQuery = "SELECT team_name, team_id FROM team;";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt3);
            comboBox4.DataSource = dt3;
            comboBox4.DisplayMember = "team_name";
            comboBox4.ValueMember = "team_id";

            dt8 = new DataTable();
            sqlQuery = "SELECT team_name, team_id FROM team;";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt8);
            comboBox3.DataSource = dt8;
            comboBox3.DisplayMember = "team_name";
            comboBox3.ValueMember = "team_id";

            dt5 = new DataTable();
            string command = $"select m.manager_name, n.nation, m.birthdate, m.manager_id from manager m left join nationality n on m.nationality_id = n.nationality_id where working = '0';";
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt5);
            dataGridView2.DataSource = dt5;

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dt6 = new DataTable();
            sqlQuery = $"select m.manager_name, t.team_name, m.birthdate, n.nation from manager m, team t, nationality n where m.working = '1' and m.manager_id = t.manager_id and n.nationality_id = m.nationality_id and team_id = '{comboBox2.SelectedValue}';";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt6);
            dataGridView1.DataSource = dt6;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt7 = new DataTable();
            sqlQuery = $"select p.player_id, p.team_number, p.player_name, n.nation, p.playing_pos, p.height, p.weight, p.birthdate, t.team_name FROM player p, nationality n, team t where p.nationality_id = n.nationality_id and t.team_id = p.team_id and p.team_id = '" + comboBox3.SelectedValue + "' and p.status = 1; ";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt7);
            dataGridView3.DataSource = dt7;

        }

        private void Execute(string command)
        {
            try
            {
                mySqlConnection.Open();
                mySqlCommand = new MySqlCommand(command, mySqlConnection);
                mySqlDataReader = mySqlCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                mySqlConnection.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string playerid = textBox1.Text;
            string name = textBox2.Text;
            string number = textBox3.Text;
            string position = textBox4.Text;
            string height = textBox5.Text;
            string weight = textBox6.Text;
            string tgl = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string command = $"insert into player values ('{playerid}' ,'{number}', '{name}', '{comboBox1.SelectedValue}', '{position}', '{height}', '{weight}', '{tgl}', '{comboBox2.SelectedValue}', '1', '0');";
            Execute(command);

        }

        private void menghapusPemain()
        {
            dt9 = new DataTable();
            sqlQuery = "select p.player_id, p.team_number, p.player_name, n.nation, p.playing_pos, p.height, p.weight, p.birthdate, t.team_name FROM player p, nationality n, team t where p.nationality_id = n.nationality_id and t.team_id = p.team_id and p.team_id = '" + comboBox3.SelectedValue + "' and p.status = 1; ";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt9);
            dataGridView3.DataSource = dt9;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count >= 12)
            {
                string pemain = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                string delete = $"update player set status = 0 where player_id = '{pemain}'";
                try
                {
                    mySqlConnection.Open();
                    mySqlCommand = new MySqlCommand(delete, mySqlConnection);
                    mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                    mySqlDataReader = mySqlCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mySqlConnection.Close();
                    menghapusPemain();
                }
            }
            else
            {
                MessageBox.Show("Harus melebihi dari > 11 ya");
            }
        }
        private void updatepertama()
        {
            dt10 = new DataTable();
            sqlQuery = $"select m.manager_name, t.team_name, m.birthdate, n.nation from manager m, team t, nationality n where m.working = '1' and m.manager_id = t.manager_id and n.nationality_id = m.nationality_id and team_id = '{comboBox2.SelectedValue}';";
            mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt10);
            dataGridView1.DataSource = dt10;

        }

        private void updatekedua()
        {
            dt11 = new DataTable();
            string command = $"select m.manager_name, n.nation, m.birthdate, m.manager_id from manager m left join nationality n on m.nationality_id = n.nationality_id where working = '0';";
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            mySqlDataAdapter.Fill(dt11);
            dataGridView2.DataSource = dt11;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[rowIndex];
            string pertama = row.Cells[0].Value.ToString();
            string kedua = row.Cells[3].Value.ToString();
            string command = $"update manager set working = '1' where manager_name = '{pertama}';";
            string command2 = $"update team set manager_id = '{kedua}' where team_id = '{comboBox2.SelectedValue}';";
            Execute(command);
            Execute(command2);
            updatekedua();
            int rowIndex1 = dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow row1 = dataGridView1.Rows[rowIndex1];
            string ketiga = row1.Cells[0].Value.ToString();
            string command3 = $"update manager set working = '0' where manager_name = '{ketiga}';";
            Execute(command3);
            updatepertama();
        }
    }
        
}

