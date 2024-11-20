using OrderApi.Domain.Entities;
namespace OrderApi.Application.DTOs.Conversions
{
    public static class OrderConversion
    {

        public static Order ToEntity(OrderDTO order) => new()
        {
            Id = order.Id,
            ClientId = order.ClientId,
            ProductId = order.ProductId,
            OrderedData = order.OrderedDate,
            PurchaseQuantity = order.PurchaseQuantity
        };

        public static (OrderDTO?, IEnumerable<OrderDTO>?) FromEntity(Order? order, IEnumerable<Order>? orders)
        {
            //return single
            if (order is not null || orders is null)
            {
                var singleOder = new OrderDTO(
                    order!.Id,
                    order.ProductId,
                    order.ClientId,

                    order.PurchaseQuantity,
                    order.OrderedData


                    );
                return (singleOder, null);
            }

            // return List
            if (orders is not null || order is null)
            {
                var _orders = orders!.Select(o =>
                new OrderDTO(
                    // write squence wise entity otherwise it will mismatch
                    o.Id,
                    o.ProductId,
                    o.ClientId,
                    o.PurchaseQuantity,
                    o.OrderedData
                    ));
                return (null, _orders);
            }

            return (null, null);
        }

    }
}

