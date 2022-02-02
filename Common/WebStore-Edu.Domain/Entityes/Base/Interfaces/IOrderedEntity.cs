namespace WebStore_Edu.Domain.Entityes.Base.Interfaces
{
    public interface IOrderedEntity : IEntity
    {
        int Order { get; set; }
    }
}