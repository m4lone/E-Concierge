using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;

namespace qodeless.DataManager.Seeds
{
    public class TokenSeeder
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region TOKENSEEDER
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var Tokens = new List<Token>() {
                new Token(Guid.NewGuid()){Code = "124356", DtExpiration= DateTime.Now, Phone= "5563999478684"},
                new Token(Guid.NewGuid()){Code = "124357", DtExpiration= DateTime.Now, Phone= "5563999367111"}
            };
            int count = 0;
            var tokenRepository = new TokenRepository(_dbContext);
            foreach (var token in Tokens)
            {
                count++;
                tokenRepository.Upsert(token, _ => _.Id == token.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //TOKENSEEDER

        }
    }
}