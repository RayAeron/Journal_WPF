﻿using Journal_WPF.JournalTableAdapters;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : MetroWindow
    {
        Journal Journal;
        usersTableAdapter usersTableAdapter;
        markTableAdapter markTableAdapter;
        Mark_VTableAdapter Mark_VTableAdapter;
        disciplineTableAdapter disciplineTableAdapter;
        groupTableAdapter groupTableAdapter;
        string check = "add";
        public Admin()
        {
            InitializeComponent();

            Journal = new Journal();
            usersTableAdapter = new usersTableAdapter();
            usersTableAdapter.Fill(Journal.users);
            permiss_grid.ItemsSource = Journal.users.DefaultView;
            permiss_grid.CanUserDeleteRows = false;
            permiss_grid.CanUserAddRows = false;
            permiss_grid.IsReadOnly = true;

            groupTableAdapter = new groupTableAdapter();
            groupTableAdapter.Fill(Journal.group);
            group_grid.ItemsSource = Journal.group.DefaultView;
            group_grid.CanUserDeleteRows = false;
            group_grid.CanUserAddRows = false;
            group_grid.IsReadOnly = true;

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
            for (int i = 0; i < Journal.group.Rows.Count; i++)
            {
                string bd_group = Journal.group.Rows[i].ItemArray[1].ToString();
                group_s.Items.Add(bd_group);
            }

            for (int i = 0; i < Journal.discipline.Rows.Count; i++)
            {
                string bd_discipline = Journal.discipline.Rows[i].ItemArray[1].ToString();
                mark_d.Items.Add(bd_discipline);
            }
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
            Mark_VTableAdapter.FillBy2(Journal.Mark_V, group_search.Text);

            mark_student.Items.Clear();
            mark_d.Items.Clear();
            try
            {
                usersTableAdapter.FillBy3(Journal.users, int.Parse(group_iner));
                for (int i = 0; i < Journal.users.Rows.Count; i++)
                {
                    mark_student.Items.Add(Journal.users.Rows[i]["Email"]);
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


        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            Mark_VTableAdapter.FillBy(Journal.Mark_V, searh.Text);
        }

        private void mark_ad_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Добавить оценку"))
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
                    markTableAdapter.FillBy1(Journal.mark, mark_date.Text, Convert.ToInt32(discipline_iner), Convert.ToInt32(users_iner));
                    if (Journal.mark.Rows.Count.Equals(0))
                    {
                        if (mark_d.Text != "" && mark_student.Text != "" && mark_date.Text != "" && mark_n.Text != "")
                        {
                            markTableAdapter.InsertQuery(mark_n.Text, mark_date.Text, Convert.ToInt32(discipline_iner), Convert.ToInt32(users_iner));
                            Mark_VTableAdapter.Fill(Journal.Mark_V);
                            mark_d.Text = "";
                            mark_student.Text = "";
                            mark_date.Text = "";
                            mark_n.Text = "";
                        }
                        else MessageBox.Show("Введите данные");
                        Mark_VTableAdapter.Fill(Journal.Mark_V);
                    }
                    else MessageBox.Show("Нельзя добавить больше одной оценки в день по одной дисциплине!!!");
                }
                catch
                {

                }
            }
            if (((Button)sender).Content.Equals("Изменить оценку"))
            {
                //MessageBox.Show("Не работает, зайдите позже...");

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
                markTableAdapter.FillBy(Journal.mark, Convert.ToInt32(users_iner), mark_date.Text);
                if (!Journal.mark.Rows.Count.Equals(0))
                {
                    markTableAdapter.UpdateQuery(mark_select.Text, Convert.ToInt32(users_iner), mark_date.Text);
                    Mark_VTableAdapter.Fill(Journal.Mark_V);
                }
                mark_d.Text = "";
                mark_student.Text = "";
                mark_date.Text = "";
                mark_n.Text = "";
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void add_group_btn_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Добавить группу"))
            {
                groupTableAdapter.FillBy2(Journal.group, group_name.Text, code.Text, name.Text);
                if (Journal.mark.Rows.Count.Equals(0))
                {
                    if (group_name.Text != "" && name.Text != "" && code.Text != "")
                    {
                        groupTableAdapter.InsertQuery(group_name.Text, code.Text, name.Text);
                    }
                    else MessageBox.Show("Введите данные");
                }
                else MessageBox.Show("Нельзя добавлять одинаковые группы!!!");
            }
            if (((Button)sender).Content.Equals("Изменить группу"))
            {
                groupTableAdapter.FillBy3(Journal.group, code.Text, name.Text);
                if (!Journal.group.Rows.Count.Equals(0))
                {
                    groupTableAdapter.UpdateQuery(group_name.Text, code.Text, name.Text);
                }
 
            }
            groupTableAdapter.Fill(Journal.group);
            group_s.Items.Clear();
            for (int i = 0; i < Journal.group.Rows.Count; i++)
            {
                string bd_group = Journal.group.Rows[i].ItemArray[1].ToString();
                group_s.Items.Add(bd_group);
            } 
            group_name.Text = "";
            name.Text = "";
            code.Text = "";
        }

        private void check_mark(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            check = radioButton.Name;
            if (check == "edit")
            {
                mark_ad.Content = "Изменить оценку";
            }
            else
            {
                mark_ad.Content = "Добавить оценку";
            }
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            mark_date.Text = Convert.ToString(date_picker.Text);
        }

        private void group_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selecteDataRow = (DataRowView)group_grid.SelectedItem;
            if (selecteDataRow != null)
            {
                group_id.Content = selecteDataRow.Row.ItemArray[0].ToString();
                group_name.Text = selecteDataRow.Row.ItemArray[1].ToString();
                code.Text = selecteDataRow.Row.ItemArray[2].ToString();
                name.Text = selecteDataRow.Row.ItemArray[3].ToString();
            }
        }

        private void group_b_Click(object sender, RoutedEventArgs e)
        {
            groupTableAdapter.FillBy(Journal.group, group_s.Text);
        }

        private void canv_next(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Оценки"))
            {
                canv_mark_add_edit.Visibility = Visibility.Visible;
                canv_group_add_edit.Visibility = Visibility.Hidden;
                permiss_edit.Visibility = Visibility.Hidden;
            }
            if (((Button)sender).Content.Equals("Группы"))
            {
                canv_group_add_edit.Visibility = Visibility.Visible;
                canv_mark_add_edit.Visibility = Visibility.Hidden;
                permiss_edit.Visibility = Visibility.Hidden;
            }
            if (((Button)sender).Content.Equals("Права пользователей"))
            {
                permiss_edit.Visibility = Visibility.Visible;
                canv_mark_add_edit.Visibility = Visibility.Hidden;
                canv_group_add_edit.Visibility = Visibility.Hidden;
            }
        }

        private void check_group(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            check = radioButton.Name;
            if (check == "edit_group")
            {
                add_group_btn.Content = "Изменить группу";
            }
            else
            {
                add_group_btn.Content = "Добавить группу";
            }
        }

        private void del_group_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedatarow = (DataRowView)group_grid.SelectedItem;
                groupTableAdapter.DeleteQuery(Convert.ToInt32(selectedatarow.Row.ItemArray[0]));
                groupTableAdapter.Fill(Journal.group);
            }
            catch
            {
                MessageBox.Show("Нельзя удалить так, как к этой группе уже привязанны студенты!!!");
            }
            
        }

        private void mark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selecteDataRow = (DataRowView)mark.SelectedItem;
            if (selecteDataRow != null)
            {
                mark_select.Text = selecteDataRow.Row.ItemArray[0].ToString();
                date_picker.Text = selecteDataRow.Row.ItemArray[1].ToString();
                mark_student.Text = selecteDataRow.Row.ItemArray[5].ToString();
                mark_d.Text = selecteDataRow.Row.ItemArray[6].ToString();
            }
        }

        private void edit_permiss_Click(object sender, RoutedEventArgs e)
        {
            if (permiss_combo.Text != "")
            {
                usersTableAdapter.FillBy4(Journal.users, Convert.ToString(login.Content), Convert.ToString(permiss.Content));
                if (!Journal.users.Rows.Count.Equals(0))
                {
                    usersTableAdapter.UpdateQuery5(permiss_combo.Text, Convert.ToString(login.Content));
                    usersTableAdapter.Fill(Journal.users);
                    permiss_combo.Text = null;
                    edit_permiss.IsEnabled = false;
                }
            }else MessageBox.Show("Выберите один из вариантов из списка!");
        }

        private void permiss_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selecteDataRow = (DataRowView)permiss_grid.SelectedItem;
            if (selecteDataRow != null)
            {
                permiss.Content = selecteDataRow.Row.ItemArray[6].ToString();
                login.Content = selecteDataRow.Row.ItemArray[4].ToString();
                edit_permiss.IsEnabled = true;
                string log = Convert.ToString(permiss.Content);
                if (log == "adm")
                {
                    edit_permiss.IsEnabled = false;
                }
                else
                {
                    edit_permiss.IsEnabled = true;
                }
            }
        }

        private void permiss_grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Pass")
            {
                e.Cancel = true;
            }
        }

        private void searh_student_btn_Click(object sender, RoutedEventArgs e)
        {
            usersTableAdapter.FillBy(Journal.users, searh_student.Text);
        }
    }
}
