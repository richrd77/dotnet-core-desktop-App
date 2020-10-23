using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "comma seperated value (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    label1.Text = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                int rowIndex = 0, colIndex = 0;
                dt = new DataTable();
                foreach (string row in fileContent.Split(Environment.NewLine))
                {
                    DataRow dr = null;
                    colIndex = 0;
                    if (rowIndex != 0)
                    {
                        dr = dt.NewRow();
                    }
                    foreach (string col in row.Split(','))
                    {
                        if (rowIndex == 0)
                        {
                            dt.Columns.Add(col);
                        }
                        else
                        {
                            dr[colIndex] = col;
                        }
                        colIndex++;
                    }
                    rowIndex++;
                    colIndex = 0;
                    if(dr != null)
                    {
                        dt.Rows.Add(dr);
                    }
                }
                dataGridView1.DataSource = dt;
            }
        }
    }
}
