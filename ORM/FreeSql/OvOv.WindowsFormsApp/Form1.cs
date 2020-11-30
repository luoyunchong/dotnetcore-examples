using FreeSql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (WinFromContext db = new WinFromContext())
            {
                db.ConfigInfo.Add(new ConfigInfo()
                {
                    ConfigKey = textKey.Text,
                    ConfigValue = textValue.Text
                });
                db.SaveChanges();
            }
        }
    }
}
