namespace Adres.Models
{
    public class Adquisicion
    {
        public int Id { get; set; }
        public double Presupuesto { get; set; }
        public int UnidadId { get; set; }
        public int BienId { get; set; }
        public int Cantidad { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Fecha { get; set; }
        public int ProveedorId { get; set; }
        public string Documentacion { get; set; }
        public virtual Parametrica Unidades { get; set; }
        public virtual Parametrica Bienes { get; set; }
        public virtual Parametrica Proveedores { get; set; }
        
    }
    
}