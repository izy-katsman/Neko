using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Neko
{
    class Program
    {
        static Database database = new Database("server=localhost;user=root;database=user;password='';");


        static VkApi vk = new VkApi();
        static VkApi bot = new VkApi();

        static long? grupid = 175384626;//152541438;

        static string az;
        static string message = string.Empty;

        static int Type = -1;
        static int dbCnt = -1;
        static ulong cnt = 0;

        static List<long> albumsId = new List<long>()
        {
            260518371, //0
            260676421, //1
            260521585, //2
            260909385, //3
            260578308  //4
        };
        //количество должно совпадать сколичеством альбомов + 2, количество регулируется командой init
        static List<ulong> maxCount = new List<ulong>()
        {
            0, 0, 0, 0, 0,
            0,//gif
            0//viseo
        };

        static bool isTextCommand = false;
        static bool isCommand = true;
        static void Main(string[] args)
        {
            List<VkNet.Model.Attachments.MediaAttachment> attachement = new List<VkNet.Model.Attachments.MediaAttachment>();
            VkNet.Enums.Filters.Settings
            scope = VkNet.Enums.Filters.Settings.Photos | VkNet.Enums.Filters.Settings.Video | VkNet.Enums.Filters.Settings.Documents | VkNet.Enums.Filters.Settings.Groups;
            scope |= VkNet.Enums.Filters.Settings.Offline;
            while (!vk.IsAuthorized)
            {
                try
                {
                    vk.Authorize(new ApiAuthParams
                    {
                        ApplicationId = 6813101,
                        Login = "89110918216",
                        Password = "Vfrcbvjd11121973-",
                        Settings = scope
                    });


                    Console.WriteLine("Editor");
                    bot.Authorize(new ApiAuthParams
                    {
                        AccessToken = "5001a44df8159537e85e5e1f7aef044a6dc80f28c654558a743cae3ec706be070e759652e786c6fd27f45"//"77bd4687e99b592e73aaa349b20db37d983a78fb70595f2736842780631187a1a2aad07060d874dc4c90a",
                    });

                    Console.WriteLine("Bot");
                }
                catch (VkNet.Exception.VkApiException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            Keyboard.InitKeyboard();

            List<VkNet.Model.Attachments.Photo> l = new List<VkNet.Model.Attachments.Photo>();
            VkNet.Utils.VkCollection<VkNet.Model.Attachments.Photo> photo = new VkNet.Utils.VkCollection<VkNet.Model.Attachments.Photo>(1, l);
            List<VkNet.Model.Attachments.Document> d = new List<VkNet.Model.Attachments.Document>();
            VkNet.Utils.VkCollection<VkNet.Model.Attachments.Document> doc = new VkNet.Utils.VkCollection<VkNet.Model.Attachments.Document>(1, d);


            GetConversationsResult dialogs = new GetConversationsResult();
            attachement.Add(null);

            Init();
            while (true)
            {
                Thread.Sleep(200);
                try
                {
                    dialogs = bot.Messages.GetConversations(new VkNet.Model.RequestParams.GetConversationsParams
                    {
                        Extended = true,
                        GroupId = (ulong)grupid,
                        Filter = VkNet.Enums.SafetyEnums.GetConversationFilter.Unread,
                        Count = 200
                    });

                    for (int i = 0; i < dialogs.UnreadCount; i++)
                    {
                        message = dialogs.Items[i].LastMessage.Text.ToLower();
                        az = DateTime.Now.ToString() + " " + message + "\n";
                        Console.WriteLine(az);
                        System.IO.File.AppendAllText(@"keyboard_log.txt", az);

                        database.IntializeUser((long)dialogs.Items[i].LastMessage.PeerId);

                        GetTypeAndDbCnt(dialogs.Items[i].LastMessage.Text.ToLower(), dialogs.Items[i].LastMessage.PeerId);

                        if (isCommand)
                        {
                            if (isTextCommand)
                            {
                                if (Type == 100 && IsAdmin(dialogs.Items[i].LastMessage.PeerId))//признак админской команды
                                {
                                    List<User> users = new List<User>();
                                    var members = bot.Groups.GetMembers(new GroupsGetMembersParams()
                                    {
                                        GroupId = grupid.ToString()
                                    });

                                    foreach(var item in members)
                                        if (bot.Messages.IsMessagesFromGroupAllowed((ulong)grupid, (ulong)members[i].Id))
                                        {
                                            bot.Messages.Send(new MessagesSendParams
                                            {
                                                Keyboard = Keyboard.MessageKeyboard,
                                                UserId = item.Id,
                                                RandomId = (int)DateTime.UtcNow.Ticks,
                                                Message = Messages.rassMessage
                                            });
                                        }
                                }
                                bot.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                                {
                                    Keyboard = Keyboard.MessageKeyboard,
                                    UserId = dialogs.Items[i].LastMessage.PeerId,
                                    RandomId = (int)DateTime.UtcNow.Ticks,
                                    Message = message
                                });
                            }
                            else
                            {
                                cnt = database.GetArtCount((long)dialogs.Items[i].LastMessage.PeerId, dbCnt);
                                if (cnt >= maxCount[Type])
                                {
                                    database.InsertCurrentCount((long)dialogs.Items[i].LastMessage.PeerId, 0, dbCnt);
                                    cnt = 0;
                                }

                                if (Type < 5)
                                {
                                    photo = vk.Photo.Get(new VkNet.Model.RequestParams.PhotoGetParams
                                    {
                                        OwnerId = -grupid,
                                        AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Id(albumsId[Type]),
                                        Count = 1,
                                        Offset = cnt
                                    });
                                    attachement[0] = photo[0];
                                }
                                else//видео или гифка
                                {
                                    if (Type == 5)//гифка
                                    {
                                        doc = vk.Docs.Get(1, (int)cnt, -grupid);
                                        attachement[0] = doc[0];
                                    }
                                    else//видео
                                    {
                                        var video = vk.Video.Get(new VkNet.Model.RequestParams.VideoGetParams
                                        {
                                            OwnerId = -grupid,
                                            Offset = (long?)cnt,
                                            Count = 1,
                                        });
                                        attachement[0] = video[0];
                                    }
                                }
                                if (bot.Messages.IsMessagesFromGroupAllowed((ulong)grupid, (ulong)dialogs.Items[i].LastMessage.PeerId))
                                {
                                    bot.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                                    {
                                        Keyboard = Keyboard.MessageKeyboard,
                                        UserId = dialogs.Items[i].LastMessage.PeerId,
                                        RandomId = (int)DateTime.UtcNow.Ticks,
                                        Attachments = attachement
                                    });
                                    database.InsertCurrentCount((long)dialogs.Items[i].LastMessage.PeerId, cnt + 1, dbCnt);
                                }
                                else
                                    bot.Messages.MarkAsRead(dialogs.Items[i].LastMessage.PeerId.ToString());
                            }
                        }
                        Type = -1;
                        isCommand = true;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    attachement.Clear();
                    attachement.Add(null);
                }
            }

        }
        //задаем макимальные велечины
        static private void Init()
        {
            var photos = vk.Photo.GetAlbums(new VkNet.Model.RequestParams.PhotoGetAlbumsParams
            {
                OwnerId = -grupid,
                AlbumIds = albumsId,
                Count = 1,
                Offset = 0,
            });

            for (int i = 0; i < albumsId.Count; i++)
            {
                maxCount[i] = (ulong)photos[i].Size;
            }


            var doc = vk.Docs.Get(10, 0, -grupid, VkNet.Enums.DocFilter.Gif);
            maxCount[5] = 92;
            //(ulong)doc[0].Size;


            //var video = vk.Video.GetAlbums(grupid, 0, 1);//new VkNet.Model.RequestParams.VideoGetParams
            //{
            //    OwnerId = -grupid,
            //    Offset = 0
            //});
            //туплю не помню как взять количество видео
            maxCount[6] = 2;

        }


        static bool IsAdmin(long? id)
        {
            if (id == 160656338)
                return true;

            return false;
        }


        static void GetTypeAndDbCnt(string message, long? id)
        {
            switch (message)
            {
                case "рассылка":
                    Type = 100;
                    message = Messages.rass;
                    isTextCommand = true;
                    break;

                case "неко":
                    Type = 0;
                    dbCnt = 1;
                    break;

                case "неко+":
                    Type = 1;
                    dbCnt = 5;
                    break;

                case "некололи":
                    Type = 2;
                    dbCnt = 2;
                    break;

                case "некололи+":
                    Type = 3;
                    dbCnt = 6;
                    break;

                case "некочиби":
                    Type = 4;
                    dbCnt = 4;
                    break;

                case "нековидео":
                    Type = 6;
                    dbCnt = 7;
                    break;

                case "некогиф":
                    Type = 5;
                    dbCnt = 3;
                    break;

                case "привет":
                    message = Messages.hi;
                    isTextCommand = true;
                    break;

                case "сука":
                    message = Messages.suka;
                    isTextCommand = true;
                    break;

                case "команды":
                    message = Messages.commands;
                    isTextCommand = true;
                    break;

                case "команда":
                    message = Messages.commands;
                    isTextCommand = true;
                    break;

                case "помощь":
                    message = Messages.commands;
                    isTextCommand = true;
                    break;

                case "help":
                    message = Messages.commands;
                    isTextCommand = true;
                    break;

                case "версия":
                    message = Messages.version;
                    isTextCommand = true;
                    break;
                default:
                    isCommand = false;
                    bot.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                    {
                        Keyboard = Keyboard.MessageKeyboard,
                        UserId = id,
                        RandomId = (int)DateTime.UtcNow.Ticks,
                        Message = "😅 Не знаю такой команды хозяин...\nВведи «команды» или воспользуйся кнопками 🐾"
                    });
                    break;
            }
        }
    }
}
