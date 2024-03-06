using RestWithASP_NET8Udemy.Hypermedia.Utils;
using RestWithASP_NET8Udemy.Repository;
using RestWithASPNETErudio.Data.Converter.Implementations;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        // Method responsible for returning all people,
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var offSet = page > 0 ? (page - 1) * pageSize : 0;
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;

            string query = @"SELECT * FROM PERSON P WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $"AND p.name like '%{name}%'";

            }
            query += $"ORDER BY P.FIRSTNAME {sort} LIMIT {size} OFFSET {offSet}";
                            

            var persons = _repository.FindWithPagedSearch(query);
            string countQuery = "SELECT count(*) FROM PERSON P WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery += $"AND p.name like '%{name}%'";

            }
            int totalResults = _repository.GetCount(countQuery);
            return new PagedSearchVO<PersonVO> { 
            CurrentPage = offSet,
            List = _converter.Parse(persons),
            PageSize = size, 
            SortDirections = sort,
            TotalResults = totalResults
            };
        }

        // Method responsible for returning one person by ID
        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // Method responsible for returning one person by first name or last name
        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        // Method responsible to crete one new person
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for updating one person
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for disable a person form an ID
        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disabled(id);

            return _converter.Parse(personEntity);
        }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }


    }
}
