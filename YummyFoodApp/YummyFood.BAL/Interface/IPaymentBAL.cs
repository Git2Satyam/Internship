using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;

namespace YummyFood.BAL.Interface
{
    public interface IPaymentBAL
    {
        string CreateOrder(decimal? amount, string currency, string receipt);
        Payment GetPaymentDetail(string paymentId);
        bool IsSignatureVerified(string signature, string OrderId, string paymentId);
        int SavePaymentDetail(PaymentDetail model);
    }
}
