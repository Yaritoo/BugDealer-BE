﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using System.Threading;
using Bug.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Bug.Core.Utils;

namespace Bug.Data.Repositories
{
    public class StatusRepo : EntityRepoBase<Status>, IStatusRepo
    {
        public StatusRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IReadOnlyList<Status>> GetDefaultStatusesNoTrackAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusByCreatorIdSpecification(creatorId);
            return await GetNextByOffsetNoTrackBySpecAsync
                (0, 10, null, specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Status>> GetDefaultStatusesAsync
            (string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusByCreatorIdSpecification(creatorId);
            return await GetNextByOffsetBySpecAsync
                (0, 10, null, specificationResult, cancellationToken);
        }

        public override IQueryable<Status> SortOrder
            (IQueryable<Status> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
                    break;
                case "startdate":
                    //result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    //result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                    //result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    //result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    //result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    //result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }
    }
}
