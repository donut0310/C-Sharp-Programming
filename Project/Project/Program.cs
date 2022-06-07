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
        // 통장 등록 테스트
        // 자산 조회 테스트
        // 입금 테스트
        // 출금 테스트
        // 일별 가계부 조회 테스트
        // 월별 가계부 조회 테스트
    }
}