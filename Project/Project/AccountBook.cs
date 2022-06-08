using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Project
{
    internal class AccountBook
    {
        ArrayList cards = new ArrayList();
        ArrayList bankBooks = new ArrayList();

		public AccountBook(){}

        // 카드 등록
        public void enrollCard(string name) {
			Card card = new Card(name);

            cards.Add(card);
            foreach(Card obj in cards)
            {
                Console.WriteLine("** 카드 등록 **");
                Console.WriteLine("카드 명 >> {0}",obj.getCardName);
                Console.WriteLine("------------------------------");
            }

        }

        // 통장 등록
        public void enrollBankBook(string name, string asset) {
			BankBook book = new BankBook(name,asset);
            bankBooks.Add(book);
            Console.WriteLine("** 통장 등록 **");
            Console.WriteLine("통장 명 >> {0}", book.getBankBookName);
            Console.WriteLine("------------------------------");
		}


        // 입금	-> 유형, 장소, 날짜, 금액, 메모,
		public Boolean deposit(string bankBook,string category, string place, string price, string memo) {
            foreach(BankBook bankbook in bankBooks)
            {
                if (bankbook.getBankBookName == bankBook) {

                    string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string date = datetime.Split(" ")[0];

                    // 총 자산 업데이트
                    bankbook.updateTotalAsset(Int32.Parse(price));

                    // 통장 내역 추가
                    var dict = new Dictionary<string, string>();
                    dict.Add("bankbook", bankBook);
                    dict.Add("category", category);
                    dict.Add("place", place);
                    dict.Add("price", price);
                    dict.Add("memo", memo);
                    dict.Add("datetime", datetime);


                    Dictionary<string, ArrayList> hs = bankbook.History;
                    if (!hs.ContainsKey(date))
                    {
                        hs[date] = new ArrayList();
                    }
                    hs[date].Add(dict);
                    
                    bankbook.History = hs;
                    Console.WriteLine("** <<{0}>> 통장 입금 **", bankBook);
                    Console.WriteLine("총자산: {0}",bankbook.getTotalAsset);
                    Console.WriteLine("------------------");
                    return true;
                }
            }
            Console.WriteLine("존재하지 않는 통장입니다");
            return false;       
        }

        // 출금 -> 통장 명, 유형, 장소, 날짜, 금액, 메모
        public Boolean withDraw(string name, string category, string place, string price, string memo)
        {
            foreach (BankBook obj in bankBooks)
            {
                if (obj.getBankBookName == name)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string date = datetime.Split(" ")[0];
                    int tmp = Int32.Parse(price) * (-1);

                    // 총 자산 업데이트
                    obj.updateTotalAsset(tmp);

                    // 통장 내역 추가
                    var dict = new Dictionary<string, string>();
                    dict.Add("bankbook", name);
                    dict.Add("category", category);
                    dict.Add("place", place);
                    dict.Add("price", tmp.ToString());
                    dict.Add("memo", memo);
                    dict.Add("datetime", datetime);


                    Dictionary<string, ArrayList> hs = obj.History;
                    if (!hs.ContainsKey(date))
                    {
                        hs[date] = new ArrayList();
                    }
                    hs[date].Add(dict);

                    obj.History = hs;
                    Console.WriteLine("** <<{0}>> 통장 출금 **", name);
                    Console.WriteLine("총자산: {0}", obj.getTotalAsset);
                    Console.WriteLine("------------------");
                    return true;
                }
            }
            Console.WriteLine("존재하지 않는 통장입니다.");
            return false;
        }
        
        // 카드 출금
        public Boolean cardPayment(string name, string category, string place, string price, string memo)
        {
            foreach (Card obj in cards)
            {
                if (obj.getCardName == name)
                {

                    string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string date = datetime.Split(" ")[0];

                    // 카드 내역 추가
                    var dict = new Dictionary<string, string>();
                    dict.Add("card", name);
                    dict.Add("category", category);
                    dict.Add("place", place);
                    dict.Add("price", price);
                    dict.Add("memo", memo);
                    dict.Add("datetime", datetime);

                    Dictionary<string, ArrayList> hs = obj.History;
                    if (!hs.ContainsKey(date))
                    {
                        hs[date] = new ArrayList();
                    }
                    hs[date].Add(dict);

                    obj.History = hs;
                    Console.WriteLine("** <<{0}>> 카드 결제 **", name);
                    Console.WriteLine("결제 예정 금액: {0}", price);
                    Console.WriteLine("------------------");
                    return true;
                }
            }
            Console.WriteLine("존재하지 않는 카드입니다.");
            return false;
        }

         // 총 자산 조회 -> 총 자산, 통장 별 잔고, 카드 별 결제 예정 금액 출력
        public string getBalance() {
            // 총 자산 구하기 => 전체 통장
            // 통장 별 잔고 구하기
            // 카드 별 결제 예정 금액 구하기
            int totalAsset = 0;
            Dictionary<string,int> dictBankBook = new Dictionary<string, int>();
            Dictionary<string,int> dictCard = new Dictionary<string, int>();

            foreach(BankBook bankbook in bankBooks){
                totalAsset+=Int32.Parse(bankbook.getTotalAsset); // 총 자산
                dictBankBook.Add(bankbook.getBankBookName, Int32.Parse(bankbook.getTotalAsset)); // 통장별 잔고
            }
            foreach(Card card in cards){ // 카드 별 결제 예정 금액
                string date = DateTime.Now.ToString("yyyy-MM");
                foreach(string key in card.History.Keys){
                    string target = key.Substring(0,7);
                    if(target==date){
                        foreach (Dictionary<string, string> items in card.History[key]){
                            if (!dictCard.ContainsKey(items["card"])){
                                    dictCard[items["card"]] = Int32.Parse(items["price"]);
                            }else{
                                dictCard[items["card"]] += Int32.Parse(items["price"]);
                            }
                        }
                    }
                }
            }   

            Dictionary<string,object> obj = new Dictionary<string, object>();
            obj.Add("totalAsset",totalAsset);
            obj.Add("bankbooks",dictBankBook);
            obj.Add("cards",dictCard);

            string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });

            Console.WriteLine("** 총 자산 조회 **");
            Console.WriteLine(json);
            Console.WriteLine("------------------------------");
            return json;
        }

        // 통장 별 조회 => 통장명, 잔고
        public string getBankBookInfo() {
            Dictionary<string,object> dict = new Dictionary<string, object>();

            foreach (BankBook bankbook in bankBooks)
            {
                dict[bankbook.getBankBookName] = Int32.Parse(bankbook.getTotalAsset);
            }

            string json = JsonSerializer.Serialize(dict, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });
            
            Console.WriteLine("** 통장 별 조회 **");
            Console.WriteLine(json);
            Console.WriteLine("-------------------------");
        
            return json;
        }

        // 카드 별 조회 => 카드 명, 결제 예정 금액 (당월)
        public string getCardsInfo() {
            var dict = new Dictionary<string, int>();
            foreach (Card card in cards)
            {
                string date = DateTime.Now.ToString("yyyy-MM");
                foreach(string key in card.History.Keys){
                    string target = key.Substring(0,7);
                    if(target==date){
                        foreach (Dictionary<string, string> items in card.History[key]){
                            if (!dict.ContainsKey(items["card"])){
                                    dict[items["card"]] = Int32.Parse(items["price"]);
                            }else{
                                dict[items["card"]] += Int32.Parse(items["price"]);
                            }
                        }
                    }
                }
            }

            string json = JsonSerializer.Serialize(dict, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });

            Console.WriteLine("** 카드 별 조회 **");
            Console.WriteLine(json);
            Console.WriteLine("-------------------------");
        
            return json;
        }

        // 일별 가계부 조회 -> 유형, 장소, 금액, 일 총 입출금 금액
        public string getDailyHistory(string date) 
        {
            ArrayList al = new ArrayList();
            foreach(BankBook bankbook in bankBooks)
            {
                if (bankbook.History.ContainsKey(date))
                {
                    foreach(Dictionary<string,string> item in bankbook.History[date])
                    {
                        al.Add(item);
                    }
                }
            }
            string json = JsonSerializer.Serialize(al, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });
            Console.WriteLine("** 일별 가계부 조회 **");
            Console.WriteLine(json);
            Console.WriteLine("----------------------------------");
            return json;
        }

        // 월별 가계부 조회 -> 월 총 입출금 금액, 월 카드 결제 금액, 월 유형 별 총 금액 및 비율
        public string getMonthlyHistory(string date)
        {
            ArrayList al = new ArrayList();
            int mTotaldeposit = 0;
            int mTotalwithdraw = 0;
            int mCardPayAmt = 0;
            Dictionary<string,int> mTypeAmt = new Dictionary<string, int>();

            foreach (BankBook bankbook in bankBooks) // 통장 정산
            {   
                foreach(string key in bankbook.History.Keys){
                    string target = key.Substring(0,7);
                    if(target == date){
                        foreach(Dictionary<string,string> item in bankbook.History[key]){                            
                            if(Int32.Parse(item["price"])>=0){
                                mTotaldeposit+=Int32.Parse(item["price"]); // 월 총 입금 업데이트
                            }
                            else{
                                mTotalwithdraw+=Int32.Parse(item["price"]); // 월 총 출금 업데이트
                            }
                            
                            if(!mTypeAmt.ContainsKey(item["category"])){ // 월 유형 별 총 금액 업데이트
                                mTypeAmt[item["category"]] = Int32.Parse(item["price"]);
                            }else{
                                mTypeAmt[item["category"]] += Int32.Parse(item["price"]);
                            }
                        }
                    }
                }
            }

            foreach(Card card in cards) // 카드 정산
            {
                foreach(string key in card.History.Keys){
                    string target = key.Substring(0,7);
                    if(target==date){
                        foreach(Dictionary<string,string> item in card.History[key]){                            
                            mCardPayAmt += Int32.Parse(item["price"]);
                            if(!mTypeAmt.ContainsKey(item["category"])){ // 월 유형 별 총 금액 업데이트
                                mTypeAmt[item["category"]] = -Int32.Parse(item["price"]);
                            }else{
                                mTypeAmt[item["category"]] -= Int32.Parse(item["price"]);
                            }
                        }
                    }
                }
            }

            Dictionary<string,object> dict = new Dictionary<string, object>();
            dict.Add("mTotalDeposit", mTotaldeposit);
            dict.Add("mTotalwithdraw", mTotalwithdraw);
            dict.Add("mCardPayAmt", mCardPayAmt);
            dict.Add("mTypeAmt",mTypeAmt);

            string json = JsonSerializer.Serialize(dict, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });
            
            Console.WriteLine("** 월 별 가계부 조회 **");
            Console.WriteLine(json);
            Console.WriteLine("------------------------------");
            return json;
        }
    }
}
