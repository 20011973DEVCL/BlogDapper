namespace BlogDapperNew.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }  
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Publicacion> Publicaciones { get; set; }  = new HashSet<Publicacion>();
    }
}



