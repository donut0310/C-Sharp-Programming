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

    // Property
    public DeliveryVehicle[][] test{
        get {return this.waitPlaces;}
    }
    
    public int test2{
        get{return this.waitPlaces.Length;}
    }
    public int getPlaces{
        get {return this.numWaitingPlaces;}
    }

    // Method
    public void ReadyIn(string _wp, string _vid, string _dest, string _p){
        int wp = int.Parse(_wp.Split('W')[1]); // 대기장소 번호
        int vid = int.Parse(_vid); // 배달 자동차 번호
        int p = int.Parse(_p.Split("P")[1]); // 배달 자동차 우선순위
        
        DeliveryVehicle d = new DeliveryVehicle(vid, _dest, p);

        // 배열크기 재조정
        int arrSize = this.waitPlaces[wp-1].Length;
        Array.Resize<DeliveryVehicle>(ref waitPlaces[wp-1], arrSize+1); 
        // 배달 자동차 배정
        waitPlaces[wp-1][arrSize] = d;
        // 배달 자동차의 우선순위에 따른 정렬
        Array.Sort(waitPlaces[wp-1], new PriorityComparer());
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Vehicle {0} assigned to waitPlace #{1}", vid, wp);
        sw.Close();
    }

    public void Ready(string _vid, string _dest, string _p){
        int vid = int.Parse(_vid); // 배달 자동차 번호
        int p = int.Parse(_p.Split("P")[1]); // 배달 자동차 

        DeliveryVehicle d = new DeliveryVehicle(vid, _dest, p);
        
        // 가장 적은 배달자동차가 대기 중인 대기장소 선별
        var max = (0,0);
        for(int i=0;i<numWaitingPlaces;i++){
            int cnt = waitPlaces[i].Length;
            if(cnt >= max.Item2){
                var tmp = (i, cnt);
                max = tmp;
            }
        }

        // 배열크기 재조정
        int arrSize = this.waitPlaces[max.Item1].Length;
        Array.Resize<DeliveryVehicle>(ref waitPlaces[max.Item1], arrSize+1); 

        // 배달 자동차 배정
        waitPlaces[max.Item1][arrSize] = d;
        
        StreamWriter sw = File.AppendText("OUTPUT.txt");
        sw.WriteLine("Vehicle {0} assigned to waitPlace #{1}", vid, max.Item1+1);
        sw.Close();       
    }

    public void Status(){

    }

    public void Cancel(){

    }

    public void Deliver(){

    }

    public void Clear(){

    }
}

class PriorityComparer : IComparer
{
    public int Compare(object x, object y)
    {
        return(new CaseInsensitiveComparer()).Compare(((DeliveryVehicle)x).Priority,
               ((DeliveryVehicle)y).Priority);
    }
}