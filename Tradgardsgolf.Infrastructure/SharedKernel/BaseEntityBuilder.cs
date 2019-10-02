namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public abstract class BaseEntityBuilder<T> where T : class
    {
        protected readonly T _entity;

        protected BaseEntityBuilder(T entity)
        {
            _entity = entity;
        }

        internal virtual T Build()
        {
            return _entity;
        }
    }
}
