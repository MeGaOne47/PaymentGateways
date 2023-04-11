using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PaymentGateways.Models;

namespace PaymentGateways.Controllers
{
    public class PayPalController : Controller
    {

        public ActionResult CreatePayment()
        {
            var apiContext = new APIContext();
            var config = new Dictionary<string, string>
            {
                {"mode", "sandbox"},
                {"clientId", "ASH7G5Bkr-NTSCionZ21xqwXSK-bLdymlset-4MhrdlnKkTFDI45bRbrkmdI-c6o_U36XeuL5IhTEQQp"},
                {"clientSecret", "EDm14CnQEMzHQkIW2urflVFrNdRX0By0QHYw4uhBw_MSOlXQAcs0CTlp1vZMiUcQhPDnHmTSk8nVhhJt"}
            };
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext1 = new APIContext(accessToken);

            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            var itemList = new ItemList() { items = new List<Item>() };

            // Lặp qua từng giỏ hàng và thêm vào itemList
            foreach (var gioHang in lstGioHang)
            {
                // Tính giá trị sản phẩm trong đơn vị tiền tệ USD
                var priceUSD = gioHang.dDonGia / 23000; // 1 USD = 23.000 VND

                itemList.items.Add(new Item()
                {
                    name = gioHang.sTensp,
                    currency = "USD", // Chuyển đổi đơn vị tiền tệ sang USD
                    price = priceUSD.ToString("0.00"), // Làm tròn đến 2 chữ số thập phân
                    quantity = gioHang.nhSoLuong.ToString(),
                    sku = gioHang.nhMasp.ToString()
                });
            }

            string host = Request.Url.Host.ToLower();


            var payer = new Payer() { payment_method = "paypal" };
            var redirectUrls = new RedirectUrls()
            {
                cancel_url = "https://localhost:44325/Paypal/Cancel",
                return_url = "https://localhost:44325/Paypal/Return"
            };

            // Tính tổng giá trị của các sản phẩm trong giỏ hàng
            var total = lstGioHang.Sum(gioHang => gioHang.dDonGia * gioHang.nhSoLuong) / 23000; // Chuyển đổi đơn vị tiền tệ sang USD

            var amount = new Amount() { currency = "USD", total = total.ToString("0.00") }; // Làm tròn đến 2 chữ số thập phân
            var transaction = new List<Transaction>() { new Transaction() { description = "Transaction Description", amount = amount, item_list = itemList } };
            var payment = new Payment() { intent = "sale", payer = payer, redirect_urls = redirectUrls, transactions = transaction };
            var createdPayment = payment.Create(apiContext1);
            var redirectUrl = createdPayment.links.FirstOrDefault(x => x.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));
            return Redirect(redirectUrl.href);
        }

        public ActionResult Return()
        {
            // Create an APIContext object
            var apiContext = new APIContext();

            // Set up the configuration for the PayPal sandbox environment
            var config = new Dictionary<string, string>
            {
                {"mode", "sandbox"},
                {"clientId", "ASH7G5Bkr-NTSCionZ21xqwXSK-bLdymlset-4MhrdlnKkTFDI45bRbrkmdI-c6o_U36XeuL5IhTEQQp"},
                {"clientSecret", "EDm14CnQEMzHQkIW2urflVFrNdRX0By0QHYw4uhBw_MSOlXQAcs0CTlp1vZMiUcQhPDnHmTSk8nVhhJt"}
            };

            // Get an access token using the OAuthTokenCredential object
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            // Create a new APIContext object with the access token
            var apiContext1 = new APIContext(accessToken);

            // Get the payment ID and payer ID from the request parameters
            var paymentId = Request.Params["paymentId"];
            var payerId = Request.Params["PayerID"];

            // Set up the PaymentExecution object
            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            // Create a new Payment object with the payment ID
            var payment = new Payment() { id = paymentId };

            // Execute the payment and get the executed payment object
            var executedPayment = payment.Execute(apiContext1, paymentExecution);

            // Create a new ThanhToan object
            var thanhToan = new ThanhToan();

            // Retrieve the total amount of the first transaction and assign it to the Amount property of the ThanhToan object
            thanhToan.Amount = decimal.Parse(executedPayment.transactions[0].amount.total);

            // Set the PaymentDate property to the current date and time
            thanhToan.PaymentDate = DateTime.Now;

            // Set the IsSuccessful property to true
            thanhToan.IsSuccessful = true;

            // Save the ThanhToan object to the database
            using (var db = new Qlbanhang())
            {
                db.ThanhToans.Add(thanhToan);

                // Save the user's PayPal account and user ID to the database
                var userId = User.Identity.GetUserId();
                var user = db.ThanhToans.FirstOrDefault(u => u.PaypalUserId == userId);

                if (user != null)
                {
                    user.PaypalAccount = executedPayment.payer.payer_info.email;
                    user.PaypalUserId = executedPayment.payer.payer_info.payer_id;
                }

                db.SaveChanges();
            }

            // Return a view with the executedPayment object and the ThanhToan object
            return View(new { ExecutedPayment = executedPayment, ThanhToan = thanhToan });
        }





        public ActionResult Cancel()
        {
            return View();
        }
    }
}