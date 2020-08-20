namespace Neko
{
    class Keyboard
    {
        static public VkNet.Model.Keyboard.MessageKeyboard MessageKeyboard = new VkNet.Model.Keyboard.MessageKeyboard();
        static public void InitKeyboard()
        {
            VkNet.Model.Keyboard.KeyboardBuilder a = new VkNet.Model.Keyboard.KeyboardBuilder();
            a.AddButton("неко", "1", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некололи", "2", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некогиф", "3", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddButton("некочиби", "4", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive);
            a.AddLine();
            a.AddButton("неко+", "5", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            a.AddButton("некололи+", "6", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            a.AddButton("нековидео", "7", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative);
            MessageKeyboard = a.Build();
        }
    }
}
