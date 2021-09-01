using AluraTunes.NetFrameWork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AluraTunes.NetFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var contexto = new AluraTunesEntities()){
                //Listar Genero
                //ListarGeneroComLinq(contexto);

                //Listar Todas as Faixas e Generos
                //ListarTodasFaixaGeneroComLinq(contexto);

                //Listar Todas as Faixas e Generos de forma páginada
                //ListarPaginadoFaixaGeneroComLinq(contexto);

                //Busca artista pelo nome 
                //BuscaArtistaPorNomeComEFLinq(contexto, "Led");
                //BuscaArtistaPorNomeComEFLWehre(contexto, "Led");

                //Busca artista e álbum  pelo nome do artista
                //BuscaAlbumArtistaPorNomeComEFLWehre(contexto, "Led");
                //BuscaAlbumEArtistaPorNomeComEFLinq(contexto, "Led");

                //Buscar Faixa por nome do artista e algum
                //BuscaFaixaPorNomeArtisteENomealbum(contexto, "Led Zeppelin", "Graffiti");
                //BuscaFaixaPorNomeArtisteENomealbumOrdenada(contexto, "Led Zeppelin", "Graffiti");

                //Conatr as faixas
                //ContarFaixa(contexto, "Led Zeppelin");

                //Somar total de venda de um artista
                //SomarTotalDeVendasDeUmArtistaFaixa(contexto, "Led Zeppelin");

                //Totaliza a quantodade d álbuns vendidos dos artistas
                //TotalDeAlbunsVendidosPorArtistaFaixa(contexto, "Led Zeppelin");

                //Calculando o valor da maior, menor e a média de vendas
                //CalculandoVendas(contexto);

                //Cálculando a médiana das Vendas
                CalculandoAMedianaDasVendas(contexto);
            }

            Console.ReadKey();
        }

        private static void CalculandoAMedianaDasVendas(AluraTunesEntities contexto)
        {
            var vendaMedia = contexto.NotaFiscais.Average(nf => nf.Total);
            Console.WriteLine($"A venda média é {vendaMedia.ToString("C")}");//5.65

            var query = from nf in contexto.NotaFiscais
                        select nf.Total;

            decimal mediana = Mediana(query);

            Console.WriteLine($"A mediana das vendas é {mediana.ToString("C")}");

            var vendaMediana = contexto.NotaFiscais.Mediana(nf => nf.Total) ;


            Console.WriteLine("Agora cálculado com o método de extensão");

            Console.WriteLine($"A mediana das vendas é {vendaMediana.ToString("C")}");
        }

        private static decimal Mediana(IQueryable<decimal> query)
        {
            var contagem = query.Count();
            var queryOrdenada = query.OrderBy(total => total);
            var elementoCentral1 = queryOrdenada.Skip(contagem / 2).First();
            var elementoCentral2 = queryOrdenada.Skip((contagem-1) / 2).First();

            var mediana = (elementoCentral1 + elementoCentral2) /2;
            return mediana;
        }

        private static void CalculandoVendas(AluraTunesEntities contexto)
        {
            //contexto.Database.Log = Console.WriteLine;
            var maiorVendas = contexto.NotaFiscais.Max(nf => nf.Total);
            var menorVendas = contexto.NotaFiscais.Min(nf => nf.Total);
            var mediaVendas = contexto.NotaFiscais.Average(nf => nf.Total);

            Console.WriteLine($"A maior venda é de {maiorVendas.ToString("C")}" +
                $"\nA menor venda é de {menorVendas.ToString("C")}" +
                $"\nA média das vendas é {mediaVendas.ToString("C")}");
            
            var vendas = (from nf in contexto.NotaFiscais
                        group nf by 1 into agrupado
                        select new { 
                            Maior = agrupado.Max(nf => nf.Total),
                            Menor = agrupado.Min(nf => nf.Total),
                            Media = agrupado.Average(nf => nf.Total)

                        }).Single();


            Console.WriteLine("\n================== OU ===========================\n");

            Console.WriteLine($"A maior venda é de {vendas.Maior.ToString("C")}" +
                            $"\nA menor venda é de {vendas.Menor.ToString("C")}" +
                            $"\nA média das vendas é {vendas.Media.ToString("C")}");

        }

        private static void TotalDeAlbunsVendidosPorArtistaFaixa(AluraTunesEntities contexo, string nome)
        {
            var query = from inf in contexo.ItemNotaFiscals
                        where inf.Faixa.Album.Artista.Nome == nome
                        group inf by inf.Faixa.Album into agrupado
                        let vendasPoralbum = agrupado.Sum(a => a.Quantidade * a.PrecoUnitario)
                        orderby vendasPoralbum descending
                        select new { 
                            TituloDoAlbum = agrupado.Key.Titulo,
                            TotalPorAlbum = vendasPoralbum
                        };

            foreach (var agrupado in query)
            {
                Console.WriteLine($"" +
                    $"{agrupado.TituloDoAlbum.PadRight(40)} " +
                    $"{agrupado.TotalPorAlbum}");
            }
        }

        private static void SomarTotalDeVendasDeUmArtistaFaixa(AluraTunesEntities contexo, string nome)
        {
            var query = from inf in contexo.ItemNotaFiscals
                        where inf.Faixa.Album.Artista.Nome == nome
                        select new { totalDoItem = inf.Quantidade * inf.PrecoUnitario};

            var totalDoArtista = query.Sum(q => q.totalDoItem);


            Console.WriteLine($"Total de vendas {totalDoArtista.ToString("C")} do artista {nome} .");
        }

        private static void ContarFaixa(AluraTunesEntities contexo, string nome)
        {
            var query = from f in contexo.Faixas
                        where f.Album.Artista.Nome == nome
                        select f;

            //var quantidade = query.Count();
            var quantidade = contexo.Faixas
                .Count(f => f.Album.Artista.Nome == nome);

            Console.WriteLine($"O artista {nome}, tem {quantidade} de músicas no banco de dados\n");

            foreach (var faixa in query)
            {
                Console.WriteLine($"Nome: {faixa.Album.Artista.Nome.PadRight(10)} Música: {faixa.Nome.PadRight(25)}");
            }
            
            Console.WriteLine($"\nO artista {nome}, tem {quantidade} de músicas no banco de dados\n");
        }

        private static void BuscaFaixaPorNomeArtisteENomealbumOrdenada(AluraTunesEntities contexto, string nome, string album)
        {
            var query = from f in contexto.Faixas
                        where f.Album.Artista.Nome.Contains(nome)
                        && (!string.IsNullOrEmpty(album) ? f.Album.Titulo.Contains(album) : true) // esta linha substitui o If
                        orderby f.Album.Titulo descending, f.Nome //Esta linha substitui a query com order by
                        select f;

            //if (!string.IsNullOrEmpty(album))
            //{
            //    query = query.Where(a => a.Album.Titulo.Contains(album));
            //}

            //query = query.OrderBy(q => q.Album.Titulo).ThenByDescending(q => q.Nome);

            foreach (var artista in query)
            {
                Console.WriteLine($"Id: {artista.Album.ArtistaId.ToString().PadRight(5)} Nome: {artista.Album.Artista.Nome.PadRight(15)} Música: {artista.Nome.PadRight(25)} Álbum: {artista.Album.Titulo}");
            }

            Console.WriteLine();
        }

        private static void BuscaFaixaPorNomeArtisteENomealbum(AluraTunesEntities contexto, string nome, string album)
        {
            var query = from f in contexto.Faixas
                        where f.Album.Artista.Nome.Contains(nome)
                        select f;

            if (!string.IsNullOrEmpty(album))
            {
                query = query.Where(a => a.Album.Titulo.Contains(album));
            }

            foreach (var artista in query)
            {
                Console.WriteLine($"Id: {artista.Album.ArtistaId.ToString().PadRight(5)} Nome: {artista.Album.Artista.Nome.PadRight(15)} Música: {artista.Nome.PadRight(25)} Álbum: {artista.Album.Titulo}");
            }

            Console.WriteLine();
        }


        private static void BuscaAlbumEArtistaPorNomeComEFLinq(AluraTunesEntities contexto, string nome)
        {
            string nomeArtista = nome;

            var query = contexto.Albums.Where(a => a.Artista.Nome.Contains(nomeArtista));

            foreach (var artista in query)
            {
                Console.WriteLine($"Id: {artista.ArtistaId}, Nome: {artista.Artista.Nome}, Álbum: {artista.Titulo}");
            }

            Console.WriteLine();

            //Ou assim

            var query2 = from alb in contexto.Albums
                         where alb.Artista.Nome.Contains(nomeArtista)
                         select new
                         {
                             Id = alb.ArtistaId, 
                             Nome = alb.Artista.Nome,
                             Album = alb.Titulo
                         };

            foreach (var item in query2)
            {
                Console.WriteLine($"Id: {item.Id}, Nome: {item.Nome}, Álbum: {item.Album}");
            }

            Console.WriteLine();

        }

        private static void BuscaAlbumArtistaPorNomeComEFLWehre(AluraTunesEntities contexto, string nome)
        {
            string nomeArtista = nome;

            var query = from a in contexto.Artistas
                        join alb in contexto.Albums
                        on a.ArtistaId equals alb.ArtistaId
                        where a.Nome.Contains(nomeArtista)
                        select new { 
                            Id = a.ArtistaId,
                            Nome = a.Nome,
                            Album = alb.Titulo
                        };

            foreach (var item in query)
            {
                Console.WriteLine($"Id: {item.Id}, Nome: {item.Nome}, Álbum: {item.Album}");
            }

            Console.WriteLine();
        }

        private static void BuscaArtistaPorNomeComEFLWehre(AluraTunesEntities contexto, string nome)
        {
            string nomeArtista = nome;

            var query = contexto.Artistas.Where( a => a.Nome.Contains(nomeArtista));

            foreach (var artista in query)
            {
                Console.WriteLine($"Id: {artista.ArtistaId}, Nome: {artista.Nome}");
            }

            Console.WriteLine();
        }

        private static void BuscaArtistaPorNomeComEFLinq(AluraTunesEntities contexto, string nome)
        {
            string nomeArtista = nome;

            var query = from a in contexto.Artistas
                        where a.Nome.Contains(nomeArtista)
                        select a;

            foreach (var artista in query)
            {
                Console.WriteLine($"Id: {artista.ArtistaId}, Nome: {artista.Nome}");
            }

            Console.WriteLine();
        }

        private static void ListarPaginadoFaixaGeneroComLinq(AluraTunesEntities contexto)
        {
            //definição de uma Consulta
            var query = from g in contexto.Generos
                        join f in contexto.Faixas
                        on g.GeneroId equals f.GeneroId
                        select new { f, g };

            query = query.Take(10);

            contexto.Database.Log = Console.WriteLine;

            //imprime no console
            foreach (var item in query)
            {
                Console.WriteLine($"Id: {item.f.FaixaId}, Nome: {item.f.Nome}, Genero: {item.g.Nome}");
            }
        }

        private static void ListarTodasFaixaGeneroComLinq(AluraTunesEntities contexto)
        {
            //definição de uma Consulta
            var query = from g in contexto.Generos
                        join f in contexto.Faixas 
                        on g.GeneroId equals f.GeneroId 
                        select new {f, g };

            //imprime no console

            foreach (var item in query)
            {
                Console.WriteLine($"Id: {item.f.FaixaId}, Nome: {item.f.Nome}, Genero: {item.g.Nome}");
            }
        }

        private static void ListarGeneroComLinq(AluraTunesEntities contexto)
        {
            //definição de uma Consulta
            var query = from g in contexto.Generos
                        select g;

            //imprime no console

            foreach (var genero in query)
            {
                Console.WriteLine($"Id: {genero.GeneroId} | Genero: {genero.Nome}");
            }
        }
    }

    static class LinqExtension
    {
        public static decimal Mediana<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector) {
            var contagem = source.Count();

            var funcSelector = selector.Compile();

            var queryOrdenada = source.Select(funcSelector).OrderBy(total => total);
            var elementoCentral1 = queryOrdenada.Skip(contagem / 2).First();
            var elementoCentral2 = queryOrdenada.Skip((contagem - 1) / 2).First();

            var mediana = (elementoCentral1 + elementoCentral2) / 2;
            return mediana;
        }
    }
}
