using Microsoft.AspNetCore.Mvc;
using aula14.Models;
using System.Text.Json;

namespace aula14.Controllers;

public class HomeController : Controller
{
    private string DADOS = "./Dados/dados.json";

    public IActionResult Index()
    {
        List<Produto> produtos = CarregarProdutos();
        return View(produtos);
    }

    [HttpPost]
    public IActionResult Criar(string nome, string quantidade, string prioridade)
    {
        List<Produto> produtos = CarregarProdutos();

        Produto novoProduto = new Produto(
            int.Parse(quantidade),
            nome,
            Enum.Parse<Prioridade>(prioridade)
        );

        produtos.Add(novoProduto);

        SalvarProdutos(produtos);

        return RedirectToAction("Index");
    }

    public IActionResult Excluir(string id)
    {
        List<Produto> produtos = CarregarProdutos();

        var produto = produtos.FirstOrDefault(p => p.Id == id);

        if (produto != null)
        {
            produtos.Remove(produto);
            SalvarProdutos(produtos);
        }

        return RedirectToAction("Index");
    }

    private List<Produto> CarregarProdutos()
    {
        if (!System.IO.File.Exists(DADOS))
            return new List<Produto>();

        string json = System.IO.File.ReadAllText(DADOS);

        if (string.IsNullOrEmpty(json))
            return new List<Produto>();

        var lista = JsonSerializer.Deserialize<List<Produto>>(json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return lista ?? new List<Produto>();
    }

    private void SalvarProdutos(List<Produto> produtos)
    {
        string json = JsonSerializer.Serialize(produtos);

        System.IO.File.WriteAllText(DADOS, json);
    }
}