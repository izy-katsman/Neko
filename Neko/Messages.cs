using System.Collections.Generic;

namespace Neko
{
    class Messages
    {
        static public readonly string hi = "Ня! Привет хозяин.";
        static public readonly string suka = "Ругаться не хорошо! 😾";
        static public readonly string commands = "Список доступных команд (7 из 7):\n🍒 Неко — арты с неко 😺\n👙 Неко+ — неко для взрослых мальчиков (16+) 😻\n" +
            "🌞 Некололи — арты с лоли неко 🙀\n🌝 Некололи+ — арты с лоли неко+ 👮\n🆕 Некочиби — арты с мини неко 😺\n🎬 Некогиф — гифки с неко😽\n" +
            "📺 Нековидео — в главной роли НЕКО 😿";
        static public readonly string version = "💾 Версия бота: 1.0.45 от 23.05.19";
        static public readonly string rass = "Начата рассылка";
        static public readonly string rassMessage = "текст рассылки";
        static public readonly List<string> hellowList = new List<string>()
        {
            "Привет мой котик.", "И тебе привет!", "Привет..."
        };

        static public readonly List<string> hellowCommand = new List<string>()
        {
            "Привет", "хаюшки", "хай", "здравствуйте", "приветствую", "добрый день", "доброго времени суток", "хеллоу", "бонжур", "хай", "прив",
        };

        static public readonly string blackListWords = @"[хxh][уyu]([уy][aа]|[йieеёюuяи])
        ([з|3][\,\.]14|II|[пpр])([иui]|[з3z])([з3z]|[дd6])([дd6]|[цсc])
        [бb6][лl](я|[уy][aа])
        ([оo0]|[аa]){0,1}[ёеeuиi][б6b]([yу]|[аa]|[лl]|JI|[иui]){0,1}
        [зz3][бb6][сcs]
        [еeё][б6пpр][тt]
        (ch|[ч4])[мm][оo0]
        (II|[пpр])[иui][дd6]([рpr]|[оo0]|[аa])[рpr]{0,1}
        [пpр][еe][дd6][иeеi][кk]
        [sсc][уyu](ch|4|ч){0,1}([кk]|[aа])([aа]|[оo0])
        [лl][oо]([хx]|[(ш|sh)[oо]])
        [bб6][iиu][tт][(ch|ч|4)]
        [fф][uaа][cс]{0,1}[kк]
        [мm][уy][дd6]([аa]|[oо]|[иui])
        [гg]([о0O]|[aа])[вvb|mм][нnh]
        шлюха
        проститутка
        [гg]([о0O]|[aа])[нnh][dд][о0O][нnh]
        [mм][aа][нnh][dд][aа]
        jala[pb]
        жал[ая][пб]
        [о0]не[йи]н
        [o0]ne[yi]n";

        static public readonly string whiteListWords = @"нибудь
xyz
рубля
хлеб
сабл[яе]
гребл[яе]
оглобля
[тнм]ебе
требу[еийю]
оск[оа]рбля
небо
резаг
не[\s]{0,1}бы{0,1}л[ои]
лошад
факел
греб
штрихуй
перебалтыв
дебет
[тс]еб[ея]
[ts]eb(e|ya)
пасиб
pasib
серебр
serebr
переб
ч[её]рт
команд
реб[её]н
ребят
прибор
ошиб
рецепт
гриб
плох
коридор
жертв
враждебн
алгебр
смерт
библио
верт
волшеб
гибел
гибл
погиб
требов
концерт
либо
м[её]ртв
служебн
тепло
употребл
уч[её]б
факт
телохран
cub
сердцебие
[lл][оo]{1,}[лl]
ибрагим
blood
эксперт
butcher
телебашн
альберт
приб
кибер
кибор
кариб
хребту
ибо
р[её]бра
р[её]бер
трибун
litecraft.ru";
    }
}
