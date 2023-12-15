namespace BlogDapperNew.Entities
{
    public class Comentario
        {
            public int Id { get; set; } 
            public string Asunto { get; set; }  
            public string Email { get; set; }
            public string Contenido { get; set; }
            public bool Aprobado { get; set; } 
            public bool EsVisible =>Aprobado == true ? true : false;
        }
}



