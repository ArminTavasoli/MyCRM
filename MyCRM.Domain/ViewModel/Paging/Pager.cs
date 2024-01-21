namespace MyCRM.Domain.ViewModel.Paging
{
    public class Pager
    {
        public static BasePaging BuildPager(int pageId, int howManyShowPageAfterAndBefore, int pageAllEntitiesCount, int take)
        {
            var PageCount = Convert.ToInt32(Math.Ceiling(pageAllEntitiesCount / (double)take));

            return new BasePaging()
            {
                PageId = pageId,
                AllEntitiesCount = pageAllEntitiesCount,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                StartPage = pageId - howManyShowPageAfterAndBefore <= 0 ? 1 : pageId - howManyShowPageAfterAndBefore,
                EndPage = pageId + howManyShowPageAfterAndBefore > PageCount ? PageCount : pageId + howManyShowPageAfterAndBefore,
                PageCount = PageCount
            };
        }
    }
}
