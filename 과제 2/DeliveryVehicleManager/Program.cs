using System;
using System.IO;

class Program {
    static void Main(string[] args) {
        string path = String.Format("{0}/INPUT.txt", System.Environment.CurrentDirectory);
        StreamReader sr = new StreamReader(path);
        DeliveryVehicleManager dvm = new DeliveryVehicleManager(int.Parse(sr.ReadLine()));
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
                    break;
                case "Cancel":
                    break;
                case "Deliver":
                    break;
                case "Clear":
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