using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Models
{
    public interface IApprovalRepository
    {
        ApprovalVM GetApproval(int id);

        IEnumerable<ApprovalVM> GetApprovals();

        ApprovalVM addApprovalDetails(ApprovalVM approvalVM);

         ApprovalVM DeleteApprovalDetails(int id);

        ApprovalVM EditApprovalDetails(int id, ApprovalVM approvalVM);
    }
}
