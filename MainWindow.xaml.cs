using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using Journal_WPF.JournalTableAdapters;
using MahApps.Metro.Controls;

namespace Journal_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Journal Journal;
        usersTableAdapter usersTableAdapter;

        public MainWindow()
        {
            InitializeComponent();
            Journal = new Journal();
            usersTableAdapter = new usersTableAdapter();
            usersTableAdapter.Fill(Journal.users);
        }
        private void pass_recovery_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //reck_pass reck_pass = new reck_pass();
            //reck_pass.Show();
            //this.Close();
        }
        private void Back_Focus(Canvas login_canv, Canvas reg_canv)
        {
            login_canv.Visibility = Visibility.Hidden;
            reg_canv.Visibility = Visibility.Visible;
            login_l.Clear();
            pass_l.Clear();
            login_r.Clear();
            pass_r.Clear();
            error.Content = null;
            error1.Content = null;
        }

        private void back_l(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Назад"))
            {
                Back_Focus(reg_canv, login_canv);
            }
            else
            {
                Back_Focus(login_canv, reg_canv);
            }
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            if (login_r.Text != "" && pass_r.Password != "" )
            {
                usersTableAdapter.FillBy(Journal.users, login_r.Text);
                if (Journal.users.Rows.Count.Equals(0))
                {
                    string group_iner = null;
                    
                    string permiss = "no";
                    //usersTableAdapter.InsertQuery(surname.Text, name.Text, patronymic.Text, login_r.Text, pass_r.Password, permiss);
                    MailAddress from1 = new MailAddress("balance.emulation.card@gmail.com", "Register");
                    MailAddress to1 = new MailAddress(login_r.Text);
                    MailMessage m1 = new MailMessage(from1, to1);
                    m1.Subject = "Регистрация";
                    m1.Body = "Регистрация прошла успешно. Ваш логин:  " + login_r.Text + " | Ваш пароль: " + pass_r.Password + " |";
                    m1.IsBodyHtml = true;
                    SmtpClient smtp1 = new SmtpClient("smtp.gmail.com", 587);
                    smtp1.Credentials = new NetworkCredential("balance.emulation.card@gmail.com", "testbalance");
                    smtp1.EnableSsl = true;
                    smtp1.Send(m1);
                    Back_Focus(reg_canv, login_canv);
                    error.Content = "Регистрация прошла успешно";
                }
                else error1.Content = "Логин существует";
            }
            else error1.Content = "Введите данные";
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            if (login_l.Text != "" && pass_l.Password != "")
            {
                usersTableAdapter.FillBy1(Journal.users, login_l.Text, pass_l.Password);
                if (!Journal.users.Rows.Count.Equals(0))
                {
                    //string name = login_l.Text;
                    //mainPolyclinic mainPolyclinic = new mainPolyclinic(name);
                    //mainPolyclinic.Show();
                    //this.Close();
                }
                else error.Content = "Логин или пароль не совпадают";
            }
            else error.Content = "Введите данные";
        }
    }
}
