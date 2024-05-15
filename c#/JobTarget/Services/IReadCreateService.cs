namespace JobTargetCodingChallange.Services
{
    public interface IReadCreateService<TEntity>
    {
        Task<TEntity?> GetById(long id);

        Task<TEntity> Create(TEntity entity);

    }
}
