using System;

namespace Dawnx.Localization
{
    public abstract class CurrencyUtility
    {
        protected abstract string _GetString(double money);
        protected abstract double _GetDouble(string money);

    }
}
