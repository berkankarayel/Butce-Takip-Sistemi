using AutoMapper;
using BudgetTracking.Application.DTOs.Expenses;
using BudgetTracking.Application.DTOs.Incomes;
using BudgetTracking.Domain.Entities;

namespace BudgetTracking.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Expense → ExpenseListItemDto
            CreateMap<Expense, ExpenseListItemDto>()
                .ForMember(dest => dest.CategoryName, 
                           opt => opt.MapFrom(src => src.Category.Name));

            // Income → IncomeListItemDto
            CreateMap<Income, IncomeListItemDto>()
                .ForMember(dest => dest.CategoryName, 
                           opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
