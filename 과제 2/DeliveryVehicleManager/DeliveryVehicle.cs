class DeliveryVehicle {
    int vehicleId; // 자동차 아이디
    string destination; // 배달 목적지 문자열
    int priority; // 우선순위
    public DeliveryVehicle(int vid, string dest, int p){
        vehicleId = vid;
        destination = dest;
        priority = p;
    }

    // Property
    public int Vid{
        get {return this.vehicleId;}
    }
    public string Destination{
        get {return this.destination;}
    }
    public int Priority{
        get {return this.priority;}
    }
// …
}