using System;
using System.IO;

namespace RentalProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // input.txt 파일의 상대경로 지정
            string curDir = System.Environment.CurrentDirectory;
            string path = String.Format("{0}/input.txt",curDir);
       
            StreamReader sr = new StreamReader(path);
            StreamWriter sw = new StreamWriter("output.txt");
            int lineNum = 1;

            ComputerManager cm = new ComputerManager(); // 컴퓨터 관리자 생성
            while (sr.Peek() >= 0) // 입력 파일에 더 이상 읽을 문자가 없을 때 까지 실행 
            {
                // 총 컴퓨터 수 
                if (lineNum == 1)
                {
                    cm.initComputer(int.Parse(sr.ReadLine()));
                }
                // 노트북 수, 데스크탑 수, 넷북 수
                if (lineNum == 2)
                {
                    string[] tmp = sr.ReadLine().Split(' ');
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        cm.initComputer(i, int.Parse(tmp[i]));
                    }
                }
                // 총 사용자 수 
                else if (lineNum == 3)
                {
                    cm.initUser(int.Parse(sr.ReadLine()));
                }
                // 유저 정보 등록 
                else if (lineNum >= 4 && lineNum < 4 + cm.getUserCnt)
                {
                    string[] tmp = sr.ReadLine().Split(' ');
                    cm.initUser(tmp[0], tmp[1]);
                }
                // 명령어 실행 
                else if (lineNum >= 4 + cm.getUserCnt)
                {
                    string cmd = sr.ReadLine();
                    if (cmd.Length == 1)
                    {
                        switch (cmd)
                        {
                            case "Q": // 프로그램 종료
                                sr.Close();
                                sw.Close();
                                return;
                            case "T": // 하루의 시간 경과
                                cm.TimeCheck();
                                break;
                            case "S": // 총 지불금액, 컴퓨터, 사용자 리스트 출력
                                cm.PrintList();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        string[] info = cmd.Split(' ');
                        switch (info[0])
                        {
                            case "A": // 사용자에게 컴퓨터 할당 
                                cm.RentalComputer(int.Parse(info[1]), int.Parse(info[2])); // 사용자 아이디, 요구 대여일 수 
                                break;
                            case "R": // 사용자에게 할당된 컴퓨터 반납
                                cm.ReturnComputer(int.Parse(info[1])); // 사용자 아이디
                                break;
                            default:
                                break;
                        }
                    }
                }
                lineNum += 1;
            }
            sr.Close();
            sw.Close();
        }
    }
}
