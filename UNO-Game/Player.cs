﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UNO_Game
{
    class Player
    {
        public List<Card> CardsOnHand;
        public string Name;

        public Player(string name)
        {
            this.Name = name;
            CardsOnHand = new List<Card>();

        }

        public bool PlayCard(Card c)
        {
            if (CardsOnHand.Contains(c))
            {
                CardsOnHand.Remove(c);
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}