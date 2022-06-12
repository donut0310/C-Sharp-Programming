using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    public partial class MainWindow : Window
    {
        private AccountBook acb;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            this.Grid_AssetManagement.Visibility = Visibility.Visible;

            acb = new AccountBook();

            /* 메소드 테스트 */

            // 통장 등록 테스트
            acb.enrollBankBook("야옹", "100000");
            acb.enrollBankBook("멍멍", "500000");
            
            // 카드 등록 테스트
            acb.enrollCard("꼬끼오 페이");
            acb.enrollCard("으르렁 페이");

            // 통장 입금 테스트
            acb.deposit("야옹", "월급", "세종컴퍼니", "2000000", null);
            acb.deposit("야옹", "캐시백", "띵동은행", "3200", null);
            acb.deposit("멍멍", "캐시백", "부자은행", "2100", null);

            // 통장 출금 테스트
            acb.withDraw("야옹", "적금", "띵동은행", "1000000", null);
            acb.withDraw("멍멍", "적금", "간장은행", "500000", null);

            // 카드 결제 테스트 // return 
            acb.cardPayment("꼬끼오 페이", "쇼핑", "라뗴 백화점", "320000", null);
            acb.cardPayment("꼬끼오 페이", "식사", "우동마을", "7000", null);
            acb.cardPayment("꼬끼오 페이", "오락", "(주) RRR", "10500", null);
            acb.cardPayment("으르렁 페이", "오락", "(주) RRR", "10500", null);

            // 통장 별 조회 테스트 // return string, format => json
            acb.getBankBookInfo();

            // 카드 별 조회 테스트 // return string, format => json
            acb.getCardsInfo();

            // 총 자산 조회 테스트 // return string, format => json
            acb.getBalance();

            // 일별 가계부 조회 테스트 // return string, format => ArrayList of object(json)
            acb.getDailyHistory("2022-06-08");

            // 월별 가계부 조회 테스트 // return string, format => json
            acb.getMonthlyHistory("2022-06");

            DataGrid_Refresh();
        }

        // 통장 추가 Dialog 핸들링
        private void DialogHost_DialogClosing_AddBankBook(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(text_BankBookName.Text) && !string.IsNullOrWhiteSpace(text_BankBookAsset.Text))
            {
                acb.enrollBankBook(text_BankBookName.Text, text_BankBookAsset.Text);
                this.dg_BankBooks.ItemsSource = acb.getBankBookInfo();
            }
        }

        // 카드 추가 Dialog 핸들링
        private void DialogHost_DialogClosing_AddCard(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(text_CardName.Text))
            {
                acb.enrollCard(text_CardName.Text);
                this.dg_Cards.ItemsSource = acb.getCardsInfo();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            DragMove();
        }

        // 자산 관리 버튼
        private void Navbar_Btn_AssetManagement(object sender, RoutedEventArgs e)
        {
            this.Grid_TransferManagement.Visibility = Visibility.Collapsed;
            this.Grid_ShowAccountBook.Visibility = Visibility.Collapsed;
            this.Grid_ShowDevelopers.Visibility = Visibility.Collapsed;
            this.Grid_AssetManagement.Visibility = Visibility.Visible;
        }

        // 입출금 관리 버튼
        private void Navbar_Btn_TransferManagement(object sender, RoutedEventArgs e)
        {
            this.Grid_AssetManagement.Visibility = Visibility.Collapsed;
            this.Grid_ShowAccountBook.Visibility = Visibility.Collapsed;
            this.Grid_ShowDevelopers.Visibility = Visibility.Collapsed;
            this.Grid_TransferManagement.Visibility = Visibility.Visible;
        }

        // 가계부 조회 버튼
        private void Navbar_Btn_ShowAccountBook(object sender, RoutedEventArgs e)
        {
            this.Grid_TransferManagement.Visibility = Visibility.Collapsed;
            this.Grid_AssetManagement.Visibility = Visibility.Collapsed;
            this.Grid_ShowDevelopers.Visibility = Visibility.Collapsed;
            this.Grid_ShowAccountBook.Visibility = Visibility.Visible;
        }

        // 앱 정보 버튼
        private void Navbar_Btn_ShowDevelopers(object sender, RoutedEventArgs e)
        {
            this.Grid_TransferManagement.Visibility = Visibility.Collapsed;
            this.Grid_AssetManagement.Visibility = Visibility.Collapsed;
            this.Grid_ShowAccountBook.Visibility = Visibility.Collapsed;
            this.Grid_ShowDevelopers.Visibility = Visibility.Visible;
        }

        // 입금 버튼
        private void btn_Deposit(object sender, RoutedEventArgs e)
        {
            acb.deposit(
                text_deposit_BankAccountName.Text,
                text_deposit_Category.Text,
                text_deposit_Place.Text,
                text_deposit_Price.Text,
                text_deposit_Memo.Text
            );

            DataGrid_Refresh();

            MessageBox.Show("입금 처리 되었습니다.");

            text_deposit_BankAccountName.Text = "";
            text_deposit_Category.Text = "";
            text_deposit_Place.Text = "";
            text_deposit_Price.Text = "";
            text_deposit_Memo.Text = "";
        }

        // 출금 버튼
        private void btn_WithDrawal(object sender, RoutedEventArgs e)
        {
            acb.withDraw(
                text_withdrawal_BankAccountName.Text,
                text_withdrawal_Category.Text,
                text_withdrawal_Place.Text,
                text_withdrawal_Price.Text,
                text_withdrawal_Memo.Text
            );

            DataGrid_Refresh();

            MessageBox.Show("출금 처리 되었습니다.");

            text_withdrawal_BankAccountName.Text = "";
            text_withdrawal_Category.Text = "";
            text_withdrawal_Place.Text = "";
            text_withdrawal_Price.Text = "";
            text_withdrawal_Memo.Text = "";
        }

        private void DataGrid_Refresh()
        {
            string dateDaily = DateTime.Now.ToString("yyyy-MM-dd");
            string dateMonthly = DateTime.Now.ToString("yyyy-MM");

            this.label_TotalAssets.Content = "총 자산은 " + acb.getBalance()["totalAsset"].ToString() + " 원 입니다.";
            this.dg_BankBooks.ItemsSource = acb.getBankBookInfo();
            this.dg_Cards.ItemsSource = acb.getCardsInfo();
            this.dg_AccountBookByDays.ItemsSource = acb.getDailyHistory(dateDaily);
        }

        // Window 닫기 버튼
        private void btn_WindowClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ShowAccountBookByDays(object sender, RoutedEventArgs e)
        {
            String date = text_daily_DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            this.dg_AccountBookByDays.ItemsSource = acb.getDailyHistory(date);
        }

        private void btn_ShowAccountBookByMonths(object sender, RoutedEventArgs e)
        {
            String date = text_monthly_DatePicker.SelectedDate.Value.ToString("yyyy-MM");
            Dictionary<string, object> monthlyData = acb.getMonthlyHistory(date);
            label_totalDeposit.Content = "이번 달 입금 총 금액은 " + monthlyData["mTotalDeposit"] + " 원";
            label_totalWithdraw.Content = "이번 달 출금 총 금액은 " + monthlyData["mTotalwithdraw"] + " 원";
            label_cardPayAmt.Content = "이번 달 카드 결제 예정 금액은 " + monthlyData["mCardPayAmt"] + " 원";
        }

        /*
        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (!e.NewMode.Equals(CalendarMode.Year)) { MonthCal.DisplayMode = CalendarMode.Year; }
        }
        */
    }
}
