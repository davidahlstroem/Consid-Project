using Lab1_ConnectedMode.Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab1_ConnectedMode.DataAccess
{

    public static class EmployeeDB
    {
        public class ControlID
        {
            public string TextData { get; set; }

        }

        // check in datebase if ID is unique
        public static bool IsUniqueId(int tempId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "SELECT * FROM Employees " +
                                " WHERE EmpID= " + tempId;
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            if (id != 0)
            {
                return false;
            }
            return true;

        }

        // return a bool, true if there already are an CEO in the datebase
        public static bool OnlyOneCeo()
        {

            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "SELECT * FROM Employees " +
                                " WHERE IsCEO= 1;";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // check if the unique id is the same as the old one
        public static bool SameId(int newId, int OldId)
        {
            if (newId == OldId) { return true; }
            else if (IsUniqueId(newId)) { return true; }
            else return false;
        }

        // Save employee in database
        public static void SaveRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "INSERT INTO Employees(EmpID,FirstName,LastName,Salary,IsCEO,IsManager,ManagerID) " +
                              " VALUES (@EmpID, @FirstName, @LastName, @Salary, @IsCEO, @IsManager, @ManagerID) ";
            cmd.Parameters.AddWithValue("@EmpID", emp.EmployeeId);
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@IsCEO", emp.IsCeo);
            cmd.Parameters.AddWithValue("@IsManager", emp.IsManager);
            cmd.Parameters.AddWithValue("@ManagerID", emp.ManagerID);
            cmd.ExecuteNonQuery();
            connDB.Close();

        }

        // List all emp's in datebase
        public static List<Employee> ListRecord()
        {
            List<Employee> listEmp = new List<Employee>();

            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", connDB);
            SqlDataReader reader = cmd.ExecuteReader();
            Employee emp;
            while (reader.Read())
            {

                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader["EmpID"]);
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.Salary = Convert.ToDecimal(reader["Salary"]);
                emp.IsCeo = Convert.ToBoolean(reader["IsCeo"]);
                emp.IsManager = Convert.ToBoolean(reader["IsManager"]);
                emp.ManagerID = Convert.ToInt32(reader["ManagerID"]);
                listEmp.Add(emp);
            }
            connDB.Close();
            return listEmp;
        }

        // uppdate emp in database
        public static void UpdateRecord(Employee emp, int OldId)
        {
            using (SqlConnection connDB = UtilityDB.ConnectDB())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDB;
                cmd.CommandText = "UPDATE Employees " +
                                  " SET EmpID = @EmpID, " +
                                    " FirstName = @FirstName, " +
                                    " LastName = @LastName, " +
                                    " Salary = @Salary, " +
                                    " IsCEO = @IsCEO, " +
                                    " IsManager = @IsManager, " +
                                    " ManagerID = @ManagerID" +
                                    " WHERE EmpID = @OldId ";
                cmd.Parameters.AddWithValue("@EmpID", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@IsCEO", emp.IsCeo);
                cmd.Parameters.AddWithValue("@IsManager", emp.IsManager);
                cmd.Parameters.AddWithValue("@ManagerID", emp.ManagerID);
                cmd.Parameters.AddWithValue("@OldId", OldId);
                cmd.ExecuteNonQuery();
                connDB.Close();
            }

        }

        //delete emp in database
        public static void DeleteRecord(int empId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "DELETE FROM Employees " +
                                " WHERE EmpID= " + empId;
            cmd.ExecuteNonQuery();
            connDB.Close();

        }
    }
}
