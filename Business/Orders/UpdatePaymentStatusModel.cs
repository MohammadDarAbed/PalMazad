using DataAccess.Models;

namespace Business.Orders
{
    public class UpdatePaymentStatusModel
    {
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
    }
}
