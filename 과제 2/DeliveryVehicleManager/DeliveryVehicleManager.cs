using System;
using System.Collections;
using System.IO;
class DeliveryVehicleManager {
    int numWaitingPlaces; // 대기장소 총 개수
    DeliveryVehicle [][] waitPlaces; // 대기장소 배열

    public DeliveryVehicleManager(int num){
        numWaitingPlaces = num;
        waitPlaces = new DeliveryVehicle[num][];
        for(int i=0;i<num;i++){
            waitPlaces[i] =new DeliveryVehicle[0];
        }
    }
    // --------------- Method --------------- //

    // 배달자동차를 특정 대기장소에 배정하는 메소드
    public void ReadyIn(string _wp, string _vid, string _dest, string _p){
        int wp = int.Parse(_wp.Split('W')[1]); // 대기장소 번호
        int vid = int.Parse(_vid); // 배달 자동차 번호
        int p = int.Parse(_p.Split("P")[1]); // 배달 자동차 우선순위
        
        // 배달 자동차 객체 생성
        DeliveryVehicle d = new DeliveryVehicle(vid, _dest, p);

        // 배열 크기 재조정
        int arrSize = this.waitPlaces[wp-1].Length;
        Array.Resize<DeliveryVehicle>(ref waitPlaces[wp-1], arrSize+1); 

        // 배달 자동차 배정
        waitPlaces[wp-1][arrSize] = d;

        // 배달 자동차의 우선순위에 따른 정렬
        Array.Sort(waitPlaces[wp-1], new PriorityComparer());

        // 파일 출력
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Vehicle {0} assigned to waitPlace #{1}", vid, wp);
        sw.Close();
    }

    // 가장 적은 배달자동차가 대기하고 있는 대기장소에 배달 자동차 배정 메소드
    public void Ready(string _vid, string _dest, string _p){
        int vid = int.Parse(_vid); // 배달 자동차 번호
        int p = int.Parse(_p.Split("P")[1]); // 배달 자동차 

        // 배달 자동차 객체 생성
        DeliveryVehicle d = new DeliveryVehicle(vid, _dest, p);
        
        // 가장 적은 배달자동차가 대기 중인 대기장소 선별
        var min = (0,float.PositiveInfinity);
        for(int i = 0; i < numWaitingPlaces; i++){
            int cnt = waitPlaces[i].Length;
            if(cnt < min.Item2){
                var tmp = (i, cnt);
                min = tmp;
            }
        }

        // 배열크기 재조정
        int arrSize = this.waitPlaces[min.Item1].Length;
        Array.Resize<DeliveryVehicle>(ref waitPlaces[min.Item1], arrSize+1); 

        // 배달 자동차 배정
        waitPlaces[min.Item1][arrSize] = d;
        
        // 배달 자동차의 우선순위에 따른 정렬
        Array.Sort(waitPlaces[min.Item1], new PriorityComparer());

        // 파일 출력
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Vehicle {0} assigned to waitPlace #{1}", vid, min.Item1+1);
        sw.Close();       
    }

    // 각 대기장소에 대기하고 있는 배달 자동차의 정보 출력 메소드
    public void Status(){
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("************************ Delivery Vehicle Info ************************");
        sw.WriteLine("Number of WaitPlaces: {0}", numWaitingPlaces);
        for(int i = 0; i < numWaitingPlaces; i++){
            sw.WriteLine("WaitPlace #{0} Number Vehicles: {1}", i+1, waitPlaces[i].Length);
            if(waitPlaces[i].Length > 0){
                foreach (DeliveryVehicle wp in waitPlaces[i])
                    sw.WriteLine("FNUM: {0} DEST: {1} PRIO: {2}", wp.Vid, wp.Destination, wp.Priority);
            }
            sw.WriteLine("---------------------------------------------------");
        }
        sw.WriteLine("************************ End Delivery Vehicle Info ************************");
        sw.Close();  
    }

    // 해당 배달자동차를 대기장소에서 삭제하는 메소드
    public void Cancel(string _vid){
        int vid = int.Parse(_vid); // 배달 자동차 번호
        for(int i = 0; i < numWaitingPlaces; i++){
            int indexToRemove = Array.FindIndex(waitPlaces[i], row => row.Vid == vid);
            if(indexToRemove >= 0){
                // 해당 배달 자동차 삭제 및 배열 사이즈 -1
                waitPlaces[i] = waitPlaces[i].Where((source, index) => index != indexToRemove).ToArray();
            }
        }
        // 파일 출력
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Cancelation of Vehicle {0} completed.", vid);
        sw.Close();
    }

    /* 해당 대기장소에서 대기하고 있는 배달자동차 중에서 우선순위가
       가장 높은 배달자동차를 배달 보내는 메소드 */
    public void Deliver(string _wp){
        int wp = int.Parse(_wp.Split('W')[1]); // 대기장소 번호
        int vid = waitPlaces[wp-1][0].Vid; // 배달 자동차 번호

        /* 우선순위가 가장 높은 배달 자동차 => 배열 인덱스 0
            대기장소에서 배달 자동차 객체 삭제 및 사이즈 재조정*/
        waitPlaces[wp-1] = waitPlaces[wp-1].Where((source, index) => index != 0).ToArray();

        // 파일 출력
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Vehicle {0} used to deliver.", vid);
        sw.Close();      

    }

    // 해당 대기장소에서 대기하고 있는 배달자동차의 대기를 취소하는 메소드
    public void Clear(string _wp){
        int wp = int.Parse(_wp.Split('W')[1]); // 대기장소 번호
        // 배달자동차 객체 삭제 및 배열 사이즈 재조정
        Array.Clear(waitPlaces[wp-1], 0, waitPlaces[wp-1].Length);
        Array.Resize<DeliveryVehicle>(ref waitPlaces[wp-1], 0); 

        // 파일 출력
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("WaitPlace #{0} cleared.", wp);
        sw.Close();
    }
}

// 배달 자동차의 우선순위에 따른 정렬
class PriorityComparer : IComparer
{
    public int Compare(object x, object y)
    {
        return(new CaseInsensitiveComparer()).Compare(((DeliveryVehicle)x).Priority,
               ((DeliveryVehicle)y).Priority);
    }
}