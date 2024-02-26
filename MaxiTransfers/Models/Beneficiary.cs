using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public decimal ParticipationPercentage { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
