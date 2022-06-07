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
                Console.WriteLine("카드 등록 완료");
                Console.WriteLine("카드 명 >> {0}",obj.getCardName);
                Console.WriteLine("------------------------------");
            }

        }

        // 통장 등록
        public void enrollBankBook(string name, string asset) {
			BankBook book = new BankBook(name,asset);
            bankBooks.Add(book);
            Console.WriteLine("통장 등록 완료");
            Console.WriteLine("통장 명 >> {0}", book.getBankBookName);
            Console.WriteLine("------------------------------");
		}


        // 입금	-> 유형, 장소, 날짜, 금액, 메모,
		public Boolean deposit(string bankBook,string category, string place, string price, string memo) {
            foreach(BankBook obj in bankBooks)
            {
                if (obj.getBankBookName == bankBook) {

                    string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string date = datetime.Split(" ")[0];

                    // 총 자산 업데이트
                    obj.updateTotalAsset(Int32.Parse(price));

                    // 통장 내역 추가
                    var dict = new Dictionary<string, string>();
                    dict.Add("bankbook", bankBook);
                    dict.Add("category", category);
                    dict.Add("place", place);
                    dict.Add("price", price);
                    dict.Add("memo", memo);
                    dict.Add("datetime", datetime);


                    Dictionary<string, ArrayList> hs = obj.History;
                    if (!hs.ContainsKey(date))
                    {
                        hs[date] = new ArrayList();
                        hs[date].Add(dict);
                    }
                    hs[date].Add(dict);
                    
                    obj.History = hs;
                    Console.WriteLine("{0} 통장에 입금이 완료되었습니다.", bankBook);
                    Console.WriteLine("총자산: {0}",obj.getTotalAsset);
                    Console.WriteLine("------------------");
                    return true;
                }
            }
            Console.WriteLine("통장이 존재하지 않습니다");
            return false;       
        }

        // 출금 -> 통장 명 또는 카드 명, 유형, 장소, 날짜, 금액, 메모
        public Boolean withDraw(string name, string category, string place, string price, string memo)
        {
            foreach (BankBook obj in bankBooks)
            {
                if (obj.getBankBookName == name)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string date = datetime.Split(" ")[0];

                    // 총 자산 업데이트
                    obj.updateTotalAsset(-Int32.Parse(price));

                    // 통장 내역 추가
                    var dict = new Dictionary<string, string>();
                    dict.Add("bankbook", name);
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
                    Console.WriteLine("{0} 통장에 출금이 완료되었습니다.", name);
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
                    Console.WriteLine("{0} 카드 사용이 완료되었습니다.", name);
                    Console.WriteLine("결제 예정 금액: {0}", price);
                    Console.WriteLine("------------------");
                    return true;
                }
            }
            Console.WriteLine("존재하지 않는 카드입니다.");
            return false;
        }

         // 총 자산 조회 -> 총 자산, 통장 별 잔고, 카드 별 결제 예정 금액 출력
        //public JsonSerializer getBalance() {
            // 총 자산 구하기
            // 통장 별 잔고 구하기
            // 카드 별 결제 예정 금액 구하기

            //string strJson = JsonSerializer.Serialize<>(, new JsonSerializerOptions() { WriteIndented = true });
            //return 1;
        //}

        // 통장 별 조회 => 통장명, 잔고
        public void getBankBookInfo() {
            var dict = new Dictionary<string, string>();

            foreach (BankBook bankbook in bankBooks)
            {
                dict[bankbook.getBankBookName] = bankbook.getTotalAsset;
            }

            foreach(var items in dict)
            {
                Console.WriteLine("통장명: {0}, 잔고: {1}", items.Key, items.Value);
            }
            Console.WriteLine("---------------------------");
        }

        // 카드 별 조회 => 카드 명, 결제 예정 금액
        public void getCardsInfo() {
            var dict = new Dictionary<string, int>();
            foreach (Card card in cards)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                foreach (Dictionary<string, string> items in card.History[date])
                {
                    if (!dict.ContainsKey(items["card"]))
                    {
                        dict[items["card"]] = 0;
                    }
                    dict[items["card"]] += Int32.Parse(items["price"]);
                }
            }

            foreach (var items in dict)
            {
                Console.WriteLine("카드명: {0}, 결제 예정 금액: {1}", items.Key, items.Value);
            }
            Console.WriteLine("---------------------------");
        }

        // 일별 가계부 조회 -> 유형, 장소, 금액, 일 총 입출금 금액
        public ArrayList getDailyHistory(string date) 
        {
            ArrayList al = new ArrayList();
            foreach(BankBook bankbook in bankBooks)
            {
                string strJson = JsonSerializer.Serialize(bankbook.History, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });
                al.Add(strJson);
            }

            Console.WriteLine("일별 가계부 조회");
            foreach (string item in al)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------------------------------");
            return al;
        }
        
        // 월별 가계부 조회 -> 월 총 입출금 금액, 월 카드 결제 금액, 월 유형 별 총 금액 및 비율
        //public void getMonthlyHistory(string date) {
        //    ArrayList al = new ArrayList();
        //    foreach(BankBook bankbook in bankBooks)
        //    {
        //        Console.WriteLine(bankbook.History.Keys);
        //        //string strJson = JsonSerializer.Serialize(bankbook.History, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true });
        //        //al.Add(strJson);
        //    }
        //}


    }
}
