using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AggreateRoot;
using Core.Models;
using Microsoft.EntityFrameworkCore;


namespace Core.Repository

{
    /// <summary>
    /// Clase base para los repositorios de comandos
    /// </summary>
    /// <typeparam name="TEntity">Entidad Root del agregado</typeparam>
    public abstract class BaseRepository<TEntity> 
        where TEntity : BaseNotMappedModel, IAggregateRoot
    {


        /// <summary>
        /// DataSet de la entidad
        /// </summary>
        protected abstract DbSet<TEntity> DataSet { get; }

        /// <summary>
        /// Añade una entidad a la unidad de trabajo. Se usa para entidades nuevas que no están siendo mapeadas
        /// </summary>
        /// <param name="item">Item a añadir</param>
        /// <returns>Retorna la entidad añadida</returns>
        protected TEntity AddAux(TEntity item)
        {
            if (item.IsTransient())
            {
                return DataSet
                    .Add(item)
                    .Entity;
            }
            else
            {
                return item;
            }
        }

        /// <summary>
        /// Actualiza una entidad a la unidad de trabajo. Se usa para entidades que ya están siendo mapeadas
        /// </summary>
        /// <param name="item">Item a actualizar</param>
        /// <returns>Retorna la entidad añadida</returns>
        protected TEntity UpdateAux(TEntity item)
        {
            return DataSet
                    .Update(item)
                    .Entity;
        }

        /// <summary>
        /// Elimina una entidad a la unidad de trabajo. Se usa para entidades que ya están siendo mapeadas
        /// </summary>
        /// <param name="item">Item a actualizar</param>
        /// <returns>Retorna la entidad añadida</returns>
        protected TEntity DeleteAux(TEntity item)
        {
            return DataSet
                    .Remove(item)
                    .Entity;
        }
    }
}
