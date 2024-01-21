using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Domain.ViewModel.Paging
{
    public static class PagingExtension
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query , BasePaging Paging)
        {
            return query.Skip(Paging.SkipEntity).Take(Paging.TakeEntity);
        }
    }
}
