using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.ViewModels;

namespace HW_7_8.Infrastructure
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpensesEnumerableModel, ExpensesEnumerableViewModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Expenses.Sum(e => e.Cost))); ;
            CreateMap<ExpensesEnumerableViewModel, ExpensesEnumerableModel>();
            CreateMap<Expense, ExpenseDataModel>();
            CreateMap<ExpenseDataModel, Expense> ();
            CreateMap<ExpenseAddModel, ExpenseAddViewModel>();
            CreateMap<ExpenseAddViewModel, ExpenseAddModel>();
            CreateMap<ExpenseAddModel, ExpenseEditViewModel>();
            CreateMap<ExpenseEditViewModel, ExpenseAddModel>();
        }
    }
}