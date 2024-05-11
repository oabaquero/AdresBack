namespace Adres.DTOs
{
    public class AdquisicionDTO
    {
        public int Id { get; set; }
        public double Presupuesto { get; set; }
        public int UnidadId { get; set; }
        public string NombreUnidad { get; set; }
        public int BienId { get; set; }
        public string NombreBien { get; set; }
        public int Cantidad { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public string Fecha { get; set; }
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string Documentacion { get; set; }
    }
}