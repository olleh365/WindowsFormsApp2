using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;
using System.Data.Common;
using System.Reflection;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();

        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<HeartProgram> collection;
        public Form1()
        {
            InitializeComponent();
            
            dt.Columns.Add("학과");
            dt.Columns.Add("프로그램명");
            dt.Columns.Add("신청 시작일");
            dt.Columns.Add("신청 마감일");
            dt.Columns.Add("운영 시작일");
            dt.Columns.Add("운영 종료일");

            client = new MongoClient();
            db = client.GetDatabase("21511959");
            collection = db.GetCollection<HeartProgram>("MyLists");
        }

        private void data_btn_Click(object sender, EventArgs e)
        {
            int stNumber = Int32.Parse(tb_start.Text);
            int endNumber = Int32.Parse(tb_end.Text);
            for(int i=stNumber; i<=endNumber;i++)
            {
                string url = "https://heart.daegu.ac.kr/ko/program/all/list/all/"+i;

                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                HtmlNodeCollection departNodes = doc.DocumentNode.SelectNodes("//div[@class='content']/label");
                HtmlNodeCollection NameNodes = doc.DocumentNode.SelectNodes("//div[@class='detail']/div/div/b");
                HtmlNodeCollection operStNodes = doc.DocumentNode.SelectNodes("//div[@class='content']/small[1]/time[1]");
                HtmlNodeCollection operEndNodes = doc.DocumentNode.SelectNodes("//div[@class='content']/small[1]/time[2]");
                HtmlNodeCollection subStNodes = doc.DocumentNode.SelectNodes("//div[@class='content']/small[2]/time[1]");
                HtmlNodeCollection subEndNodes = doc.DocumentNode.SelectNodes("//div[@class='content']/small[2]/time[2]");

                for (int j = 0; j < departNodes.Count; j++)
                {
                    dt.Rows.Add(departNodes[j].InnerText, NameNodes[j].InnerText, subStNodes[j].InnerText, subEndNodes[j].InnerText,operStNodes[j].InnerText,operEndNodes[j].InnerText);
                }
            }

            dataGridView1.DataSource = dt;



        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int rowlnex = dataGridView1.CurrentCell.RowIndex;
            //ObjectId id = (ObjectId)dataGridView1.Rows[rowlnex].Cells[0].Value;
            //var filter = Builders<HeartProgram>.Filter.Eq("_id", id);

            
            HeartProgram std = new HeartProgram
            {
                department = dataGridView1.Rows[rowlnex].Cells[0].Value.ToString(),
                title = dataGridView1.Rows[rowlnex].Cells[1].Value.ToString(),
                startRegister = dataGridView1.Rows[rowlnex].Cells[2].Value.ToString(),
                endRegister = dataGridView1.Rows[rowlnex].Cells[3].Value.ToString(),
                startOperate = dataGridView1.Rows[rowlnex].Cells[4].Value.ToString(),
                endOperate = dataGridView1.Rows[rowlnex].Cells[5].Value.ToString()
            };
            collection.InsertOne(std);
            string title = dataGridView1.Rows[rowlnex].Cells[1].Value.ToString();
            MessageBox.Show(title+"  추가완료!");
        }
    }
}
