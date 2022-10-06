using System;
using System.Collections.Generic;
using System.Text;

namespace UNO_Game
{
    class UNOController : IController
    {
        public List<Player> Players { get; private set; }

        public Deck deck { get; private set; }

        public Card VisibleCard;

        private int CardPickupLimit;

        public UNOController(Deck D, int cpl)
        {
            this.deck = D;
            CardPickupLimit = cpl;

            Players = new List<Player>();

            Players.Add(new Player("Johan"));
            Players.Add(new Player("Jakob"));
            Players.Add(new Player("Lova"));
            Players.Add(new Player("Lukas"));
            Players.Add(new Player("Felix"));



            deck = new Deck();
        }


        public void PlayOneRound()
        {
            for (int i = 0; i < 7; i++)
            {
                foreach (Player P in Players)
                {
                    P.CardsOnHand.Add(deck.DealCard());
                }
            }
            VisibleCard = deck.DealCard();

            foreach (Player P in Players)
            {
                bool isActivePlayerTurn = true;


                int currentIndex = 0;
                int pickupCounter = 0;

                while (isActivePlayerTurn)
                {
                    Console.Clear();
                    PrintVisibleCard();
                    
                    PrintController(currentIndex, P);

                    Console.WriteLine($"YOUR TURN: {P}");
                    var x = Console.ReadKey();

                    

                    switch (x.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if( currentIndex == 0)
                            {
                                currentIndex = P.CardsOnHand.Count - 1;
                            }
                            else if (currentIndex >= 0)
                                currentIndex--;
                                
                            break;
                        case ConsoleKey.DownArrow:
                            if(currentIndex == P.CardsOnHand.Count - 1)
                            {
                                currentIndex = 0;
                            }
                            else if (P.CardsOnHand.Count > currentIndex )
                                currentIndex++;
                            break;
                        case ConsoleKey.X:
                            if(ifLegalMakeMove(currentIndex, P))
                            {
                                VisibleCard = P.CardsOnHand[currentIndex];
                                P.PlayCard(P.CardsOnHand[currentIndex]);
                                isActivePlayerTurn = false;
                            }
                            break;
                        case ConsoleKey.P:

                            if(pickupCounter < CardPickupLimit)
                            P.CardsOnHand.Add(deck.DealCard());
                            pickupCounter++;

                            if (pickupCounter > CardPickupLimit )
                                isActivePlayerTurn = false;
                            
                            break;

                    }
                    

                }


                Console.WriteLine(VisibleCard);
            }
            foreach (Player P in Players)
            {
                foreach (Card C in P.CardsOnHand)
                {
                    Console.WriteLine($"{P}: {C}");
                }
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
