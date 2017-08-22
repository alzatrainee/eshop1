using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface ICartRepository
    {
        Cart GetCart(int id_user);
        Cart AddCart(Cart profile);
    }
}
