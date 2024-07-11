using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.API.Models;
using Product.DB;
using Product.DB.Entities;

namespace Product.API.Services
{
    public interface IProductService
    {
        Task<List<DtoProduct>> GetProductsAsync();
        Task<DtoProduct> GetProductByIdAsync(int id);
        Task<List<DtoProduct>> GetProductsByNameAsync(string nameFragment);
        Task<DtoProduct> PostProductAsync(DtoProduct dtoProduct);
        Task PutProductAsync(DtoProduct dtoProduct);
        Task DeleteProductAsync(int id);
    }
    public class ProductService : IProductService
    {
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;
        
        public ProductService(MainContext mainContext, IMapper mapper)
        {
            _mainContext = mainContext;
            _mapper = mapper;
        }
        public async Task<List<DtoProduct>> GetProductsAsync()
        {
            var products = await _mainContext.Products.ToListAsync();
            return _mapper.Map<List<DtoProduct>>(products);
        }

        public async Task<DtoProduct> GetProductByIdAsync(int id)
        {
            var product = await _mainContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<DtoProduct>(product);
        }

        public async Task<List<DtoProduct>> GetProductsByNameAsync(string nameFragment)
        {
            var products = await _mainContext.Products
                .Where(p => p.Name.Contains(nameFragment))
                .Take(5)
                .ToListAsync();

            return _mapper.Map<List<DtoProduct>>(products);
        }

        public async Task<DtoProduct> PostProductAsync(DtoProduct dtoProduct)
        {
            // Проверка, что строка Base64 не пуста
            if (!string.IsNullOrWhiteSpace(dtoProduct.Image))
            {
                // Генерация уникального имени файла с помощью GUID
                var uniqueFileName = Guid.NewGuid().ToString() + ".png"; // Вы можете выбрать другой формат файла

                // Путь, где будет сохранен файл
                var filePath = Path.Combine("Uploads", uniqueFileName);

                // Конвертация строки Base64 в массив байтов
                var imageBytes = Convert.FromBase64String(dtoProduct.Image);

                // Сохранение файла в папку 'uploads'
                await File.WriteAllBytesAsync(filePath, imageBytes);

                // Добавление пути к файлу в объект product
                var product = _mapper.Map<DbProduct>(dtoProduct);
                product.ImagePath = filePath; // Убедитесь, что у DbProduct есть свойство ImagePath

                _mainContext.Products.Add(product);
                await _mainContext.SaveChangesAsync();

                return _mapper.Map<DtoProduct>(product);
            }

            // Если строка Base64 пуста, обрабатываем как обычно без изображения
            var productWithoutImage = _mapper.Map<DbProduct>(dtoProduct);
            _mainContext.Products.Add(productWithoutImage);
            await _mainContext.SaveChangesAsync();

            return _mapper.Map<DtoProduct>(productWithoutImage);
        }

        public async Task PutProductAsync(DtoProduct dtoProduct)
        {
            var dbProduct = await _mainContext.Products.FirstOrDefaultAsync(p => p.Id == dtoProduct.Id);

            _mapper.Map(dtoProduct, dbProduct);
            _mainContext.Products.Update(dbProduct);

            await _mainContext.SaveChangesAsync();
            //return _mapper.Map<DtoProduct>(dbProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _mainContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            product.Deleted = DateTimeOffset.UtcNow;
            _mainContext.Products.Update(product);

            await _mainContext.SaveChangesAsync();
            //return _mapper.Map<DtoProduct>(product);
        }
    }
}
