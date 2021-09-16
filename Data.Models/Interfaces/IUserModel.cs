namespace Data.Models.Interfaces
{
    public interface IUserModel<TKey>
    {
        public TKey UserId { get; set; }
        public string Email { get; set; }
    }
}
