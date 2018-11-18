using System;
using System.Collections.Generic;
using System.Text;
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
            return new RowHouseApartment(match.Groups[1].Value, int.Parse(match.Groups[2].Value));
        }
    }
}
