using Core.AggreateRoot;
using Core.Rules;

namespace Core.Repository
{
    /// <summary>
    /// Interfaz base para las interfaces de repositorios de agregados. Trabaja sobre el root del agregado
    /// </summary>
    /// <typeparam name="TEntity">Entidad Root del agregado</typeparam>
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        /// <summary>
        /// Objeto que implementa el patron de unidad de trabajo
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Añade una entidad a la unidad de trajajo
        /// </summary>
        /// <param name="entity">Entidad a añadir</param>
        /// <returns>Retorna una referencia a la entidad ya añadida</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Actualiza una entidad a la unidad de trajajo
        /// </summary>
        /// <param name="entity">Entidad a añadir</param>
        /// <returns>Retorna una referencia a la entidad ya añadida</returns>
        TEntity Update(TEntity entity);
    }
}
