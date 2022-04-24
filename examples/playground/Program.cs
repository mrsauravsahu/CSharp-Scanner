using mrsauravsahu.scanner;

var scanner = new Scanner();

Console.WriteLine($"Enter some numbers. (Example: \"1 2 3\")");

var value = scanner.ReadNext<int>();

Console.WriteLine($"Next oncoming int is - {value}");
Console.WriteLine($"Type - {value.GetType()}");
