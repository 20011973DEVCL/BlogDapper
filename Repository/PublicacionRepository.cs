using System.Data;
using BlogDapperNew.Data;
using BlogDapperNew.Dto;
using BlogDapperNew.Entities;
using BlogDapperNew.Interfaces;
using Dapper;

namespace BlogDapper.Repository
{
    // Repositorio para realizar el CRUD solo con instrucciones SQL
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly DapperContext _context;
        public PublicacionRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<Publicacion> GetPublicacion(int id)
        {
            var query = "SELECT * FROM Publicacion WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            var publicacion = await connection.QuerySingleOrDefaultAsync<Publicacion>(query, new {id});
            return publicacion;
        }

        public async Task<IEnumerable<Publicacion>> GetPublicaciones()
        {
            var query = "SELECT * FROM Publicacion";
            using var connection = _context.CreateConnection();
            var publicaciones = await connection.QueryAsync<Publicacion>(query);
            return publicaciones;
        }

        public async Task<Publicacion> CreatePublicacion(AddUpdatePublicacionDto publicacionDto)
        {
            var query = @"INSERT INTO Publicacion(Titulo, Resumen, Contenido, Etiquetas, UsuarioId)
                            VALUES (@Titulo, @Resumen, @Contenido, @Etiquetas, @UsuarioId);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Titulo", publicacionDto.Titulo, DbType.String);
            parameters.Add("Contenido", publicacionDto.Contenido, DbType.String);
            parameters.Add("Resumen", publicacionDto.Resumen, DbType.String);
            parameters.Add("Etiquetas",publicacionDto.Contenido,DbType.String);
            parameters.Add("UsuarioId",publicacionDto.UsuarioId,DbType.Int32);
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query,parameters);
            // await connection.ExecuteAsync(query,parameters);
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

        public async Task UpdatePublicacion(int id, AddUpdatePublicacionDto publicacionDto)
        {
            var query = @"UPDATE Publicacion SET Titulo=@Titulo, Resumen=@Resumen, 
                        Contenido=@Contenido, Etiquetas=@Etiquetas, UsuarioId=@UsuarioId WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Titulo", publicacionDto.Titulo, DbType.String);
            parameters.Add("Contenido", publicacionDto.Contenido, DbType.String);
            parameters.Add("Resumen", publicacionDto.Resumen, DbType.String);
            parameters.Add("Etiquetas",publicacionDto.Contenido,DbType.String);
            using var connection = _context.CreateConnection(); 
            await connection.ExecuteAsync(query,parameters);
        }

        public async Task DeletePublicacion(int id)
        {
            var query = @"DELETE FROM Publicacion WHERE Id=@Id";
            using var connection = _context.CreateConnection(); 
            await connection.ExecuteAsync(query,new {id});
        }

        public async Task<UsuarioPublicacionDto> GetUsuarioPublicacion(int id)
        {
            var query = @"SELECT U.Id, U.Nombre, U.Apellidos, U.Email, D.Nombre as Departamento, A.Nombre as Area
                            FROM Usuario U
                            LEFT JOIN Publicacion P ON U.Id=P.UsuarioId
                            LEFT JOIN Departamento D ON D.Id = U.DepartamentoId
                            LEFT JOIN Area A ON A.Id=D.AreaId
                            WHERE P.Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32,ParameterDirection.Input);
            using var connection = _context.CreateConnection();
            var usuarioPublicacion = await connection.QueryFirstAsync<UsuarioPublicacionDto>(query, parameters);
            return usuarioPublicacion; 
        }

        public async Task<Publicacion> GetPublicacionConComentarios(int id)
        {
            var query = @"SELECT * FROM Publicacion WHERE Id=@Id;
                            SELECT * FROM Comentario WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            using var queryMultiple = await connection.QueryMultipleAsync(query, new {id});
            var publicacion = await queryMultiple.ReadSingleOrDefaultAsync<Publicacion>();
            if (publicacion!=null)
                publicacion.Comentarios = (await queryMultiple.ReadAsync<Comentario>()).ToList();
            return publicacion;

        }
    }
}