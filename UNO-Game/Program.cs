using System;

namespace UNO_Game
{
    class Program
    {
        static void Main(string[] args)
        {

            Deck deck = new Deck();


            UNOController uc = new UNOController(deck,2 );
            uc.deck.CreateDeck();
            uc.deck.ShuffleCards();

            uc.PlayOneRound();

            //foreach(Card c in deck.Cards)
            //{

            //    Console.WriteLine(c); ;
            //}
            Console.ReadLine();



            
        }
        
        static void PrintCard(Card c)
        {
            Console.WriteLine(
                $"---------------\n" +
                $"|             |\n" +
                $"|             |\n" +
                $"|             |\n" +
                $"|             |\n" +
                $"|{c}       |\n" +
                $"|             |\n" +
                $"|             |\n" +
                $"|             |\n" +
                $"---------------\n"


                );
        }

    }
}
