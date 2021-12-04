using Core.AggreateRoot;
using Core.Models;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository

{
    public abstract class BaseRepository<TEntity>: Core.Repository.BaseRepository<TEntity> where TEntity : BaseNotMappedModel, IAggregateRoot
    {
        protected readonly DataBaseContext _context;

        protected BaseRepository(DataBaseContext context)
        {
            _context = context;
        }

        protected override DbSet<TEntity> DataSet { get => _context.Set<TEntity>(); }

    }
}
