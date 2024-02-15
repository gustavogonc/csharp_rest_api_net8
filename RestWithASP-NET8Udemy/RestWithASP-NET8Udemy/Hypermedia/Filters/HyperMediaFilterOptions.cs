using RestWithASP_NET8Udemy.Hypermedia.Abstract;

namespace RestWithASP_NET8Udemy.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
