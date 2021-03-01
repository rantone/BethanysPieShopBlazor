using System.Collections.Generic;
using BethanysPieShop.Shared;

namespace BethanysPieShop.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
