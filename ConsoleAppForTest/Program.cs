using MT.Extensions;
namespace ConsoleAppForTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var now = DateTime.Now;
			var (s, e) = now.GetPreviousWeekRange();
			Console.WriteLine($" {now} => {s} - {e}");
		}
	}
}
