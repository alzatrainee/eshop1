using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Address
    {
        public Address() { }
        public Address (string street, string city, string house_number, string post_code, int country)
        {
            this.street = street;
            this.city = city;
            this.house_number = house_number;
            this.post_code = post_code;
            this.country = country;
        }

        //public Address(string street, string city, int house_number, decimal post_code, int id_us)
        //{
        //    this.street = street;
        //    this.city = city;
        //    this.house_number = house_number;
        //    this.post_code = post_code;
        //    this.id_us = id_us;
        //}

       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ad { get; set; }

        
        public string street { get; set; }

        public string block { get; set; }

        public string city { get; set; }
        public string house_number { get; set; }
        public string post_code { get; set; }
        
        [ForeignKey("code")]
        public int country { get; set; }
        [NotMapped]
        public Country Country { get; set; }


        public Nullable<int> id_us { get; set; }

    }
}
