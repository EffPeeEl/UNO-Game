using System;
using System.Collections.Generic;
using System.Text;

namespace UNO_Game
{
    class Deck
    {
        private List<Card> Cards;
        Random rng;

        

        public Card DealCard()
        {
            Card C = Cards[0];
            Cards.RemoveAt(0);
            return C;
        }

        public void CreateDeck()
        {
            Cards = new List<Card>();

            for (int x = 0; x < 2; x++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        

                        Card c = new Card((ValueType)j, (ColorType)i);
                        Cards.Add(c);

                    }
                }
            
            }

            for (int i = 10; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Cards.Add(new Card((ValueType)i, (ColorType)j));

                    }

                }
            }

            for(int i = 13; i < 15; i ++)
            {
                for(int j = 0; j < 4; j ++)
                {
                    Cards.Add(new Card((ValueType)i, (ColorType)4));
                }
            }

        }
        public void ShuffleCards()
        {
            rng = new Random();
            int i = Cards.Count;
            while (i > 1)
            {
                i--;
                int j = rng.Next(i + 1);
                Card value = Cards[j];
                Cards[j] = Cards[i];
                Cards[i] = value;
            }
        }

        
    }
}
