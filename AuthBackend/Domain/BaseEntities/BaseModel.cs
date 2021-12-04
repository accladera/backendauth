using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class BaseModelName
    {
        [JsonIgnore]
        [Column("sName")]
        public string Name { get; set; }


        [JsonIgnore]
        [Column("dCompDate")]
        public DateTime Computed { get; set; }
    }
}
