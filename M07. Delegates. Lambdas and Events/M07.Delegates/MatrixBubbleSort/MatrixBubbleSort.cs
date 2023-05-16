namespace MatrixBubbleSort
{
    class MatrixBubbleSort
    {
        private static int[,] _arr;

        public static int[,] BubbleSort(int[,] inputArr, string comparisonType, string orderType)
        {
            _arr = inputArr;
            for (int i = 0; i < _arr.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < _arr.GetLength(0) - 1; j++)
                {
                    if(comparisonType == "sum")
                    {
                        if (orderType == "ascending")
                        {
                            if (CalculateRowSum(j) > CalculateRowSum(j + 1))
                            {
                                Swap(j, j + 1, "ascending");
                            }
                        }
                        else
                        {
                            if (CalculateRowSum(j) < CalculateRowSum(j + 1))
                            {
                                Swap(j, j + 1, "descending");
                            }
                        }
                    }

                    if (comparisonType == "max")
                    {
                        if (orderType == "ascending")
                        {
                            if (FindMaxMinRowElement(j, "max") > FindMaxMinRowElement(j + 1, "max"))
                            {
                                Swap(j, j + 1, "ascending");
                            }
                        }
                        else
                        {
                            if (FindMaxMinRowElement(j, "max") < FindMaxMinRowElement(j + 1, "max"))
                            {
                                Swap(j, j + 1, "descending");
                            }
                        }

                    }

                    if (comparisonType == "min")
                    {
                        if (orderType == "ascending")
                        {
                            if (FindMaxMinRowElement(j, "min") > FindMaxMinRowElement(j + 1, "min"))
                            {
                                Swap(j, j + 1, "ascending");
                            }
                        }
                        else
                        {
                            if (FindMaxMinRowElement(j, "min") < FindMaxMinRowElement(j + 1, "min"))
                            {
                                Swap(j, j + 1, "descending");
                            }
                        }

                    }
                }

            }
            return _arr;
        }



        //public int[,] SortByMinElement(string order)
        //{
        //    for (int i = 0; i < _arr.GetLength(0) - 1; i++)
        //    {
        //        for (int j = 0; j < _arr.GetLength(0) - 1; j++)
        //        {
        //            if (order == "ascending")
        //            {
        //                if (FindMaxMinRowElement(j, "min") > FindMaxMinRowElement(j + 1, "min"))
        //                {
        //                    Swap(j, j + 1, "ascending");
        //                }
        //            }
        //            else
        //            {
        //                if (FindMaxMinRowElement(j, "min") < FindMaxMinRowElement(j + 1, "min"))
        //                {
        //                    Swap(j, j + 1, "descending");
        //                }
        //            }
        //        }

        //    }
        //    return _arr;
        //}


        //public int[,] SortByMaxElement(string order)
        //{
        //    for (int i = 0; i < _arr.GetLength(0) - 1; i++)
        //    {
        //        for (int j = 0; j < _arr.GetLength(0) - 1; j++)
        //        {
        //            if (order == "ascending")
        //            {
        //                if (FindMaxMinRowElement(j, "max") > FindMaxMinRowElement(j + 1, "max"))
        //                {
        //                    Swap(j, j + 1, "ascending");
        //                }
        //            }
        //            else
        //            {
        //                if (FindMaxMinRowElement(j, "max") < FindMaxMinRowElement(j + 1, "max"))
        //                {
        //                    Swap(j, j + 1, "descending");
        //                }
        //            }
        //        }

        //    }            
        //    return _arr;
        //}


        private static int FindMaxMinRowElement(int row, string direction)
        {
            if (direction == "max")
            {
                int max = _arr[row, 0];
                for (int i = 1; i < _arr.GetLength(1); i++)
                {
                    if (_arr[row, i] > max)
                    {
                        max = _arr[row, i];
                    }
                }
                return max;
            }
            else
            {
                int min = _arr[row, 0];
                for (int i = 1; i < _arr.GetLength(1); i++)
                {
                    if (_arr[row, i] < min)
                    {
                        min = _arr[row, i];
                    }
                }
                return min;
            }
        }


        //public int[,] SortByElementsSum(string order)
        //{
        //    for (int i = 0; i < _arr.GetLength(0) - 1; i++)
        //    {
        //        for (int j = 0; j < _arr.GetLength(0) - 1; j++)
        //        {
        //            if (order == "ascending")
        //            {
        //                if (CalculateRowSum(j) > CalculateRowSum(j + 1))
        //                {
        //                    Swap(j, j + 1, "ascending");
        //                }
        //            }
        //            else
        //            {
        //                if (CalculateRowSum(j) < CalculateRowSum(j + 1))
        //                {
        //                    Swap(j, j + 1, "descending");
        //                }
        //            }
        //        }

        //    }

        //    return _arr;
        //}

        private static int CalculateRowSum(int row)
        {
            int result = 0;
            for (int i = 0; i < _arr.GetLength(1); i++)
            {
                 result += _arr[row, i];             
            }
            return result;
        }

        private static int[,] Swap(int first, int second, string order)
        {         
            
            for (int i = 0; i < _arr.GetLength(1); i++)
            {
                if (order == "ascending")
                {
                    (_arr[first, i], _arr[second, i]) = (_arr[second, i], _arr[first, i]);
                }
                else
                {
                    (_arr[second, i], _arr[first, i]) = (_arr[first, i], _arr[second, i]);
                }
            }
            
            return _arr;
        }

    }
}
