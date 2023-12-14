namespace BlogDapper.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int AreaId { get; set; } 

        public Area Area { get; set; }
        public ICollection<Usuario> Usuario { get; set; }  = new HashSet<Usuario>();
    }
}



