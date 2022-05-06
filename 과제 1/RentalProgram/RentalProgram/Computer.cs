using System;
namespace RentalProgram
{
    // Computer 클래스
    public class Computer
    {
        protected int cId; // 컴퓨터 번호 
        protected int dId; // 장치 번호 
        protected int uId; // 유저 번호 (대여 시) 
        protected bool isRent; // 대여 여부 
        protected int DR; // 대여 요구일 수 
        protected int DL; // 남은 대여일 수 
        protected int DU; // 사용일 수 
        protected string dType; // 장치 타입 
        protected string dService; // 장치별 제공 서비스 
        protected int dPayment; // 장치별 사용 요금

        public Computer(int cId, int dId)
        {
            this.cId = cId;
            this.dId = dId;
            this.isRent = false;
            this.DR = 0;
            this.DL = 0;
            this.DU = 0;
            this.uId = 0;
        }

        // 디바이스 번호 반환
        public int getDeviceId
        {
            get { return dId; }
        }
        // 컴퓨터 번호 반환
        public int getComputerId
        {
            get { return cId; }
        }
        // 유저 번호 반환
        public int UID
        {
            get { return uId; }
            set { uId = value; }
        }
        // 대여 여부 반환
        public bool rent
        {
            get { return isRent; }
            set { isRent = value; }
        }
        // 대여 요구일 수 반환 및 설정
        public int dr
        {
            get { return DR; }
            set { DR = value; }
        }
        // 남은 대여일 수 반환  
        public int dl
        {
            get { return DL; }
            set { DL = value; }
        }
        // 사용일 수 반환 
        public int du
        {
            get { return DU; }
            set { DU = value; }
        }
        // 장치 타입 반환
        public string getDeviceType
        {
            get { return dType; }
        }
        // 제공 서비스 반환
        public string getService
        {
            get { return dService; }
        }
        // 장치별 요금 반환
        public int PAYMENT
        {
            get { return dPayment; }
        }
    }

    // 자식 클래스들 
    public class Notebook : Computer
    {
        public Notebook(int cId, int dId) : base(cId,dId)
        {
            dService = "internet, scientific";
            dType = "Notebook";
            dPayment = 10000;
        }
    }

    public class Desktop: Computer
    {
        public Desktop(int cId, int dId): base(cId, dId)
        {
            dService = "internet, scientific, game";
            dType = "Desktop";
            dPayment = 13000;
        }
    }

    public class Netbook : Computer
    {
        public Netbook(int cId, int dId) : base(cId, dId)
        {
            dService = "internet";
            dType = "Netbook";
            dPayment = 7000;
        }
    }
}
