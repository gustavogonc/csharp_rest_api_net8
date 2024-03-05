using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Repository;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disabled(long id);
    }
}
