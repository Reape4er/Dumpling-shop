using AutoMapper;
using Product.API.Models;
using Product.DB.Entities;

namespace Users.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbProduct, DtoProduct>()
                    .ForMember(dest => dest.Image, opt => opt.ConvertUsing(new ImagePathToBase64Converter(), src => src.ImagePath));
            CreateMap<DtoProduct, DbProduct>();
        }

        public class ImagePathToBase64Converter : IValueConverter<string, string>
        {
            public string Convert(string sourceMember, ResolutionContext context)
            {
                if (!string.IsNullOrWhiteSpace(sourceMember))
                {
                    // Путь к файлу
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), sourceMember);

                    // Проверка существования файла
                    if (File.Exists(filePath))
                    {
                        // Чтение файла в массив байтов
                        byte[] imageBytes = File.ReadAllBytes(filePath);
                        // Конвертация массива байтов в строку Base64
                        return System.Convert.ToBase64String(imageBytes);
                    }
                }
                // Возвращаем null или пустую строку, если путь неверен или файл не существует
                return null;
            }
        }
    }
}
