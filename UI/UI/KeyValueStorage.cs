using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace UI
{
    internal static class KeyValueStorage
    {
        private const string ApartmentIdStorageKey = "ApartmentId";

        private static IDictionary<string, object> CurrentApplicationProperties => Application.Current.Properties;

        public static void SetApartmentId(Guid id)
        {
            Set(ApartmentIdStorageKey, id);
        }

        public static bool HasApartment()
        {
            return HasValue(ApartmentIdStorageKey);
        }

        private static void Set(string key, object value)
        {
            CurrentApplicationProperties[key] = value;
        }

        private static bool HasValue(string key)
        {
            return CurrentApplicationProperties.ContainsKey(key);
        }
    }
}
