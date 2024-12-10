using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T>
{
    private List<T> _items;

    public Repository()
    {
        _items = new List<T>();
    }

    // Method to add an item to the repository
    public void Add(T item)
    {
        _items.Add(item);
    }

    // Delegate for defining the criteria
    public delegate bool Criteria<T>(T item);

    // Method to find items matching the criteria
    public List<T> Find(Criteria<T> criteria)
    {
        return _items.Where(item => criteria(item)).ToList();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose the type of repository:");
        Console.WriteLine("1. Integer");
        Console.WriteLine("2. String");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Integer repository
                Repository<int> intRepo = new Repository<int>();
                Console.WriteLine("Enter integers to add to the repository. Type 'done' to finish:");

                while (true)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "done")
                        break;

                    if (int.TryParse(input, out int number))
                    {
                        intRepo.Add(number);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter an integer.");
                    }
                }

                Console.WriteLine("Enter a criterion to find even numbers (type 'even') or numbers greater than a value (type 'greater than X'):");

                string criterion = Console.ReadLine();
                List<int> results;

                if (criterion.ToLower() == "even")
                {
                    results = intRepo.Find(item => item % 2 == 0);
                }
                else if (criterion.StartsWith("greater than"))
                {
                    if (int.TryParse(criterion.Split(' ')[2], out int threshold))
                    {
                        results = intRepo.Find(item => item > threshold);
                    }
                    else
                    {
                        Console.WriteLine("Invalid threshold value.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid criterion.");
                    return;
                }

                Console.WriteLine("Matching results:");
                results.ForEach(Console.WriteLine);
                break;

            case "2":
                // String repository
                Repository<string> stringRepo = new Repository<string>();
                Console.WriteLine("Enter strings to add to the repository. Type 'done' to finish:");

                while (true)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "done")
                        break;

                    stringRepo.Add(input);
                }

                Console.WriteLine("Enter a criterion to find strings starting with a specific letter (type 'starts with X'):");

                string strCriterion = Console.ReadLine();
                List<string> strResults;

                if (strCriterion.StartsWith("starts with"))
                {
                    string prefix = strCriterion.Split(' ')[2];
                    strResults = stringRepo.Find(item => item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    Console.WriteLine("Invalid criterion.");
                    return;
                }

                Console.WriteLine("Matching results:");
                strResults.ForEach(Console.WriteLine);
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}
