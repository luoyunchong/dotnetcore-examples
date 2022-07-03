
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ConfigInfoDto
    {
        public string ConfigValue { get; set; }
        public int Count { get; set; }
    }
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
                if (textId.Text == "")
                {
                    db.ConfigInfo.Add(new ConfigInfo()
                    {
                        ConfigKey = textKey.Text,
                        ConfigValue = textValue.Text,
                        CreateTime = DateTime.Parse("2020/05/15 00:00:00")
                    });
                }
                else
                {
                    //DB.Sqlite.Update<ConfigInfo>(int.Parse(textId.Text))
                    //    .SetDto(new ConfigInfoDto
                    //    {
                    //        ConfigValue = textValue.Text,
                    //        Count = int.Parse(textCount.Text)
                    //    })
                    //    .ExecuteAffrows();

                    object dto = new Dictionary<string, object>();

                    var dtoProps = dto.GetType().GetProperties();
                    int r = DB.Sqlite.Update<ConfigInfo>(int.Parse(textId.Text))
                       .SetDto(new Dictionary<string, object>
                       {
                         {  "ConfigValue" , textValue.Text},
                         {  "Count" ,  int.Parse(textCount.Text)},
                       })
                       .ExecuteAffrows();
                    //db.ConfigInfo.Update(new ConfigInfo()
                    //{
                    //    Id = int.Parse(textId.Text),
                    //    ConfigKey = textKey.Text,
                    //    ConfigValue = textValue.Text,
                    //    Count = int.Parse(textCount.Text)
                    //});
                }

                db.SaveChanges();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using (WinFromContext db = new WinFromContext())
            {
                var dbs = await db.ConfigInfo.Select.ToListAsync();
            }
        }
    }
}
