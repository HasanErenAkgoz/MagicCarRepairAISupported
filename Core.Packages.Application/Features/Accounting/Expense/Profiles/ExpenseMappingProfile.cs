using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetById;
using ExpenseEntity = MagicCarRepairAISupported.Domain.Entities.Expense;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Profiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<ExpenseEntity, CreateExpenseResponse>();
            CreateMap<ExpenseEntity, UpdateExpenseResponse>();
            CreateMap<ExpenseEntity, GetAllExpensesResponse>();
            CreateMap<ExpenseEntity, GetExpenseByIdResponse>();
        }
    }
}

