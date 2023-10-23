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
        public static string GetImageData(int id)
        {
            using (var model = new ModelAppMovies())
            {
                var image = model.Images.FirstOrDefault(p => p.MovieID == id);

                if (image != null)
                {
                    // Đảm bảo rằng ImageData là kiểu dữ liệu thích hợp (ví dụ, byte[])
                    // Trả về dữ liệu ảnh dưới dạng chuỗi (nếu ảnh được lưu dưới dạng base64)
                    return image.ImageData.ToString();
                }
                else
                {
                    return "";

                }
            }
        }

        public static bool addImage(Images image)
        {
            ModelAppMovies model = new ModelAppMovies();
            using (var transaction = model.Database.BeginTransaction())
            {
                try
                {
                    model.Images.Add(image);
                    model.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public static bool KTMovieID(int movieID)
        {
            using (var model = new ModelAppMovies())
            {
                return model.Images.Any(p => p.MovieID == movieID);
            }
        }

        public static bool UpdateImages(int movieID, string ImageData)
        {

            ModelAppMovies model = new ModelAppMovies();
            using (var transaction = model.Database.BeginTransaction())
            {
                try
                {
                    Images existingImage = model.Images.FirstOrDefault(m => m.MovieID == movieID);
                    if (existingImage != null)
                    {
                        existingImage.ImageData = ImageData;

                        model.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
