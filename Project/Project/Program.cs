using Project;
using System;
using System.IO;

class Program {
    static void Main(string[] args)
    {
        AccountBook acb = new AccountBook();

        /* 메소드 테스트 */

        // 카드 등록 테스트
        acb.enrollCard("꼬끼오 페이");
        acb.enrollCard("으르렁 페이");

        // 통장 등록 테스트
        acb.enrollBankBook("야옹", "100000");
        acb.enrollBankBook("멍멍", "100000");

        // 통장 입금 테스트
        acb.deposit("야옹", "월급", "세종컴퍼니", "2000000", null);
        acb.deposit("야옹","캐시백","띵동은행","3200",null);
        acb.deposit("멍멍", "캐시백", "부자은행", "2100", null);

        // 통장 출금 테스트
        acb.withDraw("야옹","적금","띵동은행","1000000",null);

        // 카드 출금 테스트
        acb.cardPayment("꼬끼오 페이","쇼핑","라뗴 백화점","320000",null);
        acb.cardPayment("꼬끼오 페이","오락","(주) RRR","10500",null);
        acb.cardPayment("으르렁 페이","오락","(주) RRR","10500",null);

        // 통장 별 조회 테스트
        acb.getBankBookInfo();

        // 카드 별 조회 테스트
        acb.getCardsInfo();

        // 총 자산 조회 테스트
        //acb.getBalance();

        // 일별 가계부 조회 테스트
        acb.getDailyHistory("2022-06-07"); // return ArrayList of Json string

        // 월별 가계부 조회 테스트
        //acb.getMonthlyHistory("2022-06"); // return ArrayList of Json string
    }
}