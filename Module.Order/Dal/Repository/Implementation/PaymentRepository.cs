using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrderDbContext _context;
        public PaymentRepository (OrderDbContext context)
        {
            _context = context;
        }

        public Payment AddPayment(Payment order)
        {
            _context.Payment.Add(order);
            _context.SaveChanges();

            return order;
        }

        public Payment UpdatePayment (Payment update)
        {
            _context.Payment.Update(update);
            _context.SaveChanges();
            return update;
        }

        public Payment GetPayment(int id_pay)
        {
            var result = _context.Payment.Where(p => p.id_pay == id_pay).FirstOrDefault();
            return result;
        }

        public Method GetPaymentMethod(int id_meth)
        {
            var result = _context.Method.Where(p => p.id_meth == id_meth).FirstOrDefault();
            return result;
        }
    }
}
