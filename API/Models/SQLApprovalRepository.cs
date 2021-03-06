using AutoMapper;
using PorabayData.Models;
using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Models
{
    public class SQLApprovalRepository : IApprovalRepository

    {
        private readonly PorabayContext dbContext;
        private readonly IMapper mapper;

        public SQLApprovalRepository(PorabayContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ApprovalVM addApprovalDetails(ApprovalVM approvalVM)
        {
            if (approvalVM != null)
            {
                var approvaldata = mapper.Map<Approval>(approvalVM);
                dbContext.tblApproval.Add(approvaldata);
                dbContext.SaveChanges();
            }

            return mapper.Map<ApprovalVM>(dbContext.tblApproval.FirstOrDefault(x => x.Id == 11));
        }

        public ApprovalVM DeleteApprovalDetails(int id)
        {
            var approval = dbContext.tblApproval.Find(id);

            if (approval != null)
            {
                dbContext.tblApproval.Remove(approval);
                dbContext.SaveChanges();
            }


            return mapper.Map<ApprovalVM>(approval);
        }

        public ApprovalVM EditApprovalDetails(int id, ApprovalVM approvalVM)
        {
            var approval = mapper.Map<Approval>(approvalVM);
            approval.Id = id;

            dbContext.Entry(approval).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            return mapper.Map<ApprovalVM>(dbContext.tblApproval.FirstOrDefault(x => x.Id == id));
        }

        public ApprovalVM GetApproval(int id)
        {
            return mapper.Map<ApprovalVM>(dbContext.tblApproval.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<ApprovalVM> GetApprovals()
        {
         return (from a in this.dbContext.tblApproval
                    join u in this.dbContext.tblLogin
                    on a.UserId equals u.LoginID
                    select new ApprovalVM {
                        Id = a.Id,
                        CreatedBy = u.Username,
                        Desc = "Applied",
                        EndDate = a.LeaveDate,
                        LeaveDate = a.LeaveDate,
                        StartDate = a.LeaveDate,
                        Status = a.Status,
                        Title = "Leave",
                        UserId = a.UserId,
                        Comment = a.Comment,
                        // Type = 2,
                        Color = "red"
                    }).ToList();
        }
    }
}
