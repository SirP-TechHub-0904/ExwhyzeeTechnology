using ExwhyzeeTechnology.Application.Dtos.Project;
using ExwhyzeeTechnology.Application.Repository.Base;
using ExwhyzeeTechnology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Application.Repository.GeneralRepository.ProjectsCategory
{
    public interface IProjectCategoryRepositoryAsync : IGenericRepositoryAsync<ProjectCategory>
    {
        Task<IReadOnlyList<ProjectCategoryListDto>> GetAllProjectCategoryWithProjectCountAsync();
    }
}
