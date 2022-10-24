namespace Pishtova.Data.Common.Model
{
    using System.ComponentModel.DataAnnotations;

    public interface IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
