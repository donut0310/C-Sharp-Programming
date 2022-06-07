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
		private string bankBookName; // 통장 이름(별칭)
		private string totalAsset; // 총 자산(잔고)
		private Dictionary<string, ArrayList> history;

		public BankBook(string name, string asset)
		{
			bankBookName = name;
			totalAsset = asset;
			history = new Dictionary<string, ArrayList>();
		}

		// Properties
		public string getBankBookName { get { return bankBookName; } }

		public string getTotalAsset {
			get { return totalAsset; }
		}

		public Dictionary<string, ArrayList> History
		{
			get { return history; }
			set { history = value; }
		}

		// Class Methods
		public void updateTotalAsset(int value)
        {
			totalAsset = (value + Int32.Parse(totalAsset)).ToString();
        }
	}
}
