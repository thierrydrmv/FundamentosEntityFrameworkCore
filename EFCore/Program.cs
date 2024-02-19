using EFCore.Domain;
using EFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new Data.ApplicationContext();

            //var existe = db.Database.GetPendingMigrations().Any();
            //if(existe)
            //{
            //Console.WriteLine("Pending Migrations!");
            //} else {
            //    Console.WriteLine("All Migrations are up to date");
            //}
            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            ConsultarPedidoCarregamentoAdiantado();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ETipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            using var db = new Data.ApplicationContext();
            // 4 maneiras de add o item(utilizar as 2 primeiras)
            // Ela rastreia a instancia do objeto por isso não multiplica
            // mesmo que sejam utilizados todos os métodos juntos
            db.Produtos.Add(produto);
            // Add generico
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            //db.Add(produto);
            var registros = db.SaveChanges();
            Console.WriteLine($"Total de Registros: {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ETipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Rafael Almeida",
                CEP = "99999000",
                Cidade = "Curitiba",
                Estado = "PR",
                Telefone = "99000001234"
            };

            var listaClientes = new[] {
                 new Cliente
                {
                    Nome = "Rafael Almeida",
                    CEP = "99999000",
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Telefone = "99000001234"
                },
                  new Cliente
                {
                    Nome = "Roberto jr",
                    CEP = "99999000",
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Telefone = "99000001234"
                }
            };

            using var db = new Data.ApplicationContext();

            //db.AddRange(produto, cliente);
            db.Clientes.AddRange(listaClientes);
            //db.AddRange(listaClientes);
            //db.Set<Cliente>().AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total de Registros: {registros}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
            foreach(var cliente in consultaPorMetodo)
            {
                // find busca primeiro em memória (poupa dados)
                Console.WriteLine(cliente.Id);
                db.Clientes.Find(cliente.Id);
                //db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }
        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                StatusPedido = EStatusPedido.Analise,
                TipoFrete = ETipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
    }
}
