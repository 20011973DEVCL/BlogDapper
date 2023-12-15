namespace BlogDapperNew.Dto
{
    public class AddUpdatePublicacionDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Contenido { get; set; }
        public string Etiquetas { get; set; }
        public int UsuarioId { get; set; }
    }
}