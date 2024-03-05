using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PersonVO Disabled(long id);
        void Delete(long id);
    }
}
