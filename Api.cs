using BlogDapperNew.Dto;
using BlogDapperNew.Interfaces;

namespace BlogDapperNew;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/publicaciones", ObtenerPublicaciones);
        app.MapGet("/publicaciones/{id}", ObtenerPublicacion);
        app.MapPost("/publicaciones", AgregarPublicacion);
        app.MapPut("/publicaciones/{id}", ActualizarPublicacion);
        app.MapDelete("/publicaciones/{id}", EliminarPublicacion);
        app.MapGet("/publicaciones/{id}/usuario", ObtenerUsuarioPublicacion);
        app.MapGet("/publicaciones/{id}/comentarios", ObtenerComentariosPublicacion);
    }
    private static async Task<IResult> ObtenerComentariosPublicacion(IPublicacionRepository publicacionRepository, int id)
    {
        try
        {
            var publicacionConComentarios = await publicacionRepository.GetPublicacionConComentarios(id);
            if (publicacionConComentarios == null)
                  return Results.NotFound();

            return Results.Ok(publicacionConComentarios);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> ObtenerUsuarioPublicacion(IPublicacionRepository publicacionRepository, int id)
    {
        try
        {
            var usuarioPublicacion = await publicacionRepository.GetUsuarioPublicacion(id);
            if (usuarioPublicacion == null)
                  return Results.NotFound();

            return Results.Ok(usuarioPublicacion);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> ObtenerPublicaciones(IPublicacionRepository publicacionRepository)
    {
        try
        {
            return Results.Ok(await publicacionRepository.GetPublicaciones());
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> ObtenerPublicacion(IPublicacionRepository publicacionRepository, int Id)
    {
        try
        {
            var publicacion = await publicacionRepository.GetPublicacion(Id);
            if (publicacion==null)
            {
                return Results.NotFound();
            }
            return Results.Ok(publicacion);
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> AgregarPublicacion(IPublicacionRepository publicacionRepository,
        AddUpdatePublicacionDto publicacionDto)
    {
        try
        {
            var publicacionBd = await publicacionRepository.CreatePublicacion(publicacionDto);
            return Results.Created($"/publicaciones/{publicacionBd.Id}", publicacionBd);
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> ActualizarPublicacion(IPublicacionRepository publicacionRepository,
        int Id, AddUpdatePublicacionDto publicacionDto)
    {
        try
        {
            var publicacionBd = await publicacionRepository.GetPublicacion(Id);
            if (publicacionBd==null)
                return Results.NotFound();

            await publicacionRepository.UpdatePublicacion(Id, publicacionDto);
            return Results.NoContent();
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> EliminarPublicacion(IPublicacionRepository publicacionRepository,
        int Id)
    {
        try
        {
            var publicacionBd = await publicacionRepository.GetPublicacion(Id);
            if (publicacionBd==null)
                return Results.NotFound();

            await publicacionRepository.DeletePublicacion(Id);
            return Results.NoContent();
        }
        catch (System.Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
