using Lab1_ConnectedMode.Business;
using Lab1_ConnectedMode.Validation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab1_ConnectedMode.GUI
{
    public partial class FormEmployee : Form
    {
        
        private string InvalidData = "'Invalid Data', MessageBoxButtons.OK, MessageBoxIcon.Warning";
        public int CurrentIdData;
        public FormEmployee()
        {
            InitializeComponent();

        }
        private void FormEmployee_Load(object sender, EventArgs e)
        {

        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        // Validate input, then add the info to an Employee object. Save it to database.
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            string input = "";
            input = textBoxEmpId.Text.Trim();
            if (!Validator.IsValidId(input, 4))
            {
                MessageBox.Show("Employee ID must be 4-digit number.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;

            }
            input = textBoxManagerID.Text.Trim();
            if (!Validator.IsValidId(input))
            {
                MessageBox.Show("Manager ID must be a digit number.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;

            }

            int tempId = Convert.ToInt32(textBoxEmpId.Text.Trim());
            if (!(emp.IsUniqueEmpId(tempId)))
            {
                MessageBox.Show("Employee ID already exists.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;
            }

            input = textBoxFirstName.Text.Trim();
            if (!Validator.IsValidName(input))
            {
                MessageBox.Show("First name must contain letters or space(s)", InvalidData);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            input = textBoxLastName.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("Last name must contain letters or space(s)", InvalidData);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            emp.EmployeeId = Convert.ToInt32(textBoxEmpId.Text.Trim());
            emp.FirstName = textBoxFirstName.Text.Trim();
            emp.LastName = textBoxLastName.Text.Trim();
            CalculateSalary(emp);
            checkIfCEO(emp);
            if (checkBoxIsManager.Checked) { emp.IsManager = true; }
            if (emp.IsManager) { emp.ManagerID = Convert.ToInt32(textBoxManagerID.Text.Trim()); } else { emp.ManagerID = 0; }
            emp.SaveEmployee(emp);
            MessageBox.Show("Employee record has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonListAll.PerformClick();
            ClearAll();

        }

        // list all the employees 
        private void buttonListAll_Click(object sender, EventArgs e)
        {

            Employee emp = new Employee();
            List<Employee> listEmp = new List<Employee>();
            listEmp = emp.ListEmployee();
            listViewEmployee.Items.Clear();
            if (listEmp.Count != 0)
            {

                foreach (Employee anEmp in listEmp)
                {
                    ListViewItem item = new ListViewItem(anEmp.EmployeeId.ToString());
                    item.SubItems.Add(anEmp.FirstName);
                    item.SubItems.Add(anEmp.LastName);
                    item.SubItems.Add(anEmp.Salary.ToString());
                    item.SubItems.Add(anEmp.IsCeo.ToString());
                    item.SubItems.Add(anEmp.IsManager.ToString());
                    item.SubItems.Add(anEmp.ManagerID.ToString());
                    listViewEmployee.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Employee list is empty" + "\n" + "Please enter Employee Data", InvalidData);
            }
        }

        // if you click on an item in the employeelist, save it to the input-boxes
        private void listViewEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listViewEmployee.SelectedItems.Count > 0)
            {

                ListViewItem item = listViewEmployee.SelectedItems[0];
                textBoxEmpId.Text = item.SubItems[0].Text;
                CurrentIdData = Convert.ToInt32(textBoxEmpId.Text.Trim());
                textBoxFirstName.Text = item.SubItems[1].Text;
                textBoxLastName.Text = item.SubItems[2].Text;
                textBoxSalary.Text = item.SubItems[3].Text;
                if (item.SubItems[4].Text == "True") { checkBoxIsCEO.Checked = true; };
                if (item.SubItems[5].Text == "True") { checkBoxIsManager.Checked = true; };
                textBoxManagerID.Text = item.SubItems[6].Text;

            }
            else
            {
                textBoxEmpId.Text = string.Empty;
                textBoxFirstName.Text = string.Empty;
                textBoxLastName.Text = string.Empty;
                textBoxSalary.Text = string.Empty;
                checkBoxIsCEO.Checked = false;
                checkBoxIsManager.Checked = false;
                textBoxManagerID.Text = string.Empty;
            }
        }

        // Validate, add info to Employee object. Update employee in the database
        private void buttonUpdate_Click(object sender, EventArgs e)
        {

            string input = textBoxEmpId.Text.Trim();
            Employee emp = new Employee();
            if (!Validator.IsValidId(input, 4))
            {
                MessageBox.Show("Employee ID must be 4-digit number.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;

            }

            input = textBoxManagerID.Text.Trim();
            if (!Validator.IsValidId(input))
            {
                MessageBox.Show("Manager ID must be a digit number.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;

            }

            input = textBoxFirstName.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("First name must contain letters or space(s)", InvalidData);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }
            emp.FirstName = textBoxFirstName.Text.Trim();

            input = textBoxLastName.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("Last name must contain letters or space(s)", InvalidData);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }
            emp.LastName = textBoxLastName.Text.Trim();

            emp.EmployeeId = Convert.ToInt32(textBoxEmpId.Text.Trim());
            if (!emp.IsSameEmpId(emp.EmployeeId, CurrentIdData))
            {
                MessageBox.Show("Employee ID must be unique.", InvalidData);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;
            };
            CalculateSalary(emp);
            checkIfCEO(emp);
            if (checkBoxIsManager.Checked) { emp.IsManager = true; }
            emp.ManagerID = Convert.ToInt32(textBoxManagerID.Text.Trim());
            emp.UpdateEmployee(emp, CurrentIdData);
            MessageBox.Show("Employee record has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonListAll.PerformClick();

        }

        // delete employee (selected in the emplist) in the database with the use of ID number
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            int Id;
            if (listViewEmployee.SelectedItems.Count > 0)
            {

                ListViewItem item = listViewEmployee.SelectedItems[0];
                Id = Convert.ToInt32(item.SubItems[0].Text);
            }
            else { return; }

            emp.DeleteEmployee(Id);
            MessageBox.Show("Employee record with ID: "+ Id +" has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            buttonListAll.PerformClick();

        }

        // clear input boxes
        private void buttonNewEmployee_Click(object sender, EventArgs e)
        {
            ClearAll();

        }

        private void ClearAll()
        {
            textBoxEmpId.Enabled = true;
            textBoxEmpId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxSalary.Clear();
            checkBoxIsCEO.Checked = false;
            checkBoxIsManager.Checked = false;
            textBoxManagerID.Enabled = true;
            textBoxManagerID.Clear();
        }

        // check if there is one CEO in database
        private void checkIfCEO(Employee emp)
        {

            if (checkBoxIsCEO.Checked)
            {
                if (emp.CheckIfCEO())
                {
                    MessageBox.Show("Only one CEO allowed.", InvalidData);
                    return;
                }
                else emp.IsCeo = true;
            }
        }

        private void CalculateSalary(Employee emp)
        {
            
            decimal baseSalary = 20000;
            decimal EmpS = 1.125m;
            decimal ManS = 1.725m;
            decimal CeoS = 2.725m;


            if (checkBoxIsManager.Checked) //Manager
            {
                emp.Salary = Decimal.Multiply(baseSalary, ManS); 
            }
            else if (checkBoxIsCEO.Checked) // CEO
            {
                emp.Salary = Decimal.Multiply(baseSalary, CeoS);
            }
            else if (checkBoxIsManager.Checked && checkBoxIsCEO.Checked) // CEO & Manager
            {
                emp.Salary = Decimal.Multiply(baseSalary, CeoS);
            }
            else  //Employee
            {
                emp.Salary = Decimal.Multiply(baseSalary, EmpS);
            }
        }

    }
}
