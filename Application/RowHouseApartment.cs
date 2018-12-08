using System;
using System.Text.RegularExpressions;

namespace RowHouseTurnManagement.Application
{
    internal class RowHouseApartment
    {
        public string RowAddress { get; }
        public int ApartmentNumber { get; }

        private RowHouseApartment(string rowAddress, int apartmentNumber)
        {
            RowAddress = rowAddress;
            ApartmentNumber = apartmentNumber;
        }

        public static RowHouseApartment Create(string streetAddress)
        {
            Match match = Regex.Match(streetAddress, @"^(.+?)\s*(?:as\.?)?\s*(\d+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                GroupCollection matchedGroups = match.Groups;
                return new RowHouseApartment(matchedGroups[1].Value, int.Parse(matchedGroups[2].Value));
            }
            throw new ArgumentException($"Couldn't separate apartment number from street address '{streetAddress}'.");
        }
    }
}
