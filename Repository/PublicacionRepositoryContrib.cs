using BlogDapper.Data;
using BlogDapper.Dto;
using BlogDapper.Entities;
using BlogDapper.Interfaces;
using Dapper.Contrib.Extensions;

namespace BlogDapper.Repository
{
    
    public class PublicacionRepositoryContrib : IPublicacionRepository
    {
        private readonly DapperContext _context;
        public PublicacionRepositoryContrib(DapperContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Publicacion>> GetPublicaciones()
        {
            using var connection = _context.CreateConnection();
            var publicaciones = await connection.GetAllAsync<Publicacion>();
            return publicaciones;
        }

        public async Task<Publicacion> GetPublicacion(int id)
        {
            using var connection = _context.CreateConnection();
            var publicaciones = await connection.GetAsync<Publicacion>(id);
            return publicaciones;
        }


        public async Task<Publicacion> CreatePublicacion(AddUpdatePublicacionDto publicacionDto)
        {
            using var connection = _context.CreateConnection();
            var nuevaPublicacion = new Publicacion
            {
                Contenido   = publicacionDto.Contenido,
                Etiquetas   = publicacionDto.Etiquetas,
                Resumen     = publicacionDto.Resumen,
                Titulo      = publicacionDto.Titulo,
                UsuarioId   = publicacionDto.UsuarioId    
            };
            var id = await connection.InsertAsync(nuevaPublicacion);
            return nuevaPublicacion;
        }

        public async Task UpdatePublicacion(int id, AddUpdatePublicacionDto publicacionDto)
        {
            using var connection = _context.CreateConnection();
            var nuevaPublicacion = new Publicacion
            {
                Id          = id,
                Contenido   = publicacionDto.Contenido,
                Etiquetas   = publicacionDto.Etiquetas,
                Resumen     = publicacionDto.Resumen,
                Titulo      = publicacionDto.Titulo,
                UsuarioId   = publicacionDto.UsuarioId    
            };
            await connection.UpdateAsync(nuevaPublicacion);
        }

        public async Task DeletePublicacion(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.DeleteAsync(new Publicacion() { Id = id});
        }

        

        


    }
}