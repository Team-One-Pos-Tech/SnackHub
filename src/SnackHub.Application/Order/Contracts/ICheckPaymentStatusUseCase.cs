using SnackHub.Application.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackHub.Application.Order.Contracts
{
    public interface ICheckPaymentStatusUseCase
    {
        Task<CheckPaymentStatusResponse> Execute(CheckPaymentStatusRequest request);
    }

}
