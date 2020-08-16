namespace Neko
{
    class Keyboard
    {
        static public VkNet.Model.Keyboard.MessageKeyboard MessageKeyboard = new VkNet.Model.Keyboard.MessageKeyboard();
        static public void InitKeyboard()
        {
            VkNet.Model.Keyboard.KeyboardBuilder a = new VkNet.Model.Keyboard.KeyboardBuilder();
            a.AddButton("неко", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некололи", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некогиф", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некочиби", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddLine();
            a.AddButton("неко+", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            a.AddButton("некололи+", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            a.AddButton("нековидео", "", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            MessageKeyboard = a.Build();
        }
    }
}
