using Bogus;
using Task5.Models;
using Task5.Utils.Locales;

namespace Task5.Utils
{
    public class FakeDataGenerator
    {
        private readonly GeneratorConfigurationModel configuration;

        private readonly Locale locale;

        private readonly AddressGenerator addressGenerator;

        public FakeDataGenerator(GeneratorConfigurationModel generatorConfigurationModel)
        {
            configuration = generatorConfigurationModel;
            locale = Locale.GetLocale(configuration.Locale);
            addressGenerator = new AddressGenerator(locale);
        }

        public List<FakeUserDataModel> GenerateUsersData()
        {
            Randomizer.Seed = new Random(configuration.Seed - configuration.Page);
            var faker = createFaker(configuration.Page * 10);
            return faker.Generate(configuration.PageSize);
        }

        private Faker<FakeUserDataModel> createFaker(int startId = 0)
        {
            return new Faker<FakeUserDataModel>(locale.Code)
                .CustomInstantiator(f => new FakeUserDataModel() { Id = startId++ })
                .RuleFor(u => u.RandomId, f => f.Random.Guid())
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Address, f => addressGenerator.GenerateAddress(f))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber(locale.PhoneFormat));
        }
    }
}