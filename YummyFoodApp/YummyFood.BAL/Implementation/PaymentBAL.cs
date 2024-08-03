using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YummyFood.BAL.Interface;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;

namespace YummyFood.BAL.Implementation
{
    public class PaymentBAL : IPaymentBAL
    {
        private readonly RazorpayClient _client;
        IConfiguration _config;
        IDataAccessLayer<PaymentDetail> _paymentAccessLayer;
        public PaymentBAL(IConfiguration config, IDataAccessLayer<PaymentDetail> paymentAccessLayer)
        {
            _config = config;
            _paymentAccessLayer = paymentAccessLayer;
            _client = new RazorpayClient(_config["RazorPay:Key"], _config["RazorPay:Secret"]);
        }
        public string CreateOrder(decimal? amount, string currency, string receipt)
        {
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", amount);
                options.Add("currency", "INR");
                options.Add("receipt", receipt);
                Razorpay.Api.Order order = _client.Order.Create(options);
                return order["id"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        public Payment GetPaymentDetail(string paymentId)
        {
            return _client.Payment.Fetch(paymentId);
        }

        public bool IsSignatureVerified(string signature, string OrderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", OrderId, paymentId);
            string secretkey = RazorpayClient.Secret;
            string actualSign = getActualSignature(payload, secretkey);
            return actualSign.Equals(signature);
        }

        private static string getActualSignature(string payload, string secretkey)
        {
            byte[] secretBytes = StringEncode(secretkey);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);
            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public int SavePaymentDetail(PaymentDetail model)
        {
            try
            {
                _paymentAccessLayer.Add(model);
                return _paymentAccessLayer.saveChanges();
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
