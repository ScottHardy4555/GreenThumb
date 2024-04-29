using GreenThumb.Models.DomainModels;
namespace GreenThumb.Data.Services
{
    public interface IContactService
    {
        List<Contact> GetAll();
        Task<Contact> GetById(int id);
        Task Add(Contact contact);
        Task Update(Contact contact);
        Task Delete(Contact contact);
        Task SaveChanges();
    }
}
