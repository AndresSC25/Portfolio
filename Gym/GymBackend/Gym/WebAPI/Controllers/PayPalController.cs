// Controller/PayPalController.cs

using CoreApp.PayPal;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PayPalController : ControllerBase
{
    [HttpPost("create-payment")]
    public IActionResult CreatePayment(double amount)
    {
        var paymentService = new PaymentService();
        var returnUrl = $"{Request.Scheme}://{Request.Host}/api/paypal/payment-success";
        var cancelUrl = $"{Request.Scheme}://{Request.Host}/api/paypal/payment-cancel";

        var payment = paymentService.CreatePayment(returnUrl, cancelUrl, amount);
        var approvalUrl = payment.links
            .FirstOrDefault(x => x.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

        if (approvalUrl != null) return Ok(new { redirectUrl = approvalUrl });

        return BadRequest("No se pudo crear el pago.");
    }

    [HttpGet("payment-success")]
    public IActionResult PaymentSuccess(string paymentId, string token, string PayerID)
    {
        var paymentService = new PaymentService();

        try
        {
            var executedPayment = paymentService.ExecutePayment(paymentId, PayerID);
            if (executedPayment.state.ToLower() == "approved")
                // Devolver un script para cerrar la ventana emergente y notificar el éxito
                return Content("<script>window.opener.postMessage('success', '*'); window.close();</script>",
                    "text/html");

            return Content("<script>window.opener.postMessage('failed', '*'); window.close();</script>", "text/html");
        }
        catch (Exception ex)
        {
            return Content("<script>window.opener.postMessage('error', '*'); window.close();</script>", "text/html");
        }
    }

    [HttpGet("payment-cancel")]
    public IActionResult PaymentCancel()
    {
        // Devolver una vista o un script para cerrar la ventana emergente
        return Content("<script>window.opener.postMessage('cancelled', '*'); window.close();</script>", "text/html");
    }

    [HttpPost("execute-payment")]
    public IActionResult ExecutePayment([FromQuery] string paymentId, [FromQuery] string payerId)
    {
        var paymentService = new PaymentService();
        var payment = paymentService.ExecutePayment(paymentId, payerId);

        if (payment.state.ToLower() == "approved") return Ok(new { status = "Pago aprobado" });

        return BadRequest("No se pudo aprobar el pago.");
    }
}