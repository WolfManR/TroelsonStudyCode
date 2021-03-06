﻿using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL_EF.Models.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
