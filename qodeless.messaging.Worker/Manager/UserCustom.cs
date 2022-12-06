using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qodeless.messaging.Worker.Manager
{
    public class UserStoreCustom : UserStore<ApplicationUser>
    {
        public UserStoreCustom(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {

        }
        public virtual Task<ApplicationUser> FindByPhoneNumberAsync(string PhoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber, cancellationToken);
        }
    }
}
