using System.Data;
using BlogDapperNew.Data;
using BlogDapperNew.Dto;
using BlogDapperNew.Entities;
using BlogDapperNew.Interfaces;
using Dapper;

namespace BlogDapper.Repository
{
    // Repositorio para realizar el CRUD solo con Procedimientos Almacenados
    public class PublicacionRepositorySP : IPublicacionRepository
    {
        private readonly DapperContext _context;
        public PublicacionRepositorySP(DapperContext context)
        {
            this._context = context;
        }

        public async Task<Publicacion> CreatePublicacion(AddUpdatePublicacionDto publicacionDto)
        {
            var storedProcedure = "spCreatePublicacion";

            var parameters = new DynamicParameters();
            parameters.Add("Id", publicacionDto.Id, DbType.Int32, direction:ParameterDirection.Output);
            parameters.Add("Titulo", publicacionDto.Titulo, DbType.String);
            parameters.Add("Contenido", publicacionDto.Contenido, DbType.String);
            parameters.Add("Resumen", publicacionDto.Resumen, DbType.String);
            parameters.Add("Etiquetas",publicacionDto.Contenido,DbType.String);
            parameters.Add("UsuarioId",publicacionDto.UsuarioId,DbType.Int32);
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(storedProcedure,parameters, commandType:CommandType.StoredProcedure);
            var id = parameters.Get<int>("@Id");

            return new Publicacion
            {
                Id=id,
                Contenido = publicacionDto.Contenido,
                Etiquetas = publicacionDto.Etiquetas,
                Resumen = publicacionDto.Resumen,
                Titulo = publicacionDto.Titulo,
                UsuarioId = publicacionDto.UsuarioId
            };
        }

        public async Task DeletePublicacion(int id)
        {
            using var connection = _context.CreateConnection(); 
            await connection.ExecuteAsync("spDeletePublicacion",new {Id = id}, commandType:CommandType.StoredProcedure);
        }

        public async Task<Publicacion> GetPublicacion(int id)
        {
            using var connection = _context.CreateConnection();
            var publicacion = await connection.QuerySingleOrDefaultAsync<Publicacion>("spGetPublicacion", new {id}, 
                                                                            commandType:CommandType.StoredProcedure);
            return publicacion;
        }

        public async Task<IEnumerable<Publicacion>> GetPublicaciones()
        {
            var storedProceduure = "spGetAllPublicacion";
            using var connection = _context.CreateConnection();
            var publicaciones = await connection.QueryAsync<Publicacion>(storedProceduure,null,
                                                                            commandType:CommandType.StoredProcedure);
            return publicaciones;
        }

        public async Task<UsuarioPublicacionDto> GetUsuarioPublicacion(int id)
        {
            var query = "spMostrarUsuarioPublicacion";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32,ParameterDirection.Input);
            using var connection = _context.CreateConnection();
            var usuarioPublicacion = await connection.QueryFirstAsync<UsuarioPublicacionDto>(query, parameters, 
                commandType:CommandType.StoredProcedure);
            return usuarioPublicacion; 
        }

        public Task<Publicacion> GetPublicacionConComentarios(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePublicacion(int id, AddUpdatePublicacionDto publicacionDto)
        {
            var storedProceduure = "spUpdatePublicacion";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Titulo", publicacionDto.Titulo, DbType.String);
            parameters.Add("Contenido", publicacionDto.Contenido, DbType.String);
            parameters.Add("Resumen", publicacionDto.Resumen, DbType.String);
            parameters.Add("Etiquetas",publicacionDto.Contenido,DbType.String);
            using var connection = _context.CreateConnection(); 
            await connection.ExecuteAsync(storedProceduure,parameters, commandType:CommandType.StoredProcedure);
        }
    }
}