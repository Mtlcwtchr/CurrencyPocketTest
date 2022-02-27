using System;
using System.Collections.Generic;
using Pocket.Abstraction;

namespace DefaultNamespace
{
    public class CurrencyPocketConfiguration : ICurrencyPocketConfiguration<float>
    {
        public Action<string, float> OnCurrencyUpdated { get; set; }
        public List<string> SupportedCurrencyTypes { get; set; }
    }
}