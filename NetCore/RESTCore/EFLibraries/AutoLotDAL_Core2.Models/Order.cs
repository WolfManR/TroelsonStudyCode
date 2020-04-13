﻿using AutoLotDAL_Core2.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL_Core2.Models
{
    public class Order : EntityBase
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        [ForeignKey(nameof(CarId))]
        public Inventory Car { get; set; }
    }
}
