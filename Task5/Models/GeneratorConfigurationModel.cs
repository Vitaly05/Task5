using Task5.Data;

namespace Task5.Models
{
    public class GeneratorConfigurationModel
    {
        public string Locale { get; set; } = LocaleCode.UNITED_STATES;

        public int Seed { get; set; } = 0;

        public int Page { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public double MistakesCount { get; set; } = 0;
    }
}