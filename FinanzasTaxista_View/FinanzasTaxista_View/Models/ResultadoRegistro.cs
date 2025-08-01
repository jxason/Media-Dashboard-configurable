namespace FinanzasTaxista_View.Models
{
    //estoy usando este modelo para ver el flujo completo de los errores en el registro, lo separe todo para ver el error exacto al que nos enfrentamos xd
    public class ResultadoRegistro
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
    }
}
