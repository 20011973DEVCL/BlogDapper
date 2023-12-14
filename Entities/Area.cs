namespace BlogDapper.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }  
        ICollection<Departamento> Departamentos {get; set;} = new HashSet<Departamento>();
    }
}



