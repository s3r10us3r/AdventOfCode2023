int t = 1000;
List<Hand> hands = [];

while(t-- > 0)
{
    string[] input = Console.ReadLine().Split(" ");
    string handString = input[0];
    int bid = Convert.ToInt32(input[1]);
    hands.Add(new Hand(handString, bid));
}

hands = hands.OrderBy(h => h, new HandComparer()).ToList();

long result = 0;

for(int i = 0; i < hands.Count ; i++)
{
    result += hands[i].bid * (i+1);
}
Console.WriteLine(result);