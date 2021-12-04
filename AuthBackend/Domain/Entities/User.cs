using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Domain.Entities;
using Core.AggreateRoot;
using System.Text;
using System.Security.Cryptography;
using Core.Rules.PersonName;
using Core.Rules.Email;

namespace Domain.Entities
{
    public class User : BaseModel, IAggregateRoot
    {
        /// <summary>
        /// Identificador del registro.
        /// </summary>

        public override int Id { get; protected set; }


        public string UserName { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

 
        public bool ResetPassword { get; set; }

        public DateTime BirthDate { get; set; }

        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Followers { get; set; }
        [JsonIgnore]
        public ICollection<Contact> Followings { get; set; }


        // Reglas de Negocio
      
        /// <summary>
        /// Realiza la verificacion de password del usuario instanciado
        /// </summary>
        /// <param name="password">Password a verificar</param>
        /// <returns>bool</returns>
        public bool authenticate(string password)
        {
            var buffer = new UnicodeEncoding().GetBytes(password);
            var hash = new SHA256Managed().ComputeHash(buffer);
            var tmp = Convert.ToBase64String(hash);

            return tmp == Hash;
        }


        /// <summary>
        /// Realiza la creacion del usuario Cliente
        /// </summary>
        /// <returns>bool</returns>
        public void CreateUserClient(string name, string password,string email, DateTime birthdate)
        {
            UserName = new PersonNameValue(name);
            Email =  new EmailValue(email);
            RoleId = 2;
            ResetPassword = false;
            BirthDate = birthdate;
            CreateAndUpdatePassword(password);
        }


        /// <summary>
        /// Realiza la creacion del usuario Gerente
        /// </summary>
        /// <returns>bool</returns>
        public void CreateUserGerent(string name, string password, string email)
        {
            UserName = new PersonNameValue(name);
            Email = new EmailValue(email);
            RoleId = 1;
            ResetPassword = false;
            CreateAndUpdatePassword(password);
        }

        /// <summary>
        /// Realiza la creacion del Hash mediante el password
        /// </summary>
        /// <returns>bool</returns>
        public void CreateAndUpdatePassword(string password)
        {
            Hash = EncryptBase64(password);
        }

        /// <summary>
        /// Encripta el password
        /// </summary>
        /// <returns>bool</returns>
        private string EncryptBase64(string password)
        {
            string result = "";

            if (!string.IsNullOrEmpty(password))
            {
                var buffer = new UnicodeEncoding().GetBytes(password);
                var hash = new SHA256Managed().ComputeHash(buffer);
                result = Convert.ToBase64String(hash);
            }
            return result;
        }


    }
}
