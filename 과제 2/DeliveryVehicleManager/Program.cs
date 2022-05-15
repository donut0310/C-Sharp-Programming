using System;
using System.IO;

class Program {
    static void Main(string[] args) {
        // 초기화 => INPUT.txt 파일 위치 지정
        string path = String.Format("{0}/INPUT.txt", System.Environment.CurrentDirectory);
        StreamReader sr = new StreamReader(path);
        DeliveryVehicleManager dvm = new DeliveryVehicleManager(int.Parse(sr.ReadLine()));

        //  초기화 => OUTPUT.txt 파일 존재시 삭제
        string outputFile = String.Format("{0}/OUTPUT.txt", System.Environment.CurrentDirectory);
        if(File.Exists(outputFile)){
            File.Delete(outputFile);
        }

        int lineNum = 1;
        while(sr.Peek()>=0){
            if(lineNum==1){
                lineNum+=1;
                continue;
            }

            string [] tmp = sr.ReadLine().Split(' ');
            switch(tmp[0]){
                case "ReadyIn":
                    dvm.ReadyIn(tmp[1],tmp[2],tmp[3],tmp[4]);
                    break;
                case "Ready":
                    dvm.Ready(tmp[1], tmp[2], tmp[3]);
                    break;
                case "Status":
                    dvm.Status();
                    break;
                case "Cancel":
                    dvm.Cancel(tmp[1]);
                    break;
                case "Deliver":
                    dvm.Deliver(tmp[1]);
                    break;
                case "Clear":
                    dvm.Clear(tmp[1]);
                    break;
                case "Quit":
                    return;
                default:
                    break;
            }
        }
        sr.Close();
    }
}