using BlogDapperNew.Dto;
using BlogDapperNew.Entities;



namespace BlogDapperNew.Interfaces
{
    public interface IPublicacionRepository
    {
        public Task<IEnumerable<Publicacion>> GetPublicaciones();
        public Task<Publicacion> GetPublicacion(int id);
        public Task<Publicacion> CreatePublicacion(AddUpdatePublicacionDto addUpdatePublicacionDto);
        public Task UpdatePublicacion(int id, AddUpdatePublicacionDto publicacionDto);
        public Task DeletePublicacion(int id);
        public Task<UsuarioPublicacionDto> GetUsuarioPublicacion(int id);
        public Task<Publicacion> GetPublicacionConComentarios(int id);
    }
} 