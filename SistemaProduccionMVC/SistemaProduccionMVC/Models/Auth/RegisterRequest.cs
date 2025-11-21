namespace SistemaProduccionMVC.Models.Auth
{
    public class RegisterRequest
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int RolId { get; set; }
    }
}
