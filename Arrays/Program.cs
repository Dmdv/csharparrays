using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

namespace Arrays
{
	internal static class Program
	{
		private static void Main()
		{
			int[,] ints =
			{
				{2, 4, 4, 1},
				{3, 9, 4, 1},
				{4, 16, 4, 1},
				{5, 25, 4, 1},
				{6, 36, 4, 1},
				{7, 49, 4, 1},
				{8, 64, 4, 1},
				{9, 81, 4, 1}
			};

			var test = new[] {44, 284, 32, 8};

			var sums = Sums(ints);
			var sumsAsync = SumsAsync(ints);

			sums.ShouldAllBeEquivalentTo(test);
			sumsAsync.ShouldAllBeEquivalentTo(test);
		}

		private static IEnumerable<int> SumsAsync(int[,] ints)
		{
			Check2DArray(ints);

			var rows = ints.GetLength(0);
			var columns = ints.GetLength(1);

			var sums = new int[columns];

			Parallel.ForEach(
				Enumerable.Range(0, columns),
				column =>
				{
					var sum = 0;
					for (var i = 0; i < rows; i++)
					{
						sum += ints[i, column];
					}
					sums[column] = sum;
				});

			return sums;
		}

		private static IEnumerable<int> Sums(int[,] ints)
		{
			Check2DArray(ints);

			var rows = ints.GetLength(0);
			var columns = ints.GetLength(1);

			var enumerable = Enumerable.Range(0, columns);
			return enumerable
				.Select(
					column =>
					{
						var sum = 0;
						for (var i = 0; i < rows; i++)
						{
							sum += ints[i, column];
						}
						return sum;
					}
				).ToArray();
		}

		private static void Check2DArray(int[,] ints)
		{
			if (ints == null || ints.Length == 0 || ints.Rank == 0)
			{
				throw new ArgumentException();
			}

			var rows = ints.GetLength(0);
			var columns = ints.GetLength(1);

			if (rows == 0 || columns == 0 || rows > 10000 || columns > 10000)
			{
				throw new ArgumentException();
			}
		}
	}
}