
public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// For example, MultiplesOf(7, 5) will result in {7, 14, 21, 28, 35}.
    /// </summary>
    /// <returns>Array of doubles that are multiples of the supplied number.</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Create a new double array with the specified length.
        // 2. Loop through each position in the array, starting at index 0.
        // 3. Multiply the supplied number by the current index plus 1.
        // 4. Store the result in the array at the current index.
        // 5. Return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the data to the right by the specified amount.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // 1. Calculate the starting index of the elements to move.
        // 2. Create a temporary list containing the last 'amount' elements.
        // 3. Remove those elements from the original list.
        // 4. Insert the temporary list at the beginning of the original list.

        int startIndex = data.Count - amount;

        List<int> lastElements = data.GetRange(startIndex, amount);

        data.RemoveRange(startIndex, amount);

        data.InsertRange(0, lastElements);
    }
}
