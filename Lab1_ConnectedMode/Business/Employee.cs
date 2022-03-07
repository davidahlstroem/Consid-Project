using Lab1_ConnectedMode.DataAccess;
using System.Collections.Generic;

namespace Lab1_ConnectedMode.Business
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public bool IsCeo { get; set; }
        public bool IsManager { get; set; }
        public int ManagerID { get; set; }

        public bool IsUniqueEmpId(int id)
        {
            return EmployeeDB.IsUniqueId(id);
        }

        public bool IsSameEmpId(int NewId, int OldId)
        {
            return EmployeeDB.SameId(NewId, OldId);
        }
        public void SaveEmployee(Employee emp)
        {
            EmployeeDB.SaveRecord(emp);
        }

        public bool CheckIfCEO()
        {
            return EmployeeDB.OnlyOneCeo();
        }

        public List<Employee> ListEmployee()
        {
            return EmployeeDB.ListRecord();
        }
        public void UpdateEmployee(Employee emp, int OldId)
        {
            EmployeeDB.UpdateRecord(emp, OldId);
        }

        public void DeleteEmployee(int Id)
        {
            EmployeeDB.DeleteRecord(Id);
        }

    }
}
