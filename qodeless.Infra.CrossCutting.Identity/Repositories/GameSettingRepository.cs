using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class GameSettingRepository : Repository<GameSetting>, IGameSettingRepository
    {
        public GameSettingRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
