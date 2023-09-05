using System;
using System.Collections.Generic;

namespace Twitter
{
    public interface IObserver
    {
        void Update(string message);
    }

    public interface ISubject
    {
        void Adicionar(IObserver observer);
        void Remover(IObserver observer);
        void Notificar(string message);
    }

    public class UsuarioTwitter : ISubject, IObserver
    {
        private List<IObserver> seguidores = new List<IObserver>();
        public string NomeDeUsuario { get; }

        public UsuarioTwitter(string nomeDeUsuario)
        {
            NomeDeUsuario = nomeDeUsuario;
        }

        public void Seguir(IObserver seguidor)
        {
            seguidores.Add(seguidor);
        }

        public void DeixarDeSeguir(IObserver seguidor)
        {
            seguidores.Remove(seguidor);
        }

        public void MandarTweet(string tweet)
        {
            Console.WriteLine($"@{NomeDeUsuario} tweetou: {tweet}");
            NotificarSeguidores($"@{NomeDeUsuario} tweetou: {tweet}");
        }

        private void NotificarSeguidores(string message)
        {
            foreach (var follower in seguidores)
            {
                follower.Update(message);
            }
        }

        public void Adicionar(IObserver observer)
        {
            seguidores.Add(observer);
        }

        public void Remover(IObserver observer)
        {
            seguidores.Remove(observer);
        }

        public void Notificar(string message)
        {
            foreach (var follower in seguidores)
            {
                follower.Update(message);
            }
        }

        public void Update(string message)
        {
            Console.WriteLine($"@{NomeDeUsuario} recebeu atualização: {message}");
        }
    }

    public class UsuarioYahoo : IObserver
    {
        public string NomeDeUsuario { get; }

        public UsuarioYahoo(string nomeDeUsuario)
        {
            NomeDeUsuario = nomeDeUsuario;
        }

        public void Update(string message)
        {
            Console.WriteLine($"{NomeDeUsuario} recebeu atualização do Twitter através do Yahoo: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UsuarioTwitter usuario1 = new UsuarioTwitter("gabriel_zionn");
            UsuarioTwitter usuario2 = new UsuarioTwitter("didialmeida7");

            UsuarioYahoo usuarioYahoo1 = new UsuarioYahoo("Branquinho-No-Yahoo");
            UsuarioYahoo usuarioYahoo2 = new UsuarioYahoo("Sobrino-No-Yahoo");

            usuario1.Seguir(usuarioYahoo1);
            usuario2.Seguir(usuarioYahoo2);

            Console.ReadKey();
            usuario1.MandarTweet("'Salve @didialmeida7'");
            Console.ReadKey();
            Console.WriteLine();

            usuario2.MandarTweet("'Salve @gabriel_zionn'");
            Console.WriteLine();

            Console.ReadKey();
            usuario1.Seguir(usuario2);
            Console.ReadKey();// UsuarioTwitter seguindo outro UsuarioTwitter
            usuario2.Seguir(usuario1);
            Console.ReadKey();// UsuarioTwitter seguindo outro UsuarioTwitter

            usuarioYahoo1.Update("'Mensagem do Yahoo para os seguidores do gabriel_zionn'");
            Console.ReadKey();
            usuarioYahoo2.Update("'Mensagem do Yahoo para os seguidores do didialmeida7'");

            Console.ReadKey();
        }
    }
}