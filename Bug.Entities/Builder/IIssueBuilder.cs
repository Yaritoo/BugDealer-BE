﻿using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public interface IIssueBuilder
    {
        IIssueBuilder AddId(string id);
        IIssueBuilder AddTitle(string title);
        IIssueBuilder AddDescription(string des);
        IIssueBuilder AddLogDate(DateTime tl);
        IIssueBuilder AddCreatedDate(DateTime cd);
        IIssueBuilder AddDueDate(DateTime dd);
        IIssueBuilder AddOriginEstimateTime(string oet);
        IIssueBuilder AddRemainEstimateTime(string ret);
        IIssueBuilder AddEnvironment(string e);
        IIssueBuilder AddStatusId(string s);
        IIssueBuilder AddPriorityId(int p);
        IIssueBuilder AddProjectId(string s);
        IIssueBuilder AddReporterId(string s);
        IIssueBuilder AddAssigneeId(string s);
        Issue Build();
    }
}
