using Bogus;
using Task5.Models;
using Task5.Utils.Locales;

namespace Task5.Utils
{
    public class InputMistakesGenerator
    {
        private readonly int MistakesCount;

        private readonly double lastMistakeChance;

        private readonly Locale locale;

        private readonly Faker faker;

        private List<Action<FakeUserDataModel>> propertiesMethods;

        private List<Func<string, string>> errorsMethods;

        private Dictionary<string, int> oldLenghts;

        public InputMistakesGenerator(GeneratorConfigurationModel model)
        {
            MistakesCount = (int)model.MistakesCount;
            lastMistakeChance = model.MistakesCount - MistakesCount;
            locale = Locale.GetLocale(model.Locale);
            faker = new Faker();
            setMethods();
        }

        public FakeUserDataModel MakeErrors(FakeUserDataModel model)
        {
            setOldLengths(model);
            for (int i = 0; i < MistakesCount; i++)
                faker.PickRandom(propertiesMethods).Invoke(model);
            if (faker.Random.Double() < lastMistakeChance)
                faker.PickRandom(propertiesMethods).Invoke(model);
            return model;
        }

        private void setMethods()
        {
            setropertiesMethods();
            setErrorsMethods();
        }

        private void setropertiesMethods()
        {
            propertiesMethods = new()
            {
                (m) => makeErrorInFullName(m),
                (m) => makeErrorInAddress(m),
                (m) => makeErrorInPhoneNumber(m)
            };
        }

        private void setErrorsMethods()
        {
            errorsMethods = new()
            {
                (s) => addCharacter(s),
                (s) => deleteCharacter(s),
                (s) => swapCharacters(s)
            };
        }

        private void setOldLengths(FakeUserDataModel model)
        {
            oldLenghts = new Dictionary<string, int>()
            {
                { nameof(model.FullName), model.FullName.Count() },
                { nameof(model.Address), model.Address.Count() },
                { nameof(model.PhoneNumber), model.PhoneNumber.Count() }
            };
        }

        private void makeErrorInFullName(FakeUserDataModel model)
        {
            float[] weights = getWeights(oldLenghts[nameof(model.FullName)], model.FullName.Length);
            model.FullName = faker.Random.WeightedRandom(errorsMethods.ToArray(), weights).Invoke(model.FullName);
        }

        private void makeErrorInAddress(FakeUserDataModel model)
        {
            float[] weights = getWeights(oldLenghts[nameof(model.Address)], model.Address.Length);
            model.Address = faker.Random.WeightedRandom(errorsMethods.ToArray(), weights).Invoke(model.Address);
        }

        private void makeErrorInPhoneNumber(FakeUserDataModel model)
        {
            float[] weights = getWeights(oldLenghts[nameof(model.PhoneNumber)], model.PhoneNumber.Length);
            model.PhoneNumber = faker.Random.WeightedRandom(errorsMethods.ToArray(), weights).Invoke(model.PhoneNumber);
        }

        private string addCharacter(string input)
        {
            int index = faker.Random.Int(0, input.Length - 1);
            char randomChar = faker.PickRandom(
                faker.Random.Char(locale.Characters.StartCharacter, locale.Characters.EndCharacter),
                faker.Random.Char('0', '9'));
            return input.Insert(index, randomChar.ToString());
        }

        private string deleteCharacter(string input)
        {
            int index = faker.Random.Int(0, input.Length - 1);
            return input.Remove(index, 1);
        }

        private string swapCharacters(string input)
        {
            int index = faker.Random.Int(0, input.Length - 2);
            char[] charArray = input.ToCharArray();
            char temp = charArray[index];
            charArray[index] = charArray[index + 1];
            charArray[index + 1] = temp;
            return new string(charArray);
        }

        private float[] getWeights(int oldLength, int actualLength) =>
            (oldLength - actualLength) switch
            {
                > 0 => new float[] { 1f, 0f, 0f },
                < 0 => new float[] { 0f, 1f, 0f },
                0 => new float[] { .33f, .33f, .34f }
            };
    }
}