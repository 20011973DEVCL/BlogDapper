using BlogDapper.Dto;
using BlogDapper.Interfaces;

namespace BlogDapper;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/publicaciones", ObtenerPublicaciones);
        app.MapGet("/publicaciones/{id}", ObtenerPublicacion);
        app.MapPost("/publicaciones", AgregarPublicacion);
        app.MapPut("/publicaciones/{id}", ActualizarPublicacion);
        app.MapDelete("/publicaciones/{id}", EliminarPublicacion);
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
