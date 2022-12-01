using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WindowsFormsApp2
{
    internal class HeartProgram
    {
        public ObjectId _id { get; set; } // "MyLists" Collection의 각 데이터(documnet) 고유 ID
        public string department { get; set; } // 학과
        public string title { get; set; } // 프로그램명
        public string startRegister { get; set; } // 신청 시작일 
        public string endRegister { get; set; } // 신청 마감일
        public string startOperate { get; set; } // 운영 시작일 
        public string endOperate { get; set; } // 운영 종료일
    }
}
