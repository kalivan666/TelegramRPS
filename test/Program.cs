// See https://aka.ms/new-console-template for more information


var test = new Test();
test.test();

public class Test
{
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

    public void test()
    {
        var a = (int)Rank.Seven;
        var b = Rank.Eight;

    }
}