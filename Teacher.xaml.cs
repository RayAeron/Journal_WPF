﻿using Journal_WPF.JournalTableAdapters;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Journal_WPF
{
    /// <summary>
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : MetroWindow
    {
        Journal Journal;
        usersTableAdapter usersTableAdapter;
        markTableAdapter markTableAdapter;
        Mark_VTableAdapter Mark_VTableAdapter;
        disciplineTableAdapter disciplineTableAdapter;
        groupTableAdapter groupTableAdapter;
        string trysi;
        public Teacher()
        {
            InitializeComponent();

            Journal = new Journal();
            usersTableAdapter = new usersTableAdapter();
            usersTableAdapter.Fill(Journal.users);

            groupTableAdapter = new groupTableAdapter();
            groupTableAdapter.Fill(Journal.group);

            Mark_VTableAdapter = new Mark_VTableAdapter();
            Mark_VTableAdapter.Fill(Journal.Mark_V);
            mark.ItemsSource = Journal.Mark_V.DefaultView;
            mark.CanUserDeleteRows = false;
            mark.CanUserAddRows = false;
            mark.IsReadOnly = true;

            markTableAdapter = new markTableAdapter();
            markTableAdapter.Fill(Journal.mark);

            disciplineTableAdapter = new disciplineTableAdapter();
            disciplineTableAdapter.Fill(Journal.discipline);
            for (int i = 0; i < Journal.users.Rows.Count; i++)
            {
                string student_tyty = Journal.users.Rows[i].ItemArray[4].ToString();
                if (Journal.users.Rows[i].ItemArray[6].ToString() == "no")
                {
                    mark_student.Items.Add(student_tyty);
                }

            }

            for (int i = 0; i < Journal.group.Rows.Count; i++)
            {
                string bd_group = Journal.group.Rows[i].ItemArray[1].ToString();
                group_search.Items.Add(bd_group);
            }

            for (int i = 0; i < Journal.discipline.Rows.Count; i++)
            {
                string bd_discipline = Journal.discipline.Rows[i].ItemArray[1].ToString();
                mark_d.Items.Add(bd_discipline);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        private void mark_Focus(Canvas main_canv, Canvas mark_canve)
        {
            main_canv.Visibility = Visibility.Hidden;
            mark_canv.Visibility = Visibility.Visible;
        }
        private void main_mark_Focus(Canvas main_canv, Canvas mark_canv)
        {
            mark_canv.Visibility = Visibility.Hidden;
            main_canv.Visibility = Visibility.Visible;
            searh.Clear();
            mark_select.Text = null;
            mark_n.Clear();
            date_picker.Text = null;
            mark_d.Text = null;
            mark_student.Text = null;
        }
        private void person_Focus(Canvas main_canv, Canvas person_canv)
        {
            main_canv.Visibility = Visibility.Hidden;
            person_canv.Visibility = Visibility.Visible;
        }
        private void main_person_Focus(Canvas main_canv, Canvas person_canv)
        {
            person_canv.Visibility = Visibility.Hidden;
            main_canv.Visibility = Visibility.Visible;
            surname_t.Clear();
            name_t.Clear();
            patronymic_t.Clear();
        }
        private void back_l(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Добавить оценку"))
            {
                mark_Focus(main_canv, mark_canv);
                Title = "Добавить оценку";

            }
            if (((Button)sender).Content.Equals("Назад"))
            {
                main_mark_Focus(main_canv, mark_canv);
                Title = "Панель преподавателя";
            }
            if (((Button)sender).Content.Equals("Личный кабинет"))
            {
                person_Focus(main_canv, person_canv);
                Title = "Личный кабинет";
                trysi = Convert.ToString(login.Content);

                usersTableAdapter.FillBy(Journal.users, Convert.ToString(login.Content));
                if (!Journal.users.Rows.Count.Equals(0))
                {
                    surname_s.Content = Journal.users.Rows[0].ItemArray[1].ToString();
                    name_s.Content = Journal.users.Rows[0].ItemArray[2].ToString();
                    patronymic_s.Content = Journal.users.Rows[0].ItemArray[3].ToString();

                }
            }
            if (((Button)sender).Content.Equals("Назад"))
            {
                main_person_Focus(main_canv, person_canv);
                Title = "Панель преподавателя";
            }
        }
        private void surname_b_Click(object sender, RoutedEventArgs e)
        {
            if (surname_t.Text != "")
            {
                usersTableAdapter.UpdateQuery1(surname_t.Text, trysi);
                surname_s.Content = surname_t.Text;
                surname_t.Text = null;
            }
            else MessageBox.Show("Введите данные");
        }

        private void name_b_Click(object sender, RoutedEventArgs e)
        {
            if (name_t.Text != "")
            {
                usersTableAdapter.UpdateQuery2(name_t.Text, trysi);
                name_s.Content = name_t.Text;
                name_t.Text = null;
            }
            else MessageBox.Show("Введите данные");
        }

        private void patronymic_b_Click(object sender, RoutedEventArgs e)
        {
            if (patronymic_t.Text != "")
            {
                usersTableAdapter.UpdateQuery3(patronymic_t.Text, trysi);
                patronymic_s.Content = patronymic_t.Text;
                patronymic_t.Text = null;
            }
            else MessageBox.Show("Введите данные");
        }

        private void mark_ad_Click(object sender, RoutedEventArgs e)
        {
            mark_n.Text = mark_select.Text;
            try
            {
                string users_iner = "";
                usersTableAdapter.FillBy(Journal.users, mark_student.Text);
                if (!Journal.users.Rows.Count.Equals(0))
                {
                    for (int i = 0; i < Journal.users.Rows.Count; i++)
                    {
                        string users_id = Convert.ToString(Journal.users.Rows[i]["id_user"]);
                        users_iner = users_id;
                    }
                }

                string discipline_iner = "";
                disciplineTableAdapter.FillBy(Journal.discipline, mark_d.Text);
                if (!Journal.discipline.Rows.Count.Equals(0))
                {
                    for (int i = 0; i < Journal.discipline.Rows.Count; i++)
                    {
                        string discipline_id = Convert.ToString(Journal.discipline.Rows[i]["id_discipline"]);
                        discipline_iner = discipline_id;
                    }
                }

                if (mark_d.Text != "" && mark_student.Text != "" && mark_date.Text != "" && mark_n.Text != "")
                {
                    markTableAdapter.InsertQuery(mark_n.Text, mark_date.Text, Convert.ToInt32(discipline_iner), Convert.ToInt32(users_iner));
                }
                else MessageBox.Show("Введите данные");
                Mark_VTableAdapter.Fill(Journal.Mark_V);
            }
            catch
            {

            }
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            mark_date.Text = Convert.ToString(date_picker.Text);
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            Mark_VTableAdapter.FillBy(Journal.Mark_V, searh.Text);
        }

        private void group_btn_Click(object sender, RoutedEventArgs e)
        {
            string group_iner = "";
            groupTableAdapter.FillBy(Journal.group, group_search.Text);
            if (!Journal.group.Rows.Count.Equals(0))
            {
                for (int i = 0; i < Journal.group.Rows.Count; i++)
                {
                    string grop_id = Convert.ToString(Journal.group.Rows[i]["id_group"]);
                    group_iner = grop_id;
                }
            }
            num.Content = group_iner;
            Mark_VTableAdapter.FillBy2(Journal.Mark_V, group_search.Text);

            mark_student.Items.Clear();
            mark_d.Items.Clear();
            try
            {
                for (int i = 0; i < Journal.Mark_V.Rows.Count; i++)
                {
                    if (!(mark_student.Items.Contains(Journal.Mark_V.Rows[i]["Почта"])))
                    {
                        mark_student.Items.Add(Journal.Mark_V.Rows[i]["Почта"]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Выберите студента");
            }
            try
            {
                disciplineTableAdapter.FillBy1(Journal.discipline, int.Parse(group_iner));
                for (int i = 0; i < Journal.discipline.Rows.Count; i++)
                {
                    mark_d.Items.Add(Journal.discipline.Rows[i]["Name_discipline"]);
                }
            }
            catch
            {
                MessageBox.Show("Выберите группу");
            }
        }
    }
}
