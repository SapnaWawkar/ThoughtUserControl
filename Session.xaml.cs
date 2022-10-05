using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace OnlineSession
{
    /// <summary>
    /// Interaction logic for Session.xaml
    /// </summary>
    public partial class Session : UserControl
    {
        private static string connectionString = @"Data Source=LAPTOP-QQJKA00R;Initial Catalog=OnlineSession;Integrated Security=True";    
    
        public List<SessionData> sessionDatas { get; set; }
        public Session()
        {
            InitializeComponent();
            sessionDatas = GetSessionData();
            this.DataContext = this;
        }
        public static List<SessionData> GetSessionData()
        {
            List<SessionData> sessions = new List<SessionData>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                        $"EXECUTE [dbo].[OnlineSessionData]", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SessionData sd = new SessionData();
                    sd.session = (string)reader[0];
                    sd.sessionId = (string)reader[1];
                    sd.date = (DateTime)reader[2];
                    sd.noOfPax = (int)reader[3];
                    sd.status = (string)reader[4];
                    sd.action = (string)reader[5];
                    sessions.Add(sd);
                }
                connection.Close();

            }
            return sessions;
        }
    }
    
    public class SessionData
    {
        public string session { get; set; }
        public string sessionId { get; set; }
        public DateTime date { get; set; }
        public int noOfPax { get; set; }
        public string status { get; set; }
        public string action { get; set; }
    }
}
