﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL_EF.Models.Models
{
    public partial class Inventory:EntityBase
    {
        [NotMapped]
        public string MakeColor => $"{Make} + ({Color})";
        public override string ToString()
        {
            // Since the PetName column could be empty, supply the default name of **No Name**.
            return $"{this.PetName ?? "**No Name**"} is a {this.Color} {this.Make} with ID {this.Id}.";
        }
    }
}
