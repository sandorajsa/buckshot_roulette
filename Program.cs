using buckshot_roulette;

Console.Write("Üdvözlünk!\nKérlek add meg a neved: ");
string input = Console.ReadLine();
Console.Clear();

Player jatekos = new Player(input, 3);
AI bot = new AI("bot", 3);

Game.Round(jatekos, bot);