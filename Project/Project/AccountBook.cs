using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class AccountBook
    {
		Card[] cards;
		BankBook[] bankBooks;

		public AccountBook()
        {
            cards = new Card[0];
			bankBooks = new BankBook[0];
        }

        public void enrollCard(string name) {
			Card card = new Card(name);

            int arrSize = cards.Length;
            Array.Resize(ref cards, arrSize+1);

			cards[arrSize] = card;

			for(int i = 0; i < cards.Length; i++)
            {
				if(cards[i] != null)
                {
                    Console.WriteLine("{0}: 카드 명 >> {1}",i, cards[i].getCardName);
                }
                else
                {
					Console.WriteLine(i);
                }
            }
        } // 카드 등록
        public void enrollBankBook(string name, int asset) {
			BankBook book = new BankBook(name,asset);
		} // 통장 등록
		public void getBalance() { } // 자산 조회 -> 총 자산, 통장 별 잔고, 카드 별 결제 예정 금액 출력
		public void deposit() { } // 입금	-> 유형, 장소, 날짜, 금액, 메모,
		public void withDraw() { } // 출금 -> 유형, 장소, 날짜, 금액, 메모
		public void getDailyHistory() { } // 일별 가계부 조회 -> 유형, 장소, 금액, 일 총 입출금 금액
		public void getMonthlyHistory() { } // 월별 가계부 조회 -> 월 총 입출금 금액, 월 카드 결제 금액, 월 유형 별 총 금액 및 비율

	}
}
