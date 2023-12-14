using BlogDapper.Dto;
using BlogDapper.Entities;

namespace BlogDapper.Interfaces
{
    public interface IPublicacionRepository
    {
        public Task<IEnumerable<Publicacion>> GetPublicaciones();
        public Task<Publicacion> GetPublicacion(int id);
        public Task<Publicacion> CreatePublicacion(AddUpdatePublicacionDto addUpdatePublicacionDto);
        public Task UpdatePublicacion(int id, AddUpdatePublicacionDto publicacionDto);
        public Task DeletePublicacion(int id);
    }
} 