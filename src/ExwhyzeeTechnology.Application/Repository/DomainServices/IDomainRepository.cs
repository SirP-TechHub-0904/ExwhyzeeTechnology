using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Application.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Application.Repository.DomainServices
{
    public interface IDomainRepository
    {
        Task<List<DomainListDto>> GetAllDomains();
        Task<bool> AddDomain(DomainDataDto data);
        Task<bool> UpdateDomain(DomainDataDto data);
        Task<DomainDataDto> GetDomain(long id);
        Task<bool> DeleteDomain(long id);
        
    }
}
