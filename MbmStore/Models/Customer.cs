using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MbmStore.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        //new property:
    public bool isNewCustomer { get; set; }





        public DateTime Birthdate { get; set; }



        // read only property
        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;
                int age = now.Year - Birthdate.Year;
                if (now.Month < Birthdate.Month ||
                    (now.Month == Birthdate.Month && now.Day < Birthdate.Day))
                {
                    age--;
                }
                return age;
            }
        }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Phone> PhoneNumbers { get; set; } = new List<Phone>();

        // constructor
        public Customer() { }

        public Customer(int customerId, string firstnavn, string lastnavn, string address, string zip, string city)
        {
            CustomerId = customerId;
            Firstname = firstnavn;
            Lastname = lastnavn;
            Address = address;
            Zip = zip;
            City = city;
        }

        // method
        public void AddPhone(Phone phone)
        {
            PhoneNumbers.Add(phone);
        }

    }
}
