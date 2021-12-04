using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Domain.Entities;
using Core.Rules.PersonName;
using Core.AggreateRoot;

namespace Domain.Entities
{
    public class Role : BaseModel, IAggregateRoot
    {
      
        /// <summary>
        /// Identificador del registro.
        /// </summary>

        public override int Id { get; protected set; }

        public string TypeRole { get; set; }

        public string Descripcion { get; private set; }

        public Role(int id, string typeRole, string descripcion)
        {
            Id = id;
            TypeRole = typeRole;
            Descripcion = descripcion;

        }

        public Role(string descripcion, string typeRole)
        {
            Descripcion = descripcion;
            TypeRole = typeRole;
        }
    }
}
