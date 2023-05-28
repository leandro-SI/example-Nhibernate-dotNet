using ExNhibernate.API.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExNhibernate.API.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository<Funcionario>
    {
        private readonly ISession _session;

        public FuncionarioRepository(ISession session)
        {
            _session = session;
        }

        public async Task Add(Funcionario item)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception _error)
            {
                Console.WriteLine(_error);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public IEnumerable<Funcionario> FindAll()
        {
            return _session.Query<Funcionario>().ToList();
        }

        public Task<Funcionario> FindById(long id)
        {
            return _session.GetAsync<Funcionario>(id);
        }

        public async Task Remove(long id)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Funcionario>(id);
                await _session.DeleteAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception _error)
            {
                Console.WriteLine(_error);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Funcionario item)
        {
            ITransaction transaction = null;

            try
            {
                ITransaction transaction1 = _session.BeginTransaction();
                await _session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception _error)
            {
                Console.WriteLine(_error);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}
