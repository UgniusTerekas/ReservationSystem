using DataLayer.Entities.City;
using DataLayer.Entities.Gallery;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.GalleryRepository
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly DataBaseContext _dbContext;

        public GalleryRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GalleryEntity>> GetGalleries()
        {
            return await _dbContext.Gallery.ToListAsync();
        }

        public async Task<bool> AddImageToDataBase(GalleryEntity galleryEntity)
        {
            _dbContext.Gallery.Add(galleryEntity);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<GalleryEntity> GetOneGallery(int galleryId)
        {
            return await _dbContext.Gallery.FindAsync(galleryId);
        }

        public async Task<GalleryEntity> CreateGallery(GalleryEntity gallery)
        {
            _dbContext.Gallery.Add(gallery);

            await _dbContext.SaveChangesAsync();

            return gallery;
        }

        public async Task<GalleryEntity> UpdateGallery(GalleryEntity gallery)
        {
            _dbContext.Entry(gallery).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return gallery;
        }

        public async Task DeleteGallery(GalleryEntity gallery)
        {
            _dbContext.Gallery.Remove(gallery);

            await _dbContext.SaveChangesAsync();
        }
    }
}
