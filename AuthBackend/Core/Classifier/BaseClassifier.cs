using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AggreateRoot;
using Core.Models;

namespace Core.Classifier
{
    /// <summary>
    /// Clase base para clasificadores
    /// </summary>
    public class BaseClassifier : BaseMappedModel, IBaseClassifier
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="id">Identificador del clasificador</param>
        /// <param name="name">Nombre del clasificador</param>
        /// <param name="computed">Fecha de cómputo de del registro</param>
        public BaseClassifier(int id, string name, DateTime computed)
        {
            Id = id;
            Name = name;
            Computed = computed;
        }

        /// <summary>
        /// Crea un nuevo clasificador del tipo dados
        /// </summary>
        /// <typeparam name="TClassiffier"></typeparam>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static TClassiffier Create<TClassiffier>(string name, List<TClassiffier> list, int? id) where TClassiffier : BaseClassifier, IAggregateRoot, new()
        {
            var returnValue = new TClassiffier();
            returnValue.Update(name, true, list, id);
            return returnValue;
        }

        /// <summary>
        /// Valida que no exista otro clasificador con el mismo nombre
        /// </summary>
        /// <typeparam name="TClassiffier"></typeparam>
        /// <param name="list"></param>
        internal void ValidateExistName<TClassiffier>(List<TClassiffier> list, int? id) where TClassiffier : BaseClassifier, IAggregateRoot
        {
            // TODO: Verificar que el ID no sea igual al ID de la instanca (this.id)
            if (id.GetValueOrDefault() > 0)
            {
                list = list.Where(r => r.Id != id).ToList();
            }
            if (list.Count > 0)
            {
                throw new InvalidOperationException("No se puede añadir dos veces el mismo nombre");
            }

        }

        /// <summary>
        /// Identificador del clasificador
        /// </summary>
        public override int Id { get; protected set; }

        /// <summary>
        /// Nombre del clasificador
        /// </summary>
        public string Name { get; protected set; }

      

        /// <summary>
        /// Actualiza los valores de un clasificador
        /// </summary>
        /// <typeparam name="TClassiffier">Tipo de clasificador</typeparam>
        /// <param name="name">Nombre a modificar</param>
        /// <param name="status">Estado a modificar</param>
        /// <param name="list">Listado que contiene los clasificadores del mismo tipo con el mismo nombre</param>
        public void Update<TClassiffier>(string name, bool status, List<TClassiffier> list, int? id) where TClassiffier : BaseClassifier, IAggregateRoot, new()
        {
            UpdateName(name, list, id);
        }

        /// <summary>
        /// Actualiza el nombre de un clasificador
        /// </summary>
        /// <typeparam name="TClassiffier">Tipo de clasificador</typeparam>
        /// <param name="name">Nombre a modificar</param>
        /// <param name="list">Listado que contiene los clasificadores del mismo tipo con el mismo nombre</param>
        public void UpdateName<TClassiffier>(string name, List<TClassiffier> list, int? id) where TClassiffier : BaseClassifier, IAggregateRoot, new()
        {
            if (name != Name)
            {
                Name = name;
                ValidateExistName(list, id);
            }
        }


    }
}
