﻿using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class User
    {
        [Key]
        public int RECNUM { get; set; }
        public string IDCARD { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public DateTime DATEOFBIRTH { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? COUNTRY { get; set; }
        public string? LOCALITY { get; set; }
    }
}
