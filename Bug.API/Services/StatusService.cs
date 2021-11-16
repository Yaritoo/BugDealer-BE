﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.Dto;

namespace Bug.API.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Status> GetDetailStatusByIdAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            StatusSpecification specificationResult =
                new(id);
            return await _unitOfWork
                .Status
                .GetEntityAsync(specificationResult, cancellationToken);
        }

        public async Task<PaginatedListDto<Status>> GetPaginatedDetailByCreatorIdAsync
            (string creatorId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            StatusSpecification specificationResult =
                new(creatorId);
            var result = await _unitOfWork
                .Status
                .GetPaginatedNoTrackAsync(pageIndex, pageSize, sortOrder, specificationResult,cancellationToken);
            return new PaginatedListDto<Status>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Status>> GetNextByOffsetDetailByCreatorIdAsync
            (string creatorId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            StatusSpecification specificationResult =
                new(creatorId);
            var result = await _unitOfWork
                .Status
                .GetNextByOffsetNoTrackAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Status>> GetStatusesByCreatorIdAsync
            (string creatorId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusByCreatorIdSpecification(creatorId);
            return await _unitOfWork
                .Status
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Status>> GetStatusesByProjectIdAsync
            (string projectId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new StatusByProjectIdSpecification(projectId);
            return await _unitOfWork
                .Status
                .GetAllEntitiesAsync(specificationResult, cancellationToken);
        }

        public async Task<Status> AddStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = new Status(Guid.NewGuid().ToString(),
                status.Name,
                status.Description,
                status.Progress,
                status.CreatorId,
                status.TagId);
            await _unitOfWork
                .Status
                .AddAsync(result, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }

        public async Task UpdateStatusAsync
            (StatusNormalDto status,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Status.GetByIdAsync(status.Id, cancellationToken);
            result.UpdateName(status.Name);
            result.UpdateDescription(status.Description);
            result.UpdateProgress(status.Progress);
            result.UpdateCreatorId(status.CreatorId);
            result.UpdateTagId(status.TagId);
            _unitOfWork.Status.Update(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteStatusAsync
            (string statusId,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.Status.GetByIdAsync(statusId, cancellationToken);
            _unitOfWork.Status.Delete(result);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
