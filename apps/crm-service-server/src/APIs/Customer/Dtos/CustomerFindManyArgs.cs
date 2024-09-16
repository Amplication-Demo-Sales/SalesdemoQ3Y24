using CrmService.APIs.Common;
using CrmService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
