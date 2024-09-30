using EcommerceManagement.APIs.Common;
using EcommerceManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ArticleFindManyArgs : FindManyInput<Article, ArticleWhereInput> { }
