using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaxiEmployeeInsurance.Services;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using System.Reflection;

namespace MaxiEmployeeInsurance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            ViewEmployees.Visibility = Visibility.Visible;
            ViewBeneficiaries.Visibility = Visibility.Collapsed;
            _viewModel = mainViewModel;
            DataContext = mainViewModel;
           
        }

        private void btnLoadEmployees_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private async void GetEmployees()
        {
            _viewModel.Employees = await new EmployeeService().GetEmployeesAsync();
        }

        private void btnEditEmployee(object sender, RoutedEventArgs e)
        {
            var emp = ((FrameworkElement)sender).DataContext as Employee;
            if (emp != null)
            {
                _viewModel.EmployeeLastName = emp.LastName;
                _viewModel.EmployeeFirstName = emp.FirstName;
                _viewModel.EmployeeSsn = emp.Ssn;
                _viewModel.EmployeePhoneNumber = emp.PhoneNumber;
                _viewModel.EmployeeNumber = emp.EmployeeNumber;
                _viewModel.EmployeeCurp = emp.Curp;
                _viewModel.EmployeeDateOfBirth = emp.DateOfBirth;
                _viewModel.EmployeeId = emp.Id;
                _viewModel.EmployeeNationality = emp.Nationality;
                _viewModel.EmployeeIsDeleted = emp.IsDeleted;
            }
            btnCancelEdit.Visibility = Visibility.Visible;
        }

        private void btnDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Employee? emp = ((FrameworkElement)sender).DataContext as Employee;
            DeleteEmployee(emp.Id);
        }

        private async void DeleteEmployee(int Id)
        {
            var employees = await new EmployeeService().DeleteEmployeeAsync(Id);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employees.Message);
            
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
        }

        private void ClearData()
        {
            txtCurp.Text = string.Empty;
            txtEmployeeNumber.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtNationality.Text = string.Empty;
            txtNss.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtIsDeleted.Text = string.Empty;
            dpkDateOfBirth.SelectedDate = DateTime.Now.AddYears(-18); 
        }

       

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeIsValid())
            {
                Employee emp = new Employee
                {
                    Curp = txtCurp.Text,
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text),
                    FirstName = txtFirstName.Text,
                    DateOfBirth = dpkDateOfBirth.SelectedDate.Value,
                    LastName = txtLastName.Text,
                    Nationality = txtNationality.Text,
                    PhoneNumber = txtPhone.Text,
                    Ssn = txtNss.Text
                };
                if (btnCancelEdit.Visibility == Visibility.Visible)
                {
                    emp.Id = Convert.ToInt32(txtId.Text);
                    UpdateEmp(emp);
                }
                else
                {
                    AddEmp(emp);
                }
            }
        }

        private async void UpdateEmp(Employee emp)
        {
            var employeeUpdated = await new EmployeeService().UpdateEmployeeAsync(emp);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employeeUpdated.Message);
        }

        private async void AddEmp(Employee emp)
        {
            var employeeUpdated = await new EmployeeService().AddEmployeeAsync(emp);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employeeUpdated.Message);
        }

        private bool EmployeeIsValid()
        {
            bool valid = false;
            if (_viewModel.IsValid())
            {
                valid = true;
            }
            return valid;
        }

        private void btnAdminBeneficiariesByEmployee(object sender, RoutedEventArgs e)
        {
            Employee? emp = ((FrameworkElement)sender).DataContext as Employee;
            ViewEmployees.Visibility = Visibility.Collapsed;
            ViewBeneficiaries.Visibility = Visibility.Visible;
            GetBeneficiaries(emp.Id);
            txtEmployeeIdB.Text = emp.Id.ToString();
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            ViewEmployees.Visibility = Visibility.Visible;
            ViewBeneficiaries.Visibility = Visibility.Collapsed;
        }

        private async void GetBeneficiaries(int Id)
        {
            var beneficiaries = await new BeneficiaryService().GetBeneficiariesAsync(Id);
            dgBeneficiaries.DataContext = beneficiaries;
        }

        private void btnEditBeneficiary(object sender, RoutedEventArgs e)
        {
            Beneficiary? ben = ((FrameworkElement)sender).DataContext as Beneficiary;
            txtCurpB.Text = ben?.Curp;
            txtEmployeeIdB.Text = ben?.EmployeeId.ToString();
            txtFirstNameB.Text = ben?.FirstName;
            txtLastNameB.Text = ben?.LastName;
            txtIdB.Text = ben?.Id.ToString();
            //txtParticipationPercentage.Text = ben?.ParticipationPercentage.ToString();
            txtNationalityB.Text = ben?.Nationality;
            txtNssB.Text = ben?.Ssn;
            txtPhoneB.Text = ben?.PhoneNumber;
            txtIsDeletedB.Text = ben?.IsDeleted.ToString();
            dpkDateOfBirthB.SelectedDate = new DateTime(ben.DateOfBirth.Year, ben.DateOfBirth.Month, ben.DateOfBirth.Day);
            btnCancelEditB.Visibility = Visibility.Visible;
        }

        private void btnCancelEditB_Click(object sender, RoutedEventArgs e)
        {
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
        }

        private void ClearDataB()
        {
            txtCurpB.Text = string.Empty;
            txtFirstNameB.Text = string.Empty;
            txtLastNameB.Text = string.Empty;
            txtIdB.Text = string.Empty;
            txtNationalityB.Text = string.Empty;
            txtNssB.Text = string.Empty;
            txtPhoneB.Text = string.Empty;
            txtIsDeletedB.Text = string.Empty;
            //txtParticipationPercentage.Text = String.Empty;
            dpkDateOfBirthB.SelectedDate = null;
        }

        private void btnDeleteBeneficiary(object sender, RoutedEventArgs e)
        {
            Beneficiary? ben = ((FrameworkElement)sender).DataContext as Beneficiary;
            DeleteBeneficiary(ben.Id, ben.EmployeeId);
        }

        private async void DeleteBeneficiary(int Id, int EmployeeId)
        {
            var ben = await new BeneficiaryService().DeleteBeneficiaryAsync(Id);
            GetBeneficiaries(EmployeeId);
            ClearData();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(ben.Message);

        }

        private bool BeneficiaryIsValid()
        {
            bool valid = false;
            if (txtCurpB.Text != string.Empty &&
                txtFirstNameB.Text != string.Empty &&
                txtEmployeeIdB.Text != string.Empty &&
                txtLastNameB.Text != string.Empty &&
                txtNationalityB.Text != string.Empty &&
                txtNssB.Text != string.Empty &&
                txtPhoneB.Text != string.Empty &&
                dpkDateOfBirthB.SelectedDate != null 
                //txtParticipationPercentage.Text != null
                )
            {
                valid = true;
            }
            else
            {
                MessageBox.Show("Type the Beneficiary missing information");
            }
            return valid;
        }

        private void btnSaveBeneficiary_Click(object sender, RoutedEventArgs e)
        {
            if (BeneficiaryIsValid())
            {
                Beneficiary ben = new Beneficiary
                {
                    Curp = txtCurpB.Text,
                    EmployeeId = Convert.ToInt32(txtEmployeeIdB.Text),
                    FirstName = txtFirstNameB.Text,
                    DateOfBirth = dpkDateOfBirthB.SelectedDate.Value,
                    LastName = txtLastNameB.Text,
                    Nationality = txtNationalityB.Text,
                    PhoneNumber = txtPhoneB.Text,
                    Ssn = txtNssB.Text,
                   // ParticipationPercentage = Convert.ToInt32(txtParticipationPercentage.Text)
                };
                if (btnCancelEditB.Visibility == Visibility.Visible)
                {
                    ben.Id = Convert.ToInt32(txtIdB.Text);
                    UpdateBen(ben);
                }
                else
                {
                    AddBen(ben);
                }
            }
        }

        private async void UpdateBen(Beneficiary ben)
        {
            var benUpdated = await new BeneficiaryService().UpdateBeneficiaryAsync(ben);
            GetBeneficiaries(ben.EmployeeId);
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(benUpdated.Message);
        }

        private async void AddBen(Beneficiary ben)
        {
            var benUpdated = await new BeneficiaryService().AddBeneficiaryAsync(ben);
            GetBeneficiaries(ben.EmployeeId);
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(benUpdated.Message);
        }
    }
}
