using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Card
    {
		private string cardName; // 카드 이름(별칭)
		private Hashtable history; // 결제 예정 금액 내역
								   // Values of object in history => 입출금 유형, 장소, 날짜, 금액, 메모
		public Card(string name)
		{
			cardName = name;
		}

		// Properties
		public string getCardName
		{
			get { return cardName; }
		}

		public Hashtable getHistory
		{
			get { return history; }
		}
	}
}
