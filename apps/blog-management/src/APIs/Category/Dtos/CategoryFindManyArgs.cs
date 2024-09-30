using BlogManagement.APIs.Common;
using BlogManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CategoryFindManyArgs : FindManyInput<Category, CategoryWhereInput> { }
