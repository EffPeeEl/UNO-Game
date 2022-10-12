using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNO_Game
{
    class UNOController : IController
    {
        public List<Player> Players { get; private set; }

        public Deck deck { get; private set; }

        public Card VisibleCard { get; private set; }

        public int StartingCardsOnHand { get; private set; }

        public int CardPickupLimit { get; private set; }

        public UNOController(Deck D, int cpl, int cardAmount)
        {
            this.deck = D;
            this.CardPickupLimit = cpl;
            this.StartingCardsOnHand = cardAmount;

            Players = new List<Player>();

            Players.Add(new Player("Johan"));
            Players.Add(new Player("Jakob"));
            Players.Add(new Player("Lova"));
            Players.Add(new Player("Lukas"));
            Players.Add(new Player("Felix"));



            deck = D;
        }

        public void DealCards(int cardAmount)
        {
            for (int i = 0; i < cardAmount; i++)
            {
                foreach (Player P in Players)
                {
                    P.CardsOnHand.Add(deck.DealCard());
                }
            }
            Players.Add(new Player("Håkan"));
            Players[Players.Count - 1].CardsOnHand.Add(new Card(ValueType.Stop, ColorType.Blue));

            Players[Players.Count - 1].CardsOnHand.Add(new Card(ValueType.Stop, ColorType.Blue));
        }


        public void PlayOneRound()
        {
            DealCards(StartingCardsOnHand);
            VisibleCard = deck.DealCard();
            int direction = 1;
            bool isGameOn = true;
            while(isGameOn)
            {
                //

                

                for (int i = 0; i < this.Players.Count ; i ++)
                {

                    if (isGameOn)
                    {
                        bool isActivePlayerTurn = true;


                        int currentIndex = 0;
                        int pickupCounter = 0;

                        bool isUno = false;
                        bool hasPlayerSaidUno = false;

                        if (Players[i].CardsOnHand.Count == 1)
                            isUno = true;

                        while (isActivePlayerTurn)
                        {
                            Console.Clear();
                            PrintVisibleCard();

                            PrintController(currentIndex, Players[i]);

                            Console.WriteLine($"YOUR TURN: {Players[i]}");
                            var x = Console.ReadKey();


                            switch (x.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    if (currentIndex == 0)
                                    {
                                        currentIndex = Players[i].CardsOnHand.Count - 1;
                                    }
                                    else if (currentIndex >= 0)
                                        currentIndex--;

                                    break;
                                case ConsoleKey.DownArrow:
                                    if (currentIndex == Players[i].CardsOnHand.Count - 1)
                                    {
                                        currentIndex = 0;
                                    }
                                    else if (Players[i].CardsOnHand.Count > currentIndex)
                                        currentIndex++;
                                    break;
                                case ConsoleKey.X:
                                    if (ifLegalMakeMove(currentIndex, Players[i]))
                                    {
                                        VisibleCard = Players[i].CardsOnHand[currentIndex];
                                        Players[i].PlayCard(Players[i].CardsOnHand[currentIndex]);
                                        isActivePlayerTurn = false;

                                        if (VisibleCard.Value == ValueType.Reverse)
                                        {
                                            Player p = Players[i];
                                            Players.Reverse();
                                            i = Players.IndexOf(p);
                                        }
                                        if (VisibleCard.Value == ValueType.Plus2)
                                        {
                                            if (i != Players.Count - 1)
                                                i++;
                                            else
                                                i = 0;


                                        }
                                        if (VisibleCard.Value == ValueType.Stop)
                                        {
                                            if (i != Players.Count - 1)
                                                i ++;
                                            else
                                                i = 0;
                                        }

                                        if (isUno)
                                        {
                                            if (hasPlayerSaidUno)
                                            {
                                                PlayerWon(Players[i]);
                                                isGameOn = false;
                                            }
                                        }
                                    }



                                    break;
                                case ConsoleKey.P:
                                    if (pickupCounter < CardPickupLimit)
                                        Players[i].CardsOnHand.Add(deck.DealCard());
                                    pickupCounter++;

                                    if (pickupCounter > CardPickupLimit)
                                        isActivePlayerTurn = false;
                                    break;
                                case ConsoleKey.U:
                                    hasPlayerSaidUno = true;

                                    break;
                            }

                        }
                    }
                }
            }

        }

        private void PlayerWon(Player p)
        {
            CalculatePlayerScore();
            Console.Clear();

            Console.WriteLine($"Player: {p} WON!");

            Console.WriteLine($"\n\nScore for other players are:");



            ///HÄR ANVÄNDER VI LINQ
            int place = 1;
            foreach(var item in Players.OrderBy(p => p.Score))
            {
                
                Console.WriteLine($"[{place}] {item}: {item.Score}");
                place++;
            }
        }

        private void CalculatePlayerScore()
        {
            foreach(Player P in Players)
            {

                int score = 0;

                foreach(Card c in P.CardsOnHand)
                {
                    score += (int)c.Value; 
                    
                }
                P.SetScore(score);

            }
        }

        private bool ifLegalMakeMove(int currentIndex, Player p)
        {
            Card c = p.CardsOnHand[currentIndex];
            if (VisibleCard.Color == c.Color || VisibleCard.Value == c.Value)
            {
                return true;
            }
            


            return false;

        }

        private void PrintController(int index, Player player)
        {
            for (int i = 0; i < player.CardsOnHand.Count; i++)
            {
                if(i == index)
                {
                    Console.WriteLine($"{player.CardsOnHand[i]}       <--");
                }
                else
                    Console.WriteLine(player.CardsOnHand[i]);
            }

            
        }

        public void PrintVisibleCard()
        {
            Console.WriteLine($"Card on top is: {VisibleCard}");
        }
    }
}
