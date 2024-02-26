using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiEmployeeInsurance
{
    public class MainViewModel : NotifyPropertyBase, INotifyDataErrorInfo
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Today;
            int Age = currentDate.Year - dateOfBirth.Year;
            if (dateOfBirth.Month > currentDate.Month || (dateOfBirth.Month == currentDate.Month && dateOfBirth.Day > currentDate.Day))
            {
                --Age;
            }
            return Age;
        }
        public MainViewModel()
        {
            EmployeeDateOfBirth = DateTime.Now.AddYears(-18);
            Validate.Add(nameof(EmployeeFirstName), () =>
            {
                if (string.IsNullOrEmpty(EmployeeFirstName))
                    return "Required Field";
                return null;
            });
            Validate.Add(nameof(EmployeeLastName), () =>
            {
                if (string.IsNullOrEmpty(EmployeeLastName))
                    return "Required Field";
                return null;
            });
            Validate.Add(nameof(EmployeeDateOfBirth), () =>
            {
                if (CalculateAge(EmployeeDateOfBirth) < 18)
                    return "The Employee must have more than 17 years old";
                return null;
            });
            Validate.Add(nameof(EmployeeNumber), () =>
            {
                if (string.IsNullOrEmpty(EmployeeNumber.ToString()))
                    return "Required Field";
                return null;
            });
            Validate.Add(nameof(EmployeeSsn), () =>
            {
                if (string.IsNullOrEmpty(EmployeeSsn))
                    return "Required Field";
                return null;
            });
            Validate.Add(nameof(EmployeeCurp), () =>
            {
                if (string.IsNullOrEmpty(EmployeeCurp))
                    return "Required Field";
                return null;
            });
            Validate.Add(nameof(EmployeePhoneNumber), () =>
            {
                if (string.IsNullOrEmpty(EmployeePhoneNumber))
                    return "Required Field";
                if (EmployeePhoneNumber.Length < 10)
                    return "The Phone must be have 10 digits";
                return null;
            });
            Validate.Add(nameof(EmployeeNationality), () =>
            {
                if (string.IsNullOrEmpty(EmployeeNationality))
                    return "Required Field";
                return null;
            });

        }

        private List<Employee>? _Employees;
        private int employeeId;
        private string employeeFirstName;
        private string employeeLastName;

        public List<Employee>? Employees
        {
            get => _Employees;
            set
            {
                _Employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public int EmployeeId { get => employeeId; set { employeeId = value; OnPropertyChanged(nameof(EmployeeId)); } }
        public string EmployeeFirstName { get => employeeFirstName; set { employeeFirstName = value; OnPropertyChanged(nameof(EmployeeFirstName)); } }
        public string EmployeeLastName { get => employeeLastName; set { employeeLastName = value; OnPropertyChanged(nameof(EmployeeLastName)); } }
        public DateTime EmployeeDateOfBirth { get => employeeDateOfBirth; set { employeeDateOfBirth = value; OnPropertyChanged(nameof(EmployeeDateOfBirth)); } }
        public int EmployeeNumber { get => employeeNumber; set { employeeNumber = value; OnPropertyChanged(nameof(EmployeeNumber)); } }
        public string EmployeeCurp { get => employeeCurp; set { employeeCurp = value; OnPropertyChanged(nameof(EmployeeCurp)); } }
        public string EmployeeSsn { get => employeeSsn; set { employeeSsn = value; OnPropertyChanged(nameof(EmployeeSsn)); } }
        public string EmployeePhoneNumber { get => employeePhoneNumber; set { employeePhoneNumber = value; OnPropertyChanged(nameof(EmployeePhoneNumber)); } }
        public string EmployeeNationality { get => employeeNationality; set { employeeNationality = value; OnPropertyChanged(nameof(EmployeeNationality)); } }
        public bool EmployeeIsDeleted { get; set; }


        public string Error => string.Empty;

        private Dictionary<string, Func<string>> _Validate = new Dictionary<string, Func<string>>();
        public Dictionary<string, Func<string>> Validate { get => _Validate; set => _Validate = value; }
        protected bool _HasErrors = false;
        private DateTime employeeDateOfBirth;
        private int employeeNumber;
        private string employeeCurp;
        private string employeeSsn;
        private string employeePhoneNumber;
        private string employeeNationality;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        bool INotifyDataErrorInfo.HasErrors => _HasErrors;

        public IEnumerable GetErrors(string? propertyName)
        {
            return GetMessageError(propertyName);
        }

        public virtual List<string> GetMessageError(string propertyName)
        {
            if (_Validate.Any(p => p.Key == propertyName))
            {
                var result = _Validate[propertyName]?.Invoke();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return new List<String> { result };
                }
            }
            else
                return null;
        }


        public bool IsValid()
        {
            _HasErrors = true;
            var result = true;
            bool finallyResponse = true;

            foreach (var valid in _Validate)
            {
                try
                {
                    result = valid.Value.Invoke() == null;
                }
                catch (Exception ex)
                {
                    result = true;
                }
                if (result)
                {
                    _HasErrors = false;
                }
                else
                {
                    _HasErrors = true;
                    finallyResponse = false;
                }
                this.OnPropertyChanged(valid.Key);
            }
            if (!finallyResponse)
                _HasErrors = true;
            return finallyResponse;
        }
    }
}
