using System;
using BankingInformation.Credits;

namespace BankingInformation.BankingData
{
    internal class Bank
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Bank(string id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Bank bank)
            {
                return false;
            }

            return this.Id == bank.Id && this.Name == bank.Name && this.Address == bank.Address;
        }

        public override int GetHashCode()
        {
            return (this.Id + this.Name + this.Address).GetHashCode();
        }
    }
}
