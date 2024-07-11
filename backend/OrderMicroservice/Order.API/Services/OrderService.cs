using AutoMapper;
using Order.API.Models;
using Order.DB;
using Order.DB.Entities;

namespace Order.API.Services
{
    public interface IOrderService
    {
        Task<DtoOrder> PostOrderAsync(DtoOrder dtoOrder);
    }
    public class OrderService : IOrderService
    {
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;
        public OrderService(MainContext mainContext, IMapper mapper)
        {
            _mainContext = mainContext;
            _mapper = mapper;
        }
        public async Task<DtoOrder> PostOrderAsync(DtoOrder dtoOrder)
        {
            // Преобразование DtoOrder в DbOrder
            var dbOrder = _mapper.Map<DbOrder>(dtoOrder);

            // Добавление заказа в контекст базы данных
            await _mainContext.Orders.AddAsync(dbOrder);
            await _mainContext.SaveChangesAsync();

            // Преобразование каждого DtoOrderItem в DbOrderItems и добавление в базу данных
           // foreach (var dtoOrderItem in dtoOrder.OrderItems)
           // {
            //    var dbOrderItem = _mapper.Map<DbOrderItems>(dtoOrderItem);
            //    dbOrderItem.OrderID = dbOrder.Id; // Установка ID заказа для каждого элемента
            //    await _mainContext.OrderItems.AddAsync(dbOrderItem);
            //}

            // Сохранение изменений в базе данных
            //await _mainContext.SaveChangesAsync();

            // Преобразование DbOrder обратно в DtoOrder для возврата
            var resultDtoOrder = _mapper.Map<DtoOrder>(dbOrder);
            resultDtoOrder.OrderItems = _mapper.Map<List<DtoOrderItem>>(dbOrder.OrderItems);

            return resultDtoOrder;
        }
    }
}
