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
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using DBTraining.Model;

namespace DBTraining.View
{
    /// <summary>
    /// Логика взаимодействия для DBAdd_view.xaml
    /// </summary>
    public partial class DBAdd_view : Window
    {
        public DBAdd_view()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string oradb = "User ID=ONLYBBQ;Data Source=localhost:1521/XEPDB1;Password=PSWRD123;";
            string insertQuery = "insert into example values (:fio,:age,:adress,:datetime, people_seq.NEXTVAL)";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = oradb;
            con.Open();
            OracleCommand cmd = new OracleCommand
            {
                CommandText = insertQuery,
                Connection = con
            };

            try
            {
                cmd.Parameters.Add(new OracleParameter("fio", OracleDbType.NVarchar2, textFio.Text, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("age", OracleDbType.Int32, textAge.Text, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("adress", OracleDbType.NVarchar2, textAdress.Text, System.Data.ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("datetime", OracleDbType.Date, textDatetime.SelectedDate, System.Data.ParameterDirection.Input));
            }
            catch
            {
                OracleException exp;
            }
            OracleDataReader dr = cmd.ExecuteReader();
            con.Close();
            con.Dispose();
            DialogResult = true;
            
        }

        private void textAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
