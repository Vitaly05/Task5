﻿using Task5.Data;

namespace Task5.Models
{
    public class GeneratorConfigurationModel
    {
        public string Locale { get; set; } = Data.LocaleCode.UNITED_STATES;

        public int Seed { get; set; } = 0;
    }
}