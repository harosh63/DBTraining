using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DBTraining.Model;
using DBTraining.View;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;



namespace DBTraining.ViewModel
{
    class DBTraining_VM : INotifyPropertyChanged
    {
        DBAdd_view dbadd;
        DBUpd_view dbupd;
        public DBTraining_VM()
        {
            LoadDB_Command = new DelegateCommand(LoadDB);
            RowAdd_Command = new DelegateCommand(RowAdd);
            RowUpd_Command = new DelegateCommand(RowUpd);
            RowDel_Command = new DelegateCommand(RowDel);
        }

        private ObservableCollection<DBTraining_Model> peoples =
            new ObservableCollection<DBTraining_Model>();
        public ObservableCollection<DBTraining_Model> Peoples
        {
            get => peoples;
            set
            {
                peoples = value;
                OnPropertyChanged("Peoples");
            }
        }

        private DBTraining_Model selectedItem;
        public DBTraining_Model SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public DelegateCommand LoadDB_Command { get; set; }
        private void LoadDB(object obj)
        {
            string oradb = "User ID=ONLYBBQ;Data Source=localhost:1521/XEPDB1;Password=PSWRD123;";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = oradb;
            con.Open();
            OracleCommand cmd = new OracleCommand
            {
                CommandText = "select * from example order by people_id",
                Connection = con
            };
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                peoples.Clear();
                while (dr.Read())
                {
                    peoples.Add(new DBTraining_Model
                    {
                        Fio = dr["Fio"].ToString(),
                        Age = Int32.Parse(dr["Age"].ToString()),
                        Adress = dr["Adress"].ToString(),
                        Date = Convert.ToDateTime(dr["Datetime"]),
                        ID = Int32.Parse(dr["people_id"].ToString())
                    });
                }
            }
            else
            {
            }
            con.Close();
            con.Dispose();
        }
        public DelegateCommand RowAdd_Command { get; set; }
        private void RowAdd(object obj)
        {
            dbadd = new DBAdd_view();
            dbadd.ShowDialog();
            LoadDB(obj);
        }
        public DelegateCommand RowUpd_Command { get; set; }
        private void RowUpd(object obj)
        {
            dbupd = new DBUpd_view();
            dbupd.textFio.Text = this.SelectedItem.Fio;
            dbupd.textAge.Text = this.SelectedItem.Age.ToString();
            dbupd.textAdress.Text = this.SelectedItem.Adress;
            dbupd.textDatetime.SelectedDate = this.SelectedItem.Date;
            dbupd.textID.Content = this.SelectedItem.ID;
            dbupd.ShowDialog();
            LoadDB(obj);
        }
        public DelegateCommand RowDel_Command { get; set; }
        private void RowDel(object obj)
        {
            string oradb = "User ID=ONLYBBQ;Data Source=localhost:1521/XEPDB1;Password=PSWRD123;";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = oradb;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            if (SelectedItem != null)
                cmd.CommandText = "delete from example where people_id=" + selectedItem.ID;
            else
            { 
                MessageBox.Show("Не выбрано строки для удаления");
                return;
            }
            cmd.Connection = con;
            OracleDataReader dr = cmd.ExecuteReader();
            con.Close();
            con.Dispose();

            LoadDB(obj);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
