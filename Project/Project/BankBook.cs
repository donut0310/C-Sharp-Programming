using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class BankBook
    {
		private string name; // 통장 이름(별칭)
		private int totalAsset; // 총 자산(잔고)
		private Hashtable history; // 입출금 내역 Hashtable of objects
								   // Values of object in history => 입출금 유형, 장소, 날짜, 금액, 메모

		public BankBook(string name, int asset)
		{
			name = name;
			totalAsset = asset;
			history = null;
		}

		// Properties
		public string getBankBookName { get { return name; } }

		public int getBankBookAsset{ get { return totalAsset;} }

		public Hashtable getHistory { get { return history; } }
	}
}
