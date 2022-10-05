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

namespace ThoughtUserControl
{
    /// <summary>
    /// Interaction logic for Thought1.xaml
    /// </summary>
    public partial class Thought1 : UserControl
    {
        private static string connectionString = @"Data Source=LAPTOP-QQJKA00R;Initial Catalog=Thoughts;Integrated Security=True";
        public List<Thoughts> Though { get; set; }
        public Thought1()
        {
            InitializeComponent();
            Though=GetThoughts();
            this.DataContext = this;
        }
        public static List<Thoughts> GetThoughts()
        {
            List<Thoughts> thoughts = new List<Thoughts>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command =
                    new SqlCommand(
                        $"EXECUTE [dbo].[GetThoughts]",connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Thoughts ths=new Thoughts();
                    ths.Title = (string)reader[0];
                    ths.FirstName = (string)reader[1];
                    ths.LastName = (string)reader[2];
                    ths.Technology = (string)reader[3];
                    ths.PostDate = (DateTime)reader[4];
                    ths.content = (string)reader[5];
                    thoughts.Add(ths);
                    
                }
                connection.Close();

            }
            return thoughts;
        }
    }
    public class Thoughts
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Technology { get; set; }
        public DateTime PostDate { get; set; }
        public string content { get; set; }
    }
}
