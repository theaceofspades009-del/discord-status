namespace TEST_DRPC
{
    internal static class Program
    {
        static readonly string[] states = [
            "Send me cat photos <3 ",
            "Send me ur floof pics~ ",
            "NieR Brother > NieR Father",
            "Weight of the world hitting hard.. :c",
            "You played NieR yet?",
            "Do I play satisfactory, or siege?",
            "Looking for a Modpack (MC) with quests, magic, tech and space... ",
            "Should I get back into beatsaber?",
            "Looking for ideas on what to program, hmu if you have an idea (for free c:)",
            "Pineapple belongs on pizza (hawaiian).",
            "WuWa > Genshin.",
            "2+2=5 for large values of 2 (:",
            "Reading: 'The Legendary Mechanic', you should check it out c:",
            "LF: Light Novel reccomendations~",
            "Cats > Dogs, I'm sorry.",
            "Japan or Canada..? Can't decide :(",
            "What else should I put here?",
            "Ask me about Project AURORA~",
            "Hello stranger~",
            "Helloo~ o/",
            "Now, if I had infinite ducks...",
            "Skiing is superior to snowboarding.",
            "I'm bored, hmu to do something...",
            "I can't decide between tea or coffee...",
            "Sunrises or sunsets?",
            "5D chess, Anyone?",
            "I think I need a new hobby.",
            "Music recommendations are always welcome!",
            "So, parallel universes?",
            "At the advent of genetic manipulation, what would cat people actually look like?",
            "There should be 36 hours in a day.",
            "This whole thing could've been an email...",
            @"https://www.youtube.com/watch?v=J-V3PFASyn4",
            @"https://www.youtube.com/watch?v=2b1IexhKPz4",
            @"https://www.youtube.com/watch?v=cQKGUgOfD8U",
            @"https://www.youtube.com/watch?v=EEk4JGzqoFg",
            "guh.",
            "I'm most definitely my cat's staff.",
            "Is it time to replay all of NieR?",
            "LF: Co-op games~",
            "I need more images for this... please send!",
            "I need more images for this... please send!",
            "I need more images for this... please send!",
            ],

            images = [
                "cornet",
                "voldusgk"
                /*
                "bored",
                "bored2",
                "lofigirl",
                "jinhsi",
                "cat1",
                "cat2",
                "cat3", */
            ];

        static string GetState(string[] ignores = null)
        {
            ignores ??= [];
            string outer;
            do outer = states[rnd.Next(states.Length)].Trim();
            while (ignores.Any(x => x.Trim() == outer));
            return outer + "  ";
        }

        static string GetImage(string[] ignores = null)
        {
            ignores ??= [];
            string outer;
            do outer = images[rnd.Next(images.Length)].Trim();
            while (ignores.Any(x => x.Trim() == outer));
            return outer;
        }

        static void Main(string[] args)
        {
            try
            {
                m(args).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                w($"Unhandled exception: {ex.Message}");
            }
        }

        static async Task m(string[] args)
        {
            w("Running Test!");
            w("Starting DClient...");
            var discord = new Discord.Discord(1419197907198017657, (UInt64)Discord.CreateFlags.Default);
            w("Created DCLient, setting log callback.");
            w("Setting disposal call...");
            System.Console.CancelKeyPress += (_, _) => { discord.Dispose(); Environment.Exit(0); };
            w("Disposal set.");
            w("Running initial callbacks");
            try
            {
                discord.RunCallbacks();
            }
            catch
            {
                w("Callbacks failed. Press any key to continue.");
                Console.ReadLine();
            }
            w("Callbacks ran, getting activity manager.");
            var am = discord.GetActivityManager();
            w("AM Recieved");
            w("Getting user manager...");
            var um = discord.GetUserManager();
            um.OnCurrentUserUpdate += () =>
            {
                var currentUser = um.GetCurrentUser();
                w("Name: " + currentUser.Username);
                w("ID: " + currentUser.Id);
            };
            try
            {
                w("Running callback for AM and UM");
                discord.RunCallbacks();
            }
            catch
            {
                w("Callbacks failed. Press any key to continue.");
                Console.ReadLine();
            }
            w("Callbacks succeeded.");
            w("Setting game path...");
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            w($"Path: {path}");
            w("UM obtained, attaching update event and attempting to get user information...");
            User user;
            try
            {
                user = um.GetCurrentUser();
            }
            catch
            {
                user = new User() { Username = "ERR", Id = 0 };
            }
            string info = 
                $@"User Info:
    ID: {user.Id}
    Name: {user.Username}
                ";
            w("Setting Activity");
            long start = ((DateTimeOffset)(DateTime.Now)).ToUnixTimeSeconds();
            long end = ((DateTimeOffset)((DateTime.Now).AddDays(30))).ToUnixTimeSeconds();
            w($"Start UNIX: {start}");
            w($"End UNIX: {end}");
            string s1 = GetState();
            string s2 = GetState([s1]);
            string s3 = GetState([s1, s2]);
            string s4 = GetState([s1, s2, s3]);
            string i1 = GetImage();
            string i2 = GetImage([i1]);
            int p1 = rnd.Next(0, 900);
            int p2 = rnd.Next(p1, 999);
            w($"State: {s1}");
            w($"Description: {s2}");
            w($"Large Tool tip: {s3}");
            w($"Small tool tip: {s4}");
            w($"Large Image: {i1}");
            w($"Small Image: {i2}");
            w($"Current: {p1}");
            w($"Max: {p2}");
            var activity = new Discord.Activity
            {
                  State = s1,
                  Details = s2,
                  Timestamps =
                  {
                      Start = start,
                      End = end,
                  },
                  Assets =
                  {
                      LargeImage = i1,
                      LargeText = s3,
                      SmallImage = i2,
                      SmallText = s4,
                  },
                  Party =
                  {
                      Id = "hmmmmm_curious",
                      Size = {
                          CurrentSize = p1,
                          MaxSize = p2,
                      },
                  },

                  Secrets =
                  {
                     Join="thisissupposedtobesecret",
                     Match="Ithoughtthiswassecrettoo",
                     Spectate="Dontwatchme_henjane"
                  },
                  Instance = true,
            };
            am.OnActivityJoin += (string secret) => 
            {
                am.AcceptInvite(user.Id, (Result a) => { string ss = "Lorum Ipsum"; _ = ss; });
            };
            am.OnActivitySpectate += (string secret) => 
            {
                string d = "Lorum Ipsum";
                _ = d;
            };
            am.OnActivityJoinRequest += (ref Discord.User d) => 
            {
                am.SendRequestReply(user.Id, ActivityJoinRequestReply.Ignore, (Result a) => { string ss = "Lorum Ipsum"; _ = ss; });
            };
            am.OnActivityInvite += (Discord.ActivityActionType a, ref Discord.User b, ref Discord.Activity c) => 
            {
                am.SendInvite(b.Id, a, "Hi! This is a testing program!", (Discord.Result d) => { string ss = "Lorum Ipsum"; _ = ss; });
            };
            am.UpdateActivity(activity, (result) =>
            {
                if (result == Discord.Result.Ok)
                {
                    w("Successfully updated activity!");
                }
                else
                {
                   w("Failed to update activity...");
                }
            });
            w("Running main loop");
            while (true)
            {
                int i = 0;
                while (i < 60)
                {
                    try
                    {
                        discord.Run();
                    }
                    catch
                    {
                        w("Callbacks failed. Press any key to continue.");
                        Console.ReadLine();
                    }
                    await Task.Delay(1000);
                    i++;
                }
                w("Changing Activity...");
                s1 = GetState([s1, s2, s3, s4]);
                s2 = GetState([s1, s2, s3, s4]);
                s3 = GetState([s1, s2, s3, s4]);
                s4 = GetState([s1, s2, s3, s4]);
                i1 = GetImage([i1]);
                i2 = GetImage([i1, i2]);
                p1 = rnd.Next(0, 900);
                p2 = rnd.Next(p1, 999);
                w($"State: {s1}");
                w($"Description: {s2}");
                w($"Large Tool tip: {s3}");
                w($"Small tool tip: {s4}");
                w($"Large Image: {i1}");
                w($"Small Image: {i2}");
                w($"Current: {p1}");
                w($"Max: {p2}");
                activity = new Discord.Activity
                {
                    State = s1,
                    Details = s2,
                    Type = ActivityType.Watching,
                    Timestamps =
                  {
                      Start = start,
                      End = end,
                  },
                    Assets =
                  {
                      LargeImage = i1,
                      LargeText = s3,
                      SmallImage = i2,
                      SmallText = s4,
                  },
                    Party =
                  {
                      Id = "randomID",
                      Size = {
                          CurrentSize = p1,
                          MaxSize = p2,
                      },
                  },

                    Secrets =
                  {

                  },
                    Instance = true,
                };
                am.UpdateActivity(activity, (result) =>
                {
                    if (result == Discord.Result.Ok)
                    {
                        w("Successfully updated activity!");
                    }
                    else
                    {
                        w("Failed to update activity...");
                    }
                });
            }
        }
    }
}
