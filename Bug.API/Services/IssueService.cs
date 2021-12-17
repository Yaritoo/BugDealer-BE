﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Data.Infrastructure;
using Bug.Entities.Builder;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using Bug.API.Dto;
using System.Text.RegularExpressions;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Bug.API.Utils;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IssueService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Stream> ExportIssueExcelFile
            (string issueId, 
            Stream stream = null,
            CancellationToken cancellationToken = default)
        {
            var issue = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(new IssueSpecification(issueId), cancellationToken);
            return await new ExcelUtils()
                .CreateExcelFile(issue, stream);
        }

        public async Task<Issue> GetNormalIssueAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Issue
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<Issue> GetDetailIssueAsync
            (string id,
            CancellationToken cancellationToken = default)
        {
            IssueSpecification specificationResult =
                new(id);
            return await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedByProjectIdSearchAsync
            (string search,
            string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByProjectIdSearchSpecification(projectId, search);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedNoTrackBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedByRelateUserAsync
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByRelateUserSpecification(accountId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextByOffsetByRelateUserAsync
            (string accountId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByRelateUserSpecification(accountId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAsync
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            IssueByProjectSpecification specificationResult =
                new(projectId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAsync
            (string projectId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueByProjectSpecification(projectId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdReporterIdAsync
            (string projectId,
            string reportId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdProjectIdSpecification(projectId, reportId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdReporterIdAsync
            (string projectId,
            string reporterId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByReporterIdProjectIdSpecification(projectId, reporterId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<PaginatedListDto<Issue>> GetPaginatedDetailByProjectIdAssigneeIdAsync
            (string projectId,
            string assigneeId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdProjectIdSpecification(projectId,assigneeId);
            var result = await _unitOfWork
                .Issue
                .GetPaginatedBySpecAsync(pageIndex, pageSize, sortOrder, specificationResult, cancellationToken);
            return new PaginatedListDto<Issue>
            {
                Length = result.Length,
                Items = result
            };
        }

        public async Task<IReadOnlyList<Issue>> GetNextDetailByOffsetByProjectIdAssigneeIdAsync
            (string projectId,
            string assigneeId,
            int offset,
            int next,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuesByAssigneeIdSpecification(assigneeId);
            var result = await _unitOfWork
                .Issue
                .GetNextByOffsetBySpecAsync(offset, next, sortOrder, specificationResult, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyList<Issue>> GetSuggestIssueByCode
            (string search,
            string projectId,
            string issueId,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var result =  await _unitOfWork
                .Issue
                .GetSuggestIssuesAsync(projectId, search, sortOrder, cancellationToken);
            result.Remove(result.FirstOrDefault(i => i.Id == issueId));
            return result;
        }

        public async Task<Issue> AddIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var pro = await _unitOfWork
                .Project
                .GetByIdAsync(issue.ProjectId, cancellationToken);
            var result = new IssueBuilder()
                .AddId(Guid.NewGuid().ToString())
                .AddDescription(issue.Description)
                .AddNumberCode(pro.Temp)
                .AddAssigneeId(issue.AssigneeId)
                .AddCreatedDate(issue.CreatedDate)
                .AddDueDate(issue.DueDate)
                .AddEnvironment(issue.Environment)
                .AddOriginEstimateTime(issue.OriginEstimateTime)
                .AddPriorityId(issue.PriorityId)
                .AddSeverityId(issue.SeverityId)
                .AddProjectId(issue.ProjectId)
                .AddRemainEstimateTime(issue.RemainEstimateTime)
                .AddReporterId(issue.ReporterId)
                .AddStatusId(issue.StatusId??pro.DefaultStatusId)
                .AddTitle(issue.Title)
                .Build();
            //increase after create new issue to generate code of issue
            pro.Temp += 1;
            var customLabelTags = issue
                .Tags
                .Select(l => 
                {                     
                    var item = new Tag(l.Id, l.Name, l.Description, l.Color, l.CategoryId);
                    if (item.Id == 0)
                        _unitOfWork.Tag.Add(item);
                    else
                        _unitOfWork.Tag.Attach(item);                   
                    return item;
                })
                .ToList();
            result.UpdateTags(customLabelTags);

            var attachments = issue
                .Attachments
                .Select(a => 
                { 
                    var item = new Attachment(a.Id, a.Uri, a.IssueId);
                    if (item.Id == 0)
                        _unitOfWork.Attachment.Add(item);
                    else
                        _unitOfWork.Attachment.Attach(item);
                    return item;
                })
                .ToList();
            result.UpdateAttachments(attachments);

            var fromRelations = issue
                .FromRelations
                .Select(r => 
                { 
                    var item = new Relation(r.Description, r.TagId, result.Id, r.ToIssueId);
                    return item;
                })
                .ToList();
            result.UpdateFromRelations(fromRelations);

            await _unitOfWork
                .Issue
                .AddAsync(result, cancellationToken);

            _unitOfWork.Save();

            return result;
        }

        public async Task UpdateIssueAsync
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Issue
                .GetByIdAsync(issue.Id, cancellationToken);
            result.UpdateAssigneeId(issue.AssigneeId);           
            result.UpdateDescription(issue.Description);
            result.UpdateEnvironment(issue.Environment);
            result.UpdateCreatedDate(issue.CreatedDate);
            result.UpdateDueDate(issue.DueDate);
            result.UpdateOriginalEstimateTime(issue.OriginEstimateTime);
            result.UpdatePriorityId(issue.PriorityId);
            result.UpdateSeverityId(issue.SeverityId);           
            result.UpdateRemainEstimateTime(issue.RemainEstimateTime);
            result.UpdateReporterId(issue.ReporterId);
            result.UpdateStatusId(issue.StatusId);
            result.UpdateTitle(issue.Title);
            
            _unitOfWork.Save();
        }       

        public async Task UpdateTagsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var tags = issue
                .Tags
                .Select(t => new Tag(t.Id, t.Name, t.Description, t.Color, t.CategoryId))
                .ToList();
            await _unitOfWork.Issue.UpdateTagsOfIssueAsync(issue.Id, tags, cancellationToken);
        }

        public async Task UpdateFromRelationsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssueSpecification(issue.Id);
            var result = await _unitOfWork
                .Issue
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
            var fromRelations = issue
                .FromRelations
                .Select(r => new Relation(r.Description, r.TagId, result.Id, r.ToIssueId))
                .ToList();
            result.UpdateFromRelations(fromRelations);
            
            _unitOfWork.Save();
        }

        public async Task UpdateAttachmentsOfIssue
            (IssueNormalDto issue,
            CancellationToken cancellationToken = default)
        {
            var attachments = issue.Attachments
                .Select(a => new Attachment(a.Id, a.Uri, a.IssueId))
                .ToList();
            await _unitOfWork
                .Issue
                .UpdateAttachmentsOfIssueAsync(issue.Id, attachments, cancellationToken);
        }

        public async Task DeleteIssueAsync
            (string id, 
            CancellationToken cancellationToken = default)
        {
            await _unitOfWork
                .Relation
                .DeleteRelationByIssueAsync(id);
            var result = _unitOfWork
                .Issue
                .GetByIdAsync(id, cancellationToken).Result;
            _unitOfWork.Issue.Delete(result);

            _unitOfWork.Save();
        }
    }
}
