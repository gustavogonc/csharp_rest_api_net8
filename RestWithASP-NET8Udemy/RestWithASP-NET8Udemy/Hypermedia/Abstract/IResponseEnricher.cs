﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASP_NET8Udemy.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
