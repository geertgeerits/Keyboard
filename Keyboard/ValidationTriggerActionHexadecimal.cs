namespace Keyboard
{
    public sealed class ValidationTriggerActionHexadecimal : TriggerAction<Entry>
    {
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }

        protected override void Invoke(Entry entry)
        {
            // Convert hexadecimal values to decimal
            bool isValidMinValue = long.TryParse(MinValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long nMinValue);
            bool isValidMaxValue = long.TryParse(MaxValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long nMaxValue);
            bool isValidNumber = long.TryParse(entry.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long nHexResult);

            // Validate the number
            if (isValidMinValue && isValidMaxValue && isValidNumber)
            {
                isValidNumber = nHexResult >= nMinValue && nHexResult <= nMaxValue;

                if (entry.Parent is Border border && Application.Current?.Resources != null)
                {
                    if (isValidNumber)
                    {
                        if (Application.Current.Resources.TryGetValue("EntryValidNumber", out var validColor) && validColor is Color validColorValue)
                        {
                            border.Stroke = validColorValue;
                        }
                    }
                    else
                    {
                        if (Application.Current.Resources.TryGetValue("EntryInvalidNumber", out var InvalidColor) && InvalidColor is Color InvalidColorValue)
                        {
                            border.Stroke = InvalidColorValue;
                        }
                    }
                }
            }
        }
    }
}
