namespace Keyboard
{
    /// <summary>
    /// Shared characters dictionary that can be reused by other pages/components
    /// Keys are the text on the key (e.g. "A" or "a")
    /// Values are arrays of characters to display in the buttons in order
    /// </summary>
    public static class ClassKeyboardLayouts
    {
        /// <summary>
        /// Provides a mapping of keyboard layout identifiers to their corresponding sets of supported characters.
        /// </summary>
        /// <remarks>Each entry associates a layout name, such as "QWERTY_US" or "AZERTY_BE", with an
        /// array of characters available on that keyboard layout. The dictionary uses ordinal string comparison for
        /// layout identifiers. This collection can be used to determine which characters are accessible for a given
        /// keyboard type, for example, when validating input or customizing user interfaces.</remarks>
        public static readonly Dictionary<string, string[]> KeyboardLayouts = new(StringComparer.Ordinal)
        {
            // The number of characters must be exactly 53 per keyboard layout (0-52)
            // The key 'space' is at index 44 zero based and 45 one based
            //                 01234567890123456789012345678901234567890123456789012

            //["ABCDEF_XX"] = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ?!/:_-,. ;@#€%&|*",
            //["AZERTY_BE"] = "1234567890AZERTYUIOPQSDFGHJKLMWXCVBN?!/:_-,. ;@#€%&|*",
            //["QWERTY_UK"] = "1234567890QWERTYUIOPASDFGHJKL:+ZXCVBNM?!/_-, .@#£%&|*",
            //["QWERTY_US"] = "1234567890QWERTYUIOPASDFGHJKL:ZXCVBNM,./?!_- ;@#$%&|*",
            //["OTHER"] = "1234567890Ø²³%‰+×÷=*/\\<>{}[]()|!?¿@#&^$€£¥_- '\";:,.§¨"

            ["ABCDEF_XX"] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "?", "!", "/", ":", "_", "-", ",", ".", " ", ";", "@", "#", "€", "%", "&", "|", "*"],
            ["AZERTY_BE"] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "Z", "E", "R", "T", "Y", "U", "I", "O", "P", "Q", "S", "D", "F", "G", "H", "J", "K", "L", "M", "W", "X", "C", "V", "B", "N", "?", "!", "/", ":", "_", "-", ",", ".", " ", ";", "@", "#", "€", "%", "&", "|", "*"],
            ["QWERTY_UK"] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", ":", "+", "Z", "X", "C", "V", "B", "N", "M", "?", "!", "/", "_", "-", ",", " ", ".", "@", "#", "£", "%", "&", "|", "*"],
            ["QWERTY_US"] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", ":", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "?", "!", "_", "-", " ", ";", "@", "#", "$", "%", "&", "|", "*"],
            ["OTHER"] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Ø", "²", "³", "%", "‰", "+", "×", "÷", "=", "*", "/", "\\", "<", ">", "{", "}", "[", "]", "(", ")", "|", "!", "?", "¿", "@", "#", "&", "^", "$", "€", "£", "¥", "_", "-", " ", "'", "\"", ";", ":", ",", ".", "§", "¨"]
        };

        /// <summary>
        /// Provides a mapping of specific characters to their corresponding popup character options
        /// </summary>
        public static readonly Dictionary<string, string[]> PopupCharacters = new(StringComparer.Ordinal)
        {
            ["1"] = ["¹", "½", "¼", "⅓", "⅛"],
            ["2"] = ["²", "⅔"],
            ["3"] = ["³", "¾", "⅜"],
            ["4"] = ["¼"],
            ["5"] = ["⅕", "⅝"],
            ["6"] = ["⅙"],
            ["7"] = ["⅐"],
            ["8"] = ["⅛"],
            ["9"] = ["⅑"],
            ["0"] = ["⁰"],

            ["A"] = ["Á", "À", "Â", "Ä", "Ã", "Å", "Ā", "Æ"],
            ["B"] = ["B́", "B̀", "B̈", "Ḃ", "Ḇ", "B̨"],
            ["C"] = ["Ć", "C̀", "Ç", "Č", "Č", "Ċ", "C̱", "C̨"],
            ["D"] = ["D́", "D̀", "D̈", "Ḋ", "Ḏ", "D̨"],
            ["E"] = ["É", "È", "Ê", "Ë", "Ē", "Ė", "Ę", "Ě"],
            ["F"] = ["F́", "F̀", "F̈", "Ḟ", "F̱", "F̨"],
            ["G"] = ["Ǵ", "G̀", "G̈", "Ġ", "G̱", "G̨"],
            ["H"] = ["H́", "H̀", "Ḧ", "Ḣ", "H̱", "H̨"],
            ["I"] = ["Í", "Ì", "Î", "Ï", "Ī", "Į", "İ"],
            ["J"] = ["J́", "J̀", "J̈", "J̇", "J̱", "J̨"],
            ["K"] = ["Ḱ", "K̀", "K̈", "K̇", "Ḵ", "K̨"],
            ["L"] = ["Ĺ", "L̀", "L̈", "L̇", "Ḻ", "L̨"],
            ["M"] = ["Ḿ", "M̀", "M̈", "Ṁ", "M̱", "M̨"],
            ["N"] = ["Ñ", "Ń", "Ǹ", "Ń", "Ņ", "N̈", "Ṅ", "Ṉ", "N̨"],
            ["O"] = ["Ó", "Ò", "Ô", "Ö", "Õ", "Ø", "Ō", "Ő", "Ö", "Ȯ", "O̱", "Ǫ"],
            ["P"] = ["Ṕ", "P̀", "P̈", "Ṗ", "P̱", "P̨"],
            ["Q"] = ["Q́", "Q̀", "Q̈", "Q̇", "Q̱", "Q̨"],
            ["R"] = ["Ŕ", "R̀", "R̈", "Ṙ", "Ṟ", "R̨"],
            ["S"] = ["Ś", "Š", "S̈", "Ṡ", "S̱", "S̨"],
            ["T"] = ["T́", "T̀", "T̈", "Ṫ", "Ṯ", "T̨"],
            ["U"] = ["Ú", "Ù", "Û", "Ü", "Ū"],
            ["V"] = ["V́", "V̀", "V̈", "V̇", "V̱", "V̨"],
            ["W"] = ["Ẃ", "Ẁ", "Ẅ", "Ẇ", "W̱", "W̨"],
            ["X"] = ["X́", "X̀", "Ẍ", "Ẋ", "X̱", "X̨"],
            ["Y"] = ["Ý", "Ỳ", "Ŷ", "Ÿ"],
            ["Z"] = ["Ź", "Ž", "Ż", "Ẑ"],

            ["a"] = ["á", "à", "â", "ä", "ã", "å", "ā", "ą", "ā́", "ā̀", "ª", "ạ", "ả", "ã́", "ã̀", "ɑ", "æ"],
            ["b"] = ["b́", "b̀", "β", "ḃ", "ḇ", "b̨"],
            ["c"] = ["ć", "c̀", "ç", "č", "č", "ċ", "c̱", "c̨"],
            ["d"] = ["d́", "d̀", "δ", "d̈", "ḋ", "ḏ", "d̨"],
            ["e"] = ["é", "è", "ê", "ë", "ē"],
            ["f"] = ["f́", "f̀", "ƒ", "f̈", "ḟ", "f̱", "f̨"],
            ["g"] = ["ǵ", "g̀", "ğ", "g̈", "ġ", "g̱", "g̨"],
            ["h"] = ["h́", "h̀", "ħ", "ḧ", "ḣ", "ẖ", "h̨"],
            ["i"] = ["í", "ì", "î", "ï", "ī"],
            ["j"] = ["j́", "j̀", "ĵ", "j̈", "j̇", "j̱", "j̨"],
            ["k"] = ["ḱ", "k̀", "ķ", "k̈", "k̇", "ḵ", "k̨"],
            ["l"] = ["ĺ", "l̀", "ł", "l̈", "l̇", "ḻ", "l̨"],
            ["m"] = ["ḿ", "m̀", "μ", "m̈", "ṁ", "m̱", "m̨"],
            ["n"] = ["ñ", "ń", "ǹ", "ń", "n̈", "ṅ", "ṉ", "n̨"],
            ["o"] = ["ó", "ò", "ô", "ö", "õ", "ø", "ö", "ȯ", "o̱", "ǫ"],
            ["p"] = ["ṕ", "p̀", "π", "p̈", "ṗ", "p̱", "p̨"],
            ["q"] = ["q́", "q̀", "ɋ", "q̈", "q̇", "q̱", "q̨"],
            ["r"] = ["ŕ", "r̀", "ř", "r̈", "ṙ", "ṟ", "r̨"],
            ["s"] = ["ś", "š", "ß", "s̈", "ṡ", "s̱", "s̨"],
            ["t"] = ["t́", "t̀", "ţ", "ẗ", "ṫ", "ṯ", "t̨"],
            ["u"] = ["ú", "ù", "û", "ü", "ū", "ü", "u̇", "u̱", "ų"],
            ["v"] = ["v́", "v̀", "ν", "v̈", "v̇", "v̱", "v̨"],
            ["w"] = ["ẃ", "ẁ", "ŵ", "ẅ", "ẇ", "w̱", "w̨"],
            ["x"] = ["x́", "x̀", "χ", "ẍ", "ẋ", "x̱", "x̨"],
            ["y"] = ["ý", "ỳ", "ŷ", "ÿ", "ẏ", "y̱", "y̨"],
            ["z"] = ["ź", "ž", "ż", "z̈", "ż", "ẕ", "z̨"]
        };
    }
}
