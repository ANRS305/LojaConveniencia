namespace aula14.Models;

public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Nome { get; set; }

    public int Quantidade { get; set; }

    public Prioridade NivelPrioridade { get; set; }

    public DateTime DataCriacao { get; set; }

    public Produto(){}

    public Produto(int quantidade, string nome, Prioridade prioridade)
    {
        Nome = nome;
        Quantidade = quantidade;
        NivelPrioridade = prioridade;
        DataCriacao = DateTime.Now;
    }
}