using System;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace BankApp
{
    public class Customer
    {
        private int? id;
        private string? firstname;
        private string? lastname;
        private string? username;
        private string? password;
        private string? country;
        private string? region;
        private string? city;
        private string? address;
        private DateTime? lastupdate;
        private ObservableCollection<BankAccount> bankaccount;
        public event PropertyChangedEventHandler? PropertyChanged;



        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Customer()
        {
            this.id = null;
            this.firstname = null;
            this.lastname = null;
            this.username = null;
            this.password = null;
            this.country = null;
            this.region = null;
            this.city = null;
            this.address = null;
            this.lastupdate = null;
            this.bankaccount = new();

        }

        public int? ID
        {
            get { return id; }
            set
            {
                this.id = value;
            }
        }

        public string? FirstName
        {
            get { return firstname; }
            set
            {
                this.firstname = value;
            }
        }
        public string? LastName
        {
            get { return lastname; }
            set
            {
                this.lastname = value;
            }
        }
        public string? Username
        {
            get { return username; }
            set
            {
                this.username = value;
            }
        }
        public string? Password
        {
            get { return password; }
            set
            {
                this.password = value;
            }
        }
        public string? Country
        {
            get { return country; }
            set
            {
                this.country = value;
            }
        }
        public string? Region
        {
            get { return region; }
            set
            {
                this.region = value;
            }
        }
        public string? City
        {
            get { return city; }
            set
            {
                this.city = value;
            }
        }
        public string? Address
        {
            get { return address; }
            set
            {
                this.address = value;
            }
        }
        public DateTime? LastUpdate
        {
            get { return lastupdate; }
            set
            {
                this.lastupdate = value;
            }
        }
        public ObservableCollection<BankAccount> BankAccount
        {
            get { return bankaccount; }
            set
            {
                this.bankaccount = value;
            }
        }
    }
}