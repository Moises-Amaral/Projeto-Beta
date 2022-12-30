namespace ProjetoBeta1.Models
{
    public class ListaDeCompras
    {
        public Guid Código { get; set; }
        public String? Produto { get; set; }
        public int Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
