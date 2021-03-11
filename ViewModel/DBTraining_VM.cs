using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.VsDevTools;

namespace DBTraining.ViewModel
{
    class DBTraining_VM : INotifyPropertyChanged
    {
        OracleConnection con;
        //void Connect()
        //{
        //    con = new OracleConnection();
        //    con.ConnectionString = "User ID=ONLYBBQ; Password=PSWRD123; Data Source=localhost:1521/XEPDB1";
        //    con.Open();
        //    Console.WriteLine("Connected: " + con.ServerVersion);
        //}
        //public DBTraining_VM()
        //{
        //    Connect();
            
        //}

        private ObservableCollection<Model.DBTraining_Model> peoples =
            new ObservableCollection<Model.DBTraining_Model>();
        public ObservableCollection<Model.DBTraining_Model> Peoples
        {
            get => peoples;
            set
            {
                peoples = value;
                OnPropertyChanged("Peoples");
            }
        }

        private void LoadDB()
        {
            peoples.Add(new Model.DBTraining_Model
            {

            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
