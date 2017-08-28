﻿using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IPaymentRepository
    {
        Payment AddPayment(Payment payment);
        Payment UpdatePayment(Payment update);
    }
}