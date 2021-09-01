using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AluraTunes
{
    class Program
    {
        static void Main(string[] args)
        {
            //listar os gêneros rock
            //ListarGeneroUsandoForeachComIf();
            //ListarGeneroUsandoLinq();

            //listar músicas
            //ListarMusicaUsandoJoinLinq();

            //Listar músicas em arquivo XML
            ListarMusicaUsandoXMLLinq();
            
            
            
            Console.ReadKey();
        }

        protected static void ListarMusicaUsandoXMLLinq()
        {
            string _path = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("\\bin\\Debug\\net5.0", "");
            string path = Path.GetFullPath("./Data/AluraTunes.xml", _path);

            XElement root = XElement.Load(path);
            var queryXML = from g in root.Element("Generos").Elements("Genero")
                           select g;

            foreach (var genero in queryXML)
            {
                Console.WriteLine($"{genero.Element("GeneroId").Value}\t{genero.Element("Nome").Value}");
            }

            Console.WriteLine();
            var queryXMLJoin = from g in root.Element("Generos").Elements("Genero")
                           join m in root.Element("Musicas").Elements("Musica")
                           on g.Element("GeneroId").Value equals m.Element("GeneroId").Value
                           select new {
                               Id = m.Element("MusicaId").Value, 
                               Musica = m.Element("Nome").Value,
                               Genero = g.Element("Nome").Value
                           };

            foreach (var genero in queryXMLJoin)
            {
                Console.WriteLine($"{genero.Id}\t{genero.Musica}\t{genero.Genero}");
            }

            Console.WriteLine();
        }

        protected static void ListarMusicaUsandoJoinLinq()
        {
            List<Genero> generos = PopulaLista();
            List<Musica> musicas = PopulaMusica();

            var query = from m in musicas
                        join g in generos on m.GeneroId equals g.Id
                        select new {m, g };

            foreach (var musica in query)
            {
                Console.WriteLine($"{musica.m.Id}\t{musica.m.Nome}\t{musica.g.Nome}"); 
            }
            Console.WriteLine();
        }

        protected static void ListarGeneroUsandoLinq()
        {
            List<Genero> generos = PopulaLista();
            var query = from g in generos
                        where g.Nome.Contains("Rock")
                        select g;

            foreach (var genero in query)
            {
                Console.WriteLine($"{genero.Id}\t{genero.Nome}");
            }

            Console.WriteLine();
        }

        protected static void ListarGeneroUsandoForeachComIf()
        {
            List<Genero> generos = PopulaLista();

            foreach (var genero in generos)
            {
                if (genero.Nome.Contains("Rock"))
                {
                    Console.WriteLine($"{genero.Id}\t{genero.Nome}");
                }
            }

            Console.WriteLine();
        }

        protected static List<Musica> PopulaMusica()
        {
            List<Musica> musica = new List<Musica>
            {
                new Musica { Id = 1, Nome = "Sweet Child O´Mine", GeneroId = 1},
                new Musica { Id = 2, Nome = "I Shot The Sheriff", GeneroId = 2},
                new Musica { Id = 3, Nome = "Danúbio Azul", GeneroId = 5}

            };

            return musica;
        }

        protected static List<Genero> PopulaLista()
        {
            List<Genero> generos = new List<Genero>
            {
                new Genero {Id = 1, Nome = "Rock"},
                new Genero {Id = 2, Nome = "Reggae"},
                new Genero {Id = 3, Nome = "Rock Progressivo"},
                new Genero {Id = 4, Nome = "Punk Rock"},
                new Genero {Id = 5, Nome = "clássica"}
            };

            return generos;
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
    }
}
