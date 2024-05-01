using System;
using BankingInformation.Credits;

namespace BankingInformation.BankingData
{
    internal class Borrower
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; }


        public Borrower(string id, string firstName, string lastName,
                            DateTime dateOfBirth, string passportNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PassportNumber = passportNumber;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Borrower borrower)
            {
                return false;
            }

            return (this.Id == borrower.Id) &&
                   (this.FirstName == borrower.FirstName) &&
                   (this.LastName == borrower.LastName) &&
                   (this.DateOfBirth == borrower.DateOfBirth) &&
                   (this.PassportNumber == borrower.PassportNumber);
        }

        public override int GetHashCode()
        {
            return (this.Id + this.FirstName + this.LastName + this.DateOfBirth.ToString() + this.PassportNumber).GetHashCode();
        }
    }
}
