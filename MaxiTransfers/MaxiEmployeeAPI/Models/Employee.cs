using System;
using System.ComponentModel.DataAnnotations;

namespace MaxiEmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int EmployeeNumber { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
