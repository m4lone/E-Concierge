using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class ExpenseRequestSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region EXPENSEREQUESTSEEDER
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var expenseRequestSeederId1 = Guid.Parse("9733b285-c229-4dbe-8126-b1ce8ab18164");
            var expenseRequestSeederId2 = Guid.Parse("e97c4bcd-838e-47d6-9fce-545955b526b3");
            int count = 0;
            var expenses = new ExpenseRepository(_dbContext).GetAll().ToList();
            var users = _dbContext.Users.ToList();
            var expenseRequests = new List<ExpenseRequest>() {
                new ExpenseRequest(expenseRequestSeederId1){ ExpenseId = GetFKRandom(expenses), UserOperationID=GetFKRandomUser(users), DueDate= DateTime.Now, Message= "First Expense", Request= EExpenseRequest.Denied},
                new ExpenseRequest(expenseRequestSeederId2){ ExpenseId = GetFKRandom(expenses), UserOperationID=GetFKRandomUser(users), DueDate= DateTime.Now, Message= "Second Expense", Request= EExpenseRequest.Approved}
            };

            var expenseRequestRepository = new ExpenseRequestRepository(_dbContext);
            foreach (var expenseRequest in expenseRequests)
            {
                count++;
                expenseRequestRepository.Upsert(expenseRequest, _ => _.Id == expenseRequest.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //EXPENSEREQUESTSEEDER
        }
    }
}