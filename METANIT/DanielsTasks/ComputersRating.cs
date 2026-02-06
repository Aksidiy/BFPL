namespace BFPL.METANIT.DanielsTasks
{
    internal class ComputersRating
    {
        public static double CalculateCompRating(uint totalComputers, uint correctComputers)
        {
            double compRating;

            if (correctComputers > totalComputers)
            {
                throw new ArgumentException("Wrong arguments: correctComputers can't be more than totalComputers.");
            }
            else if (correctComputers == 0 || totalComputers == 0)
            {
                compRating = 0;
            }
            else
            {
                compRating = ((((double)correctComputers / (double)totalComputers) * 5));
            }

            return Math.Round(compRating, 1);
        }
    }
}
