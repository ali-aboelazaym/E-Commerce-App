﻿namespace Ecom.Core.Entities
{
    public class Adress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser Appuser { get; set; }
    }
}