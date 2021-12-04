using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Domain.Entities;
using Core;
using Core.AggreateRoot;

namespace Domain.Entities
{
    public class Contact : BaseModel, IAggregateRoot
    {

        /// <summary>
        /// Identificador del registro.
        /// </summary>

        public override int Id { get; protected set; }

   
        [ForeignKey("Follower")]
        public int FollowerId { get; set; }
        public virtual User Follower { get; set; }

  
        [ForeignKey("Following")]
        public int FollowingId { get; set; }
        public virtual User Following { get; set; }


        public bool Accept { get; set; }


    }
}
