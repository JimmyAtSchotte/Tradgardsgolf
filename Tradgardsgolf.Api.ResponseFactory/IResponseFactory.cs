namespace Tradgardsgolf.Api.ResponseFactory;

public interface IResponseFactory<out TResponse, in TEntity>
{
    TResponse Create(TEntity entity);
}