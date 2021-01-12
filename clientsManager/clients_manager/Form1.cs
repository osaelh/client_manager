using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace clients_manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection("Data Source=ANDROID-87C5AZ0; Initial Catalog=clientDb; Integrated Security =true");
        DataSet DS = new DataSet();
        SqlDataAdapter DA;

        private void Form1_Load(object sender, EventArgs e)
        {
            DA = new SqlDataAdapter("Select * from clients",cnx);
            DA.Fill(DS,"clt");
            dataGridView1.DataSource = DS.Tables["clt"];
            for (int i = 0; i < DS.Tables["clt"].Rows.Count; i++)
            {
                comboBox.Items.Add(DS.Tables["clt"].Rows[i][1]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            comboBox.Items.Add(comboBox.Text);
            DataRow row = DS.Tables["clt"].NewRow();

            /* DataColumn dataColumn = DS.Tables["clt"].Columns.Add("id", typeof(Int32));
             dataColumn.AutoIncrement = true;
             dataColumn.AutoIncrementStep = 1;
             dataColumn.AutoIncrementSeed = 200;*/

            row[0] = (int.Parse(DS.Tables["clt"].Rows[DS.Tables["clt"].Rows.Count - 1][0].ToString()) + 1).ToString();
            row[1] = comboBox.Text;
            row[2] = txtAdress.Text;
            row[3] = txtCity.Text;
            DS.Tables["clt"].Rows.Add(row);
            cleaner();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in DS.Tables["clt"].Rows)
            {
                if (txtId.Text == row[0].ToString())
                {
                    row.Delete();
                }

                //int p = -1;


                //for (int i = 0; i < DS.Tables["clt"].Rows.Count; i++)
                //{
                //    if (txtId.Text== DS.Tables["clt"].Rows[i][0].ToString())
                //    {
                //        p = i;
                //    }
                //}
                //if (p== -1)
                //{
                //    MessageBox.Show("client not found");
                //}
                //else
                //{
                //    DS.Tables["clt"].Rows[p].Delete();
                //    MessageBox.Show("client deleted sucsesfully");
                //}
            }
            }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int p = -1;
            for (int i = 0; i < DS.Tables["clt"].Rows.Count; i++)
            {
                if (txtId.Text == DS.Tables["clt"].Rows[i][0].ToString())
                {
                    p = i;
                }
            }
            if (p == -1)
            {
                MessageBox.Show("client not found");
            }
            else
            {
                DS.Tables["clt"].Rows[p][0]=int.Parse(txtId.Text);
                DS.Tables["clt"].Rows[p][1] = comboBox.Text;
                DS.Tables["clt"].Rows[p][2] = txtAdress.Text;
                DS.Tables["clt"].Rows[p][3] = txtCity.Text;
                MessageBox.Show("client modified sucsesfully");
            }
            cleaner();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(DA);
                DA.Update(DS, "clt");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void cleaner()
        {
            comboBox.Text = null;
            txtId.Text = null;
            txtAdress.Text = null;
            txtCity.Text = null;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            int p = -1;
            for (int i = 0; i < DS.Tables["clt"].Rows.Count; i++)
            {
                if (comboBox.Text == DS.Tables["clt"].Rows[i][1].ToString())
                {
                    p = i;
                }
            }
            if (p == -1)
            {
                MessageBox.Show("client not found");
            }
            else
            {
              
                txtId.Text = Convert.ToString(DS.Tables["clt"].Rows[p][0]);
                txtAdress.Text = Convert.ToString(DS.Tables["clt"].Rows[p][2]);
                txtCity.Text = Convert.ToString(DS.Tables["clt"].Rows[p][3]);

            }
        }
    }
}
