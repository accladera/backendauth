using MediatR;
//using Service.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Utils
{
    public abstract class TransactionHandler<T, U> : IRequestHandler<T, U> where T: IRequest<U>
    {
        protected readonly List<string> _errors;

        public TransactionHandler()
        {
            _errors = new List<string>();
        }

        public async Task<U> Handle(T request, CancellationToken cancellationToken)
        {
            await Validate(request);
            if (_errors.Count > 0)
            {
                return (U)Activator.CreateInstance(typeof(U), null, _errors);
            }
            return await Process(request);
        }

        protected abstract Task Validate(T request);

        protected abstract Task<U> Process(T request);
    }
}
