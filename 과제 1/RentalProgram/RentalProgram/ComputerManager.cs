using System;
using System.IO;

// ComputerManager 클래스
namespace RentalProgram
{
    public class ComputerManager
    {

        private Computer[] arrComp; // 모든 컴퓨터의 정보
        private User[] arrUser; // 모든 유저의 정보
        private int totalUsers; // 총 사용자 수
        public static int totalCost; // 총 지불 금액 
        private int computerCnt;
        private int userCnt;

        public ComputerManager()
        {

        }

        // 총 사용자 수 반환
        public int getUserCnt
        {
            get { return totalUsers; }
        }

        // 컴퓨터 개수 초기화 
        public void initComputer(int num)
        {
            arrComp = new Computer[num];
        }

        // 컴퓨터 정보 초기화
        public void initComputer(int type, int num)
        {
            for (int i = 0; i < num; i++)
            {
                if (type == 0) // 노트북 
                {
                    Notebook device = new Notebook(++computerCnt, i + 1);
                    arrComp[device.getComputerId-1] = device;
                }
                else if (type == 1) // 데스크탑 
                {
                    Desktop device = new Desktop(++computerCnt, i + 1);
                    arrComp[device.getComputerId-1] = device;
                }
                else if (type == 2) // 넷북 
                {
                    Netbook device = new Netbook(++computerCnt, i + 1);
                    arrComp[device.getComputerId-1] = device;
                }
            }
        }

        // 사용자 정보 초기화
        public void initUser(int num)
        {
            Console.WriteLine(num);
            totalUsers = num;
            arrUser = new User[num];
        }

        // 사용자 타입, 이름 초기화 
        public void initUser(string type,string name)
        {
            switch (type)
            {
                case "Student":
                    Students s = new Students(++userCnt, ++User.studentCnt, name, 0);
                    arrUser[s.getUserId - 1] = s;
                    break;
                case "Worker":
                    Workers w = new Workers(++userCnt, ++User.workerCnt, name, 0);
                    arrUser[w.getUserId - 1] = w;
                    break;
                case "Gamer":
                    Gamers g = new Gamers(++userCnt, ++User.gamerCnt, name, 0);
                    arrUser[g.getUserId - 1] = g;
                    break;
                default:
                    break;
            }
        }

        // 사용자에게 컴퓨터 할당: 사용자 아이디, 요구 대여일 수
        public void RentalComputer(int uId, int ur)
        {
            StreamWriter sw = File.AppendText("output.txt");
            string uType = arrUser[uId - 1].getUserType;

            for(int i = 0; i < arrComp.Length; i++)
            {
                string dType = arrComp[i].getDeviceType; // 장치 타입 
                if (arrComp[i].rent == false) // 대여 가능한 경우 
                {
                    // 직원인 경우 노트, 데스크탑, 넷북 모두 대여 가능 
                    if(uType== "OfficeWorkers")
                    {
                        sw.WriteLine("Computer #{0} has been assigned to User #{1}", arrComp[i].getComputerId, uId);
                        sw.WriteLine("===========================================================");
                        sw.Close();
                        // 컴퓨터 할당
                        arrComp[i].dr = ur;
                        arrComp[i].dl = ur;
                        arrComp[i].UID = uId;
                        arrComp[i].rent = true;
                        // 유저 할당
                        arrUser[uId - 1].CID = arrComp[i].getComputerId;
                        arrUser[uId - 1].rent = true;
                        return;
                    }
                    // 학생인 경우 노트북과 데스크탑만 대여 가능 
                    else if(uType=="Students" && dType != "Netbook")
                    {
                        sw.WriteLine("Computer #{0} has been assigned to User #{1}", arrComp[i].getComputerId, uId);
                        sw.WriteLine("===========================================================");
                        sw.Close();
                        // 컴퓨터 할당
                        arrComp[i].dr = ur;
                        arrComp[i].dl = ur;
                        arrComp[i].UID = uId;
                        arrComp[i].rent = true;
                        // 유저 할당
                        arrUser[uId - 1].CID = arrComp[i].getComputerId;
                        arrUser[uId - 1].rent = true;
                        return;
                    }
                    // 게이머인 경우 데스크탑만 대여 가능
                    else if(uType=="Gamers" && dType == "Desktop")
                    {
                        sw.WriteLine("Computer #{0} has been assigned to User #{1}", arrComp[i].getComputerId, uId);
                        sw.WriteLine("===========================================================");
                        sw.Close();
                        // 컴퓨터 할당
                        arrComp[i].dr = ur;
                        arrComp[i].dl = ur;
                        arrComp[i].UID = uId;
                        arrComp[i].rent = true;
                        // 유저 할당
                        arrUser[uId - 1].CID = arrComp[i].getComputerId;
                        arrUser[uId - 1].rent = true;
                        return;
                    }
                }
            }
            sw.WriteLine("No computers available for rent");
            sw.WriteLine("===========================================================");
            sw.Close();
        }

        // 사용자 컴퓨터 반납: 사용자 아이디 
        public void ReturnComputer(int uId)
        {
            StreamWriter sw = File.AppendText("output.txt");
            // 사용자가 대여한 이력이 있는 경우
            for (int i = 0; i < arrComp.Length; i++)
            {
                if (arrComp[i].UID == uId)
                {
                    // 비용 지불 
                    int payment = Payment(arrComp[i]);
                    // 컴퓨터 반납 
                    arrComp[i].dr = 0;
                    arrComp[i].dl = 0;
                    arrComp[i].du = 0;
                    arrComp[i].UID = 0;
                    arrComp[i].rent = false;
                    // 사용자 대여 기록 초기화
                    arrUser[uId-1].CID = 0;
                    arrUser[uId-1].rent = false;
                    sw.WriteLine("User #{0} has returned Computer #{1} and paid {2} won.", uId, arrComp[i].getComputerId, payment);
                    sw.WriteLine("===========================================================");
                    sw.Close();
                    return;
                }
            }
            sw.WriteLine("No history of renting a computer.");
            sw.WriteLine("===========================================================");
            sw.Close();
        }

        // 사용 비용 지불
        public int Payment(Computer pc)
        {
            int payment = pc.PAYMENT * pc.du;
            totalCost += payment;
            return payment;
        }

        // 사용 시간 경과
        public void TimeCheck()
        {
            // 대여된 전체 컴퓨터의 대여 기간 -1, 사용일 +1
            // 이때 대여 가능 기간이 0 이 된 컴퓨터에 대해서 반납처리 하고 요금지불
            StreamWriter sw = File.AppendText("output.txt");
            sw.WriteLine("It has passed one day...");
            for(int i = 0; i < arrComp.Length; i++)
            {
                if (arrComp[i].rent == true)
                {
                    arrComp[i].dl -= 1;
                    arrComp[i].du += 1;
                    if (arrComp[i].dl == 0) //대여기간이 만료된 경우 
                    {
                        // 지불금액 계산
                        int payment = Payment(arrComp[i]);

                        // 파일 출력
                        int cid = arrComp[i].getComputerId;
                        int uid = arrComp[i].UID;
                        sw.WriteLine("Time for Computer #{0} has expired. User #{1} has returned Computer #{2} and paid {3} won.",
                            cid, uid, cid, payment);

                        // 유저 대여 기록 초기화
                        arrUser[uid - 1].rent = false;
                        arrUser[uid - 1].CID = 0;

                        // 컴퓨터 대여 기록 초기화
                        arrComp[i].dr = 0;
                        arrComp[i].du = 0;
                        arrComp[i].UID = 0;
                        arrComp[i].rent = false;
                    }
                }
            }
            sw.WriteLine("===========================================================");
            sw.Close();
        }

        // 총 지불금액 계산, 현재 컴퓨터와 사용자 리스트 출력
        public void PrintList()
        {
            //StreamWriter sw = new StreamWriter("output.txt");
            StreamWriter sw = File.AppendText("output.txt");
            // 총 지불금액 출력
            Console.WriteLine("Total Cost: {0}", totalCost); //debug
            sw.WriteLine("Total Cost: {0}",totalCost);

            // 컴퓨터 리스트 출력 
            sw.WriteLine("Computer List:");
            for (int i = 0; i < arrComp.Length; i++)
            {
                string dType = arrComp[i].getDeviceType;
                string pFormat = "";
                if (dType == "Notebook")
                {
                    pFormat = "({0}) type: {1}, ComId: {2}, NoteId: {3}, Used for: {4}, Avail: {5} (UserId: {6}, DR: {7}, DL: {8}, DU: {9})";
                }
                else if (dType == "Desktop")
                {
                    pFormat = "({0}) type: {1}, ComId: {2}, DeskId: {3}, Used for: {4}, Avail: {5} (UserId: {6}, DR: {7}, DL: {8}, DU: {9})";
                }
                else if (dType == "Netbook")
                {
                    pFormat = "({0}) type: {1}, ComId: {2}, NetId: {3}, Used for: {4}, Avail: {5} (UserId: {6}, DR: {7}, DL: {8}, DU: {9})";
                }
                // 대여된 컴퓨터인 경우 
                if (arrComp[i].rent)
                {
                    sw.WriteLine(pFormat, i+1, dType, arrComp[i].getComputerId, arrComp[i].getDeviceId, arrComp[i].getService, "N", arrComp[i].UID, arrComp[i].dr, arrComp[i].dl, arrComp[i].du);
                }
                // 대여되지 않은 컴퓨터인 경우
                else
                {
                    sw.WriteLine(pFormat.Split("(U")[0],i+1,dType, arrComp[i].getComputerId, arrComp[i].getDeviceId, arrComp[i].getService, "Y");
                }
            }
            // 사용자 리스트 출력
            sw.WriteLine("User List:");
            for(int i = 0; i < arrUser.Length; i++)
            {
                string uType = arrUser[i].getUserType;
                string uFormat = "";
                if(uType == "Students")
                {
                    uFormat = "({0}) type: {1}, Name: {2}, UserId: {3}, StudId: {4}, Used for: {5}, Rent: {6} (RentCompId: {7})";
                }
                else if (uType == "OfficeWorkers")
                {
                    uFormat = "({0}) type: {1}, Name: {2}, UserId: {3}, WorkerId: {4}, Used for: {5}, Rent: {6} (RentCompId: {7})";
                }
                else if (uType == "Gamers")
                {
                    uFormat = "({0}) type: {1}, Name: {2}, UserId: {3}, GamerId: {4}, Used for: {5}, Rent: {6} (RentCompId: {7})";
                }
                // 컴퓨터를 대여한 경우
                if (arrUser[i].rent)
                {
                    sw.WriteLine(uFormat, i+1, uType, arrUser[i].getName, arrUser[i].getUserId, arrUser[i].getTypeId, arrUser[i].getService, "Y", arrUser[i].CID);
                }
                // 컴퓨터를 대여하지 않은 경우
                else
                { 
                    sw.WriteLine(uFormat.Split("(R")[0], i+1, uType, arrUser[i].getName, arrUser[i].getUserId, arrUser[i].getTypeId, arrUser[i].getService, "N");
                }
            }
            sw.WriteLine("===========================================================");
            sw.Close();
        }

    }
}
