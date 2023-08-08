using Task5.Data;

namespace Task5.Utils.Locales
{
    public class PolandLocale : Locale
    {
        public override string Code => LocaleCode.POLAND;

        public override string PhoneFormat => "+48 (###) ###-###";

        public override string Corpus => "lok.";

        public override string House => "";

        public override string ApartmentNumber => "m.";
    }
}
