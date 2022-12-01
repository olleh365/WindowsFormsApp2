using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {

        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<HeartProgram> collection;
        
        
        public Form2()
        {
            InitializeComponent();
            client = new MongoClient();
            db = client.GetDatabase("21511959");
            //collection = db.GetCollection<HeartProgram>("MyLists");



        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            findAll();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int rowInex=dataGridView1.CurrentCell.RowIndex;
            ObjectId id= (ObjectId)dataGridView1.Rows[rowInex].Cells[0].Value;
            var filter = Builders<HeartProgram>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
            findAll();
        }

        private void findAll()
        {
            collection = db.GetCollection<HeartProgram>("MyLists");
            List<HeartProgram> programList = collection.AsQueryable().ToList<HeartProgram>();
            dataGridView1.DataSource = programList;
        }
    }
}
