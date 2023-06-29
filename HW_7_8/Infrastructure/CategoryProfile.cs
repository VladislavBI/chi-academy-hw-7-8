using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.ViewModels;

namespace HW_7_8.Infrastructure
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDataModel>();
            CreateMap<CategoryDataModel, Category>();
            CreateMap<CategoryDataModel, CategoryEditViewModel>();
            CreateMap<CategoryDataModel, CategoryAddViewModel>();
            CreateMap<CategoryEditViewModel, CategoryDataModel>();
            CreateMap<CategoryAddViewModel, CategoryDataModel > ();
        }
    }
}