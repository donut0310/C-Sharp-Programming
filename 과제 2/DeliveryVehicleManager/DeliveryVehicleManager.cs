class DeliveryVehicleManager {
    int numWaitingPlaces; // 대기장소 총 개수
    DeliveryVehicle [][] waitPlaces; // 대기장소 배열

    public DeliveryVehicleManager(int num){
        numWaitingPlaces = num;
        waitPlaces = new DeliveryVehicle[num][0];
        for(int i=0;i<waitPlaces.Length;i++){
            if(waitPlaces[i].Length<1){
                Console.WriteLine("none");
            }
            else Console.WriteLine(waitPlaces[i].Length);
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
    public void ReadyIn(string _wp, string _vid, string dest, string _p){
        int wp = int.Parse(_wp.Split('W')[1]);
        int vid = int.Parse(_vid);
        int p = int.Parse(_p.Split("P")[1]);
        
        DeliveryVehicle d = new DeliveryVehicle(vid, dest, p);
        int arrSize = this.waitPlaces[wp-1].Length;
        // 배열크기 재조정
        Array.Resize<int>(this.waitPlaces[wp-1],arrSize+1); 
        // 배달 자동차 배정
        waitPlaces[wp-1][arrSize] = d;
        // 배달 자동차의 우선순위에 따른 정렬
    }

    public void Ready(){

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