using Bogus;
using Task5.Models;
using Task5.Utils.Locales;

namespace Task5.Utils
{
    public class FakeDataGenerator
    {
        private readonly int seed;

        private readonly Locale locale;

        private readonly AddressGenerator addressGenerator;

        public FakeDataGenerator(GeneratorConfigurationModel generatorConfigurationModel)
        {
            seed = generatorConfigurationModel.Seed;
            locale = Locale.GetLocale(generatorConfigurationModel.Locale);
            addressGenerator = new AddressGenerator(locale);
        }

        public List<FakeUserDataModel> GenerateUsersData(int amount = 20)
        {
            Randomizer.Seed = new Random(seed);
            var faker = createFaker();
            return faker.Generate(amount);
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