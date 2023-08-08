using Task5.Data;

namespace Task5.Utils.Locales
{
    public abstract class Locale
    {
        public abstract string Code { get; }

        public abstract string PhoneFormat { get; }

        public abstract string Corpus { get; }

        public abstract string House { get; }

        public abstract string ApartmentNumber { get; }

        public abstract Characters Characters { get; }

        public static Locale GetLocale(string locale) =>
            locale switch
            {
                LocaleCode.UNITED_STATES => new UnitedStatesLocale(),
                LocaleCode.RUSSIAN => new RussianLocale(),
                LocaleCode.POLAND => new PolandLocale(),
                _ => null
            };
    }
}