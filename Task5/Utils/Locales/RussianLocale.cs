using Task5.Data;

namespace Task5.Utils.Locales
{
    public class RussianLocale : Locale
    {
        public override string Code => LocaleCode.RUSSIAN;

        public override string PhoneFormat => "+7 (###) ###-##-##";

        public override string Corpus => "корп.";

        public override string House => "д.";

        public override string ApartmentNumber => "кв.";

        public override Characters Characters => new Characters() { StartCharacter = 'а', EndCharacter = 'я' };
    }
}