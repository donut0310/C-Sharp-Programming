using System;
namespace RentalProgram
{
    // User 클래스
    public class User
    {
        protected int uId; // 유저 번호
        protected string uType; // 유저 타입
        protected int typeId; // 유저 타입 번호 
        protected string name; // 유저 이름 
        protected int cId; // 컴퓨터 대여 번호(대여 시) 
        protected bool hasRent; // 대여 여부 
        protected string service; // 이용 가능 서비스
        public static int studentCnt = 0;
        public static int workerCnt = 0;
        public static int gamerCnt = 0;

        public User(int uId, int typeId, string name, int cId)
        {
            this.uId = uId;
            this.typeId = typeId;
            this.name = name;
            this.cId = 0;

        }

        // 유저 번호 반환
        public int getUserId
        {
            get { return uId; }
        }
        // 유저 타입 번호 반환
        public int getTypeId
        {
            get { return typeId; }
        }
        // 유저 이름 반환 
        public string getName
        {
            get { return name; }
        }
        // 유저 타입 반환 
        public string getUserType
        {
            get { return uType; }
        }
        // 컴퓨터 대여 여부 반환
        public bool rent
        {
            get { return hasRent; }
            set { hasRent = value; }
        }
        // 이용 가능 서비스 반환
        public string getService
        {
            get { return service; }
        }
        // 대여한 컴퓨터 번호 반환
        public int CID
        {
            get { return cId; }
            set { cId = value; }
        }
    }

    // 자식 클래스들
    public class Students: User
    {
        public Students(int uId, int typeId, string name, int cId) : base(uId,typeId,name,cId)
        {
            service = "internet, scientific";
            uType = "Students";
        }
        
    }

    public class Workers : User
    {
        public Workers(int uId, int typeId, string name, int cId) : base(uId, typeId, name, cId)
        {
            service = "internet";
            uType = "OfficeWorkers";
        }
    }
    public class Gamers: User
    {
        public Gamers(int uId, int typeId, string name, int cId) : base(uId, typeId, name, cId)
        {
            service = "internet, game";
            uType = "Gamers";
        }
    }
}
