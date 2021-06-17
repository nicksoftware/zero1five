namespace Zero1Five.Products.Events
{
    public interface IEventData<TEntity>
    {
        public TEntity Entity { get; set; }
    }
}