using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Application.Persistence;

public interface IUnitOfWork: IDisposable
{
    IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> Complete();
}