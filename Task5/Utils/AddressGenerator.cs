using Bogus;
using Task5.Utils.Locales;

namespace Task5.Utils
{
    public class AddressGenerator
    {
        private Locale locale;

        public AddressGenerator(Locale locale)
        {
            this.locale = locale;
        }

        public string GenerateAddress(Faker f)
        {
            return String.Join(", ", f.PickRandom(getSmallAddress(f), getLargeAddress(f)).Where(s => !string.IsNullOrEmpty(s)));
        }

        private IEnumerable<string> getLargeAddress(Faker f)
        {
            yield return f.Address.City();
            yield return f.Address.StreetName();
            yield return f.PickRandom($"{locale.Corpus} {f.Random.Number(1, 10)}", "");
            yield return $"{locale.House} {f.Address.BuildingNumber()}";
            yield return $"{locale.ApartmentNumber} {f.Random.Number(0, 400)}";
        }

        private IEnumerable<string> getSmallAddress(Faker f)
        {
            yield return f.Address.City();
            yield return f.Address.StreetAddress();
        }
    }
}