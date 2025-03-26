// CoreApp/PayPal/PaymentService.cs

using System.Globalization;
using PayPal.Api;

namespace CoreApp.PayPal;

public class PaymentService
{
    public Payment CreatePayment(string returnUrl, string cancelUrl, double amount)
    {
        var apiContext = PayPalConfiguration.GetAPIContext();
        var colonToDollar = amount / 527.61;
        var cost = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", colonToDollar);

        var payment = new Payment
        {
            intent = "sale",
            payer = new Payer { payment_method = "paypal" },
            transactions = new List<Transaction>
            {
                new()
                {
                    description = "Pago a Silueta Club Fitness", /* "Transaction description" */
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = new Amount
                    {
                        currency = "USD",
                        total = cost /* "10.00" // Monto total del pago*/
                    },
                    item_list = new ItemList
                    {
                        items = new List<Item>
                        {
                            new()
                            {
                                name = "Membresía y costos adicionales", /* "Item Name" */
                                currency = "USD",
                                price = cost, /* "10.00", */
                                quantity = "1",
                                sku = "Membresía"
                            }
                        }
                    }
                }
            },
            redirect_urls = new RedirectUrls
            {
                cancel_url = cancelUrl,
                return_url = returnUrl
            }
        };

        return payment.Create(apiContext);
    }

    public Payment ExecutePayment(string paymentId, string payerId)
    {
        var apiContext = PayPalConfiguration.GetAPIContext();
        var paymentExecution = new PaymentExecution { payer_id = payerId };
        var payment = new Payment { id = paymentId };
        return payment.Execute(apiContext, paymentExecution);
    }
}