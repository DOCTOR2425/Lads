namespace ConsoleApp1
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Comparable comparable1 = new Comparable(1);
			Comparable comparable2 = new Comparable(2);
			Comparable comparable3 = new Comparable(3);
			Comparable comparable12 = new Comparable(1);

			List<Comparable> comparables = new List<Comparable>
			{
				comparable2,
				comparable1,
				comparable3
			};
			Console.WriteLine("CompareTo, Compare");
			Console.WriteLine(comparable1.CompareTo(comparable2));
			Console.WriteLine(comparable1.Compare(comparable3, comparable2));

			Console.WriteLine("\nEquals");
			Console.WriteLine(comparable1.Equals(comparable2));
			Console.WriteLine(comparable1.Equals(new Comparable(1)));

			View(comparables);
			comparables.Sort();
			View(comparables);

			Console.WriteLine("\nComparison");
			Console.WriteLine(comparable1 == comparable12);
			Console.WriteLine(comparable1 == comparable2);
		}

		public static void View(List<Comparable> comparables)
		{
			Console.Write("\nArray\n| ");
			foreach (Comparable comparable in comparables)
				Console.Write(comparable.number + " | ");
			Console.WriteLine();
		}

		public class Comparable : IComparable<Comparable>, IComparer<Comparable>, IEquatable<Comparable>
		{
			public int number;

			public Comparable(int number)
			{
				this.number = number;
			}

			public int Compare(Comparable? x, Comparable? y)
			{
				if (x.number < y.number)
					return -1;
				if (x.number > y.number)
					return 1;
				return 0;
			}

			public int CompareTo(Comparable? obj)
			{
				if (number < obj.number)
					return -1;
				if (number > obj.number)
					return 1;
				return 0;
			}

			public bool Equals(Comparable? other)
			{
				if (number == other.number)
					return true;
				return false;
			}

			public static bool operator ==(Comparable a, Comparable b)
			{
				return a.Equals(b);
			}

			public static bool operator !=(Comparable a, Comparable b)
			{
				return !a.Equals(b);
			}
		}
	}
}
