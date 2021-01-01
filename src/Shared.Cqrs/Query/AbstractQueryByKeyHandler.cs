using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Cqrs
{
    public abstract class AbstractQueryByKeyHandler<TEntity, TValidator> where TEntity : new() where TValidator : AbstractValidator<Guid>, new()
    {
        private readonly TValidator _validator = new TValidator();
        private readonly List<KeyValuePair<string, string>> _errors;

        public AbstractQueryByKeyHandler() { }

        public async Task<QueryResponse<TEntity>> Handle(GenericQueryByKey request)
        {
            var result = new QueryResponse<TEntity>() { Errors = GetRequestErrors(request) };

            if (result.Errors.Count == 0)
            {
                try
                {
                    result.Result = await ExecuteQueryAsync(request);
                }
                catch (Exception e)
                {
                    result.ThrownException = e;
                }
            }
            return result;
        }

        protected abstract Task<TEntity> ExecuteQueryAsync(GenericQueryByKey request);

        private List<KeyValuePair<string, string>> GetRequestErrors(GenericQueryByKey request)
        {
            var issues = _validator.Validate(request.Key).Errors;

            foreach (var issue in issues)
                _errors.Add(new KeyValuePair<string, string>(issue.PropertyName, issue.ErrorMessage));
            return _errors;
        }

    }
}
