namespace Adres.Models
{
    public class Historico
    {
        public int Id { get; set; }
        public int AdquisicionId { get; set; }
        public string DataAnterior { get; set; }
        public string DataActual { get; set; }
        public string Diferencia { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    
}