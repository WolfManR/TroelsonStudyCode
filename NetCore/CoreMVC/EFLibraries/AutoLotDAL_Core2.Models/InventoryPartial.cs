using AutoLotDAL_Core2.Models.MetaData;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL_Core2.Models
{
    [ModelMetadataType(typeof(InventoryMetaData))]
    public partial class Inventory
    {
        public override string ToString()
        {
            // Поскольку столбец PetName может быть пустым, определить стандартное имя **No Name**
            return $"{this.PetName ?? "**No Name**"} is a {this.Color} {this.Make} with ID {this.Id}.";
        }
        [NotMapped]
        public string MakeColor => $"{Make} + ({Color})";
    }
}
