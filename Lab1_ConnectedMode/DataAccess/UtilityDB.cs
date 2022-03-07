using System.Data.SqlClient;


namespace Lab1_ConnectedMode.DataAccess
{
    public static class UtilityDB
    {
        public static SqlConnection ConnectDB()
        {
            SqlConnection connDB = new SqlConnection();
            connDB.ConnectionString = ConsidProject.Properties.Resources.ConnectionString; // Change to your database adress in Resources.resx
            connDB.Open();
            return connDB;
        }
    }
}
