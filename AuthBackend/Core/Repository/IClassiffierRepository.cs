using Core.AggreateRoot;
using Core.Classifier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repository
{
    /// <summary>
    /// Interfaz que representa un repositorio para los clasificadores genéricos
    /// </summary>
    /// <typeparam name="TClassiffier"></typeparam>
    public interface IClassiffierRepository<TClassiffier> : IRepository<TClassiffier>
            where TClassiffier : BaseClassifier, IAggregateRoot

    {
        /// <summary>
        /// Busca un clasificador basado en el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TClassiffier> FindByIdAsync(int id);

        /// <summary>
        /// Busca un clasificador con el mismo nombre que el parámetro dado
        /// </summary>
        /// <param name="description">Nombre del clasificador a buscar</param>
        /// <returns></returns>
        IEnumerable<TClassiffier> FindByNameAsync(string description);
    }
}