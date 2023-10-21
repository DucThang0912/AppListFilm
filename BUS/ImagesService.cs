using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ImagesService
    {
        public static string GetImageData(int imageID)
        {
            using (var model = new ModelAppMovies())
            {
                var image = model.Images.FirstOrDefault(p => p.ImageID == imageID);

                if (image != null)
                {
                    // Đảm bảo rằng ImageData là kiểu dữ liệu thích hợp (ví dụ, byte[])
                    // Trả về dữ liệu ảnh dưới dạng chuỗi (nếu ảnh được lưu dưới dạng base64)
                    return image.ImageData.ToString();
                }
                else
                {
                    return null;

                }
            }
        }
    }
}
