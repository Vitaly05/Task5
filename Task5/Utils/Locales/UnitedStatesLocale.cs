using Task5.Data;

namespace Task5.Utils.Locales
{
    public class UnitedStatesLocale : Locale
    {
        public override string Code => LocaleCode.UNITED_STATES;

        public override string PhoneFormat => "+1 (###) ###-####";

        public override string Corpus => "";

        public override string House => "";

        public override string ApartmentNumber => "";

        public override Characters Characters => new Characters() { StartCharacter = 'a', EndCharacter = 'z' };
    }
}
