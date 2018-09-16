using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CrudWebForm
{
    public partial class Form1 : Form
    {
        string cs = "";
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cs = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            DisplayData();
        }

        private void DisplayData()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "select * from Student";
            SqlCommand cmd = new SqlCommand(query,con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvShow.DataSource = dt;
            con.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "insert Student values('"+nameTxt.Text+"','"+phoneTxt.Text+"','"+emailTxt.Text+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Inserted");
            
            DisplayData();
            Clear();
        }

        private void Clear()
        {
            nameTxt.Text = "";
            phoneTxt.Text = "";
            emailTxt.Text = "";
            searchTxt.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "update Student set Name='" + nameTxt.Text + "',Phone='" + phoneTxt.Text + "',Email='" + emailTxt.Text + "' where Id='"+searchTxt.Text+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Updated");

            DisplayData();
            Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "select * from Student where Id='" + searchTxt.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                nameTxt.Text = dr[1].ToString();
                phoneTxt.Text = dr[2].ToString();
                emailTxt.Text = dr[3].ToString();
            }
            else
            {
                MessageBox.Show("Insert an valid Id");
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "delete from Student where Id='" + searchTxt.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted");

            DisplayData();
            Clear();
        }
    }
}
