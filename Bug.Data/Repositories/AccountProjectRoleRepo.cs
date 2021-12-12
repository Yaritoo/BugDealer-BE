﻿using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Threading;
//using System.Data.SqlClient;

namespace Bug.Data.Repositories
{
    public class AccountProjectRoleRepo : EntityRepoBase<AccountProjectRole>, IAccountProjectRoleRepo
    {
        public AccountProjectRoleRepo(BugContext bugContext)
            : base(bugContext)
        {

        }

        public void UpdateMultiByRoleIdProjectId(string projectId, List<Role> roles)
        {
            if (roles == null)
                return;
            var listRoleId = roles
                .Select(r => r.Id.ToString())
                .ToList()
                .Aggregate((x, y) => x + "," + y);
            var list = new SqlParameter("list", listRoleId);
            var pro = new SqlParameter("pro", projectId);
            var role = new SqlParameter("role", 1);
            _bugContext
                .Database
                .ExecuteSqlRaw("EXECUTE dbo.UpdateAccountsHaveDumbRole @list, @pro, @role", list,pro,role);
        }

        public void UpdateAprAfterDeleteRole(int roleId)
        {
            var role = new SqlParameter("role", roleId);
            _bugContext
                .Database
                .ExecuteSqlRaw("EXECUTE dbo.UpdateAprAfterDeleteRole @role", role);
        }

        public async Task DeleteMemberFromProjectAsync
            (string projectId, 
            string accountId,
            CancellationToken cancellationToken = default)
        {
            var project = new SqlParameter("project", projectId);
            var user = new SqlParameter("user", accountId);
            await _bugContext
                .Database
                .ExecuteSqlRawAsync("EXECUTE dbo.DeleteMemberFromProject @project, @user", project, user, cancellationToken);
        }

        public override IQueryable<AccountProjectRole> SortOrder(IQueryable<AccountProjectRole> result, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
