﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Repositories;

namespace Bug.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugContext _bugContext;
        private IAccountRepo _account;
        private ICategoryRepo _category;
        private ICommentRepo _comment;
        private IIssueRepo _issue;
        private IIssuelogRepo _issuelog;
        private IPermissionRepo _permission;
        private IPriorityRepo _priority;
        private IProjectRepo _project;
        private IRelationRepo _relation;
        private IRoleRepo _role;
        private IStatusRepo _status;
        private ITagRepo _tag;
        private IWorklogRepo _worklog;

        public UnitOfWork(BugContext bugContext)
        {
            _bugContext = bugContext;
        }
        public IAccountRepo Account
        {
            get
            {
                if(_account == null)
                {
                    _account = new AccountRepo(_bugContext);
                }
                return _account;
            }
        }
        public ICategoryRepo Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepo(_bugContext);
                }
                return _category;
            }
        }
        public ICommentRepo Comment
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentRepo(_bugContext);
                }
                return _comment;
            }
        }
        public IIssueRepo Issue
        {
            get
            {
                if (_issue == null)
                {
                    _issue = new IssueRepo(_bugContext);
                }
                return _issue;
            }
        }
        public IIssuelogRepo Issuelog
        {
            get
            {
                if (_issuelog == null)
                {
                    _issuelog = new IssuelogRepo(_bugContext);
                }
                return _issuelog;
            }
        }
        public IPermissionRepo Permission
        {
            get
            {
                if (_permission == null)
                {
                    _permission = new PermissionRepo(_bugContext);
                }
                return _permission;
            }
        }
        public IPriorityRepo Priority
        {
            get
            {
                if (_priority == null)
                {
                    _priority = new PriorityRepo(_bugContext);
                }
                return _priority;
            }
        }
        public IProjectRepo Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepo(_bugContext);
                }
                return _project;
            }
        }
        public IRelationRepo Relation
        {
            get
            {
                if (_relation == null)
                {
                    _relation = new RelationRepo(_bugContext);
                }
                return _relation;
            }
        }
        public IRoleRepo Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepo(_bugContext);
                }
                return _role;
            }
        }
        public IStatusRepo Status
        {
            get
            {
                if (_status == null)
                {
                    _status = new StatusRepo(_bugContext);
                }
                return _status;
            }
        }
        public ITagRepo Tag
        {
            get
            {
                if (_tag == null)
                {
                    _tag = new TagRepo(_bugContext);
                }
                return _tag;
            }
        }
        
        public IWorklogRepo Worklog
        {
            get
            {
                if (_worklog == null)
                {
                    _worklog = new WorklogRepo(_bugContext);
                }
                return _worklog;
            }
        }
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _bugContext.SaveChangesAsync(cancellationToken);
        }
    }
}
