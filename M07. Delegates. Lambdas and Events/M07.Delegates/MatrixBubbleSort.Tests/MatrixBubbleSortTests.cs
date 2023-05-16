using NUnit.Framework;
using System.Collections.Generic;

namespace MatrixBubbleSort.Tests
{
    public class MatrixBubbleSortTests
    {
        static private int[,] testArray = new int[,] { { 0, 1 }, { 6, 7 }, { 1, -2 } };

        [TestCaseSource(nameof(_dataForBubbleSort))]
        public void MatrixBubbleSortTest(int[,] array, string comparisonType, string orderType, int[,] expectedResult)
        {
            // Act
            var matrixSortTest = new MatrixSort();
            int[,] result = matrixSortTest.GetSortedArr(MatrixBubbleSort.BubbleSort, array, comparisonType, orderType);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        private static List<TestCaseData> _dataForBubbleSort =
            new List<TestCaseData>(new[]
            {
        new TestCaseData(testArray, "sum", "ascending", new int[,]{{ 1, -2}, { 0, 1 }, { 6, 7 }}),
        new TestCaseData(testArray, "sum", "descending", new int[,]{{ 6, 7 }, { 0, 1 }, { 1, -2 }}),
        new TestCaseData(testArray, "max", "ascending", new int[,]{{ 0, 1 }, { 1, -2 }, { 6, 7 }}),
        new TestCaseData(testArray, "max", "descending", new int[,]{{ 6, 7}, { 0, 1 }, { 1, -2 }}),
        new TestCaseData(testArray, "min", "ascending", new int[,]{{ 1, -2}, { 0, 1 }, { 6, 7 }}),
        new TestCaseData(testArray, "min", "descending", new int[,]{{ 6, 7}, { 0, 1 }, { 1, -2 }}),
            });

    }
}