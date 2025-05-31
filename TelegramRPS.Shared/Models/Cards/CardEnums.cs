﻿namespace TelegramRPS.Shared.Models.Cards;

public class CardEnums
{
    public enum Suit
    {
        Clubs,      // ♣️
        Diamonds,   // ♦️
        Hearts,     // ♥️
        Spades      // ♠️
    }

    public enum Rank
    {
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }
}
