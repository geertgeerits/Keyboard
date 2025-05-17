namespace Keyboard
{
    public sealed class HexadecimalValidationTriggerAction : TriggerAction<Entry>
    {
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string? BorderName { get; set; }

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

                // Set the border color if the input is invalid
                Border border = (Border)entry.Parent.FindByName(BorderName);
                border.Stroke = isValidNumber ? Color.FromArgb("969696") : Colors.OrangeRed;
            }
        }
    }
}
