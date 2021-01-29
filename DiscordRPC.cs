using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;

namespace lewd_dot_se_Uploader
{
    class DiscordRPC
    {
        public static void StartDiscordRPC()
        {
            try
            {
                client = new DiscordRpcClient("804551416793530389");
                client.Initialize();
                client.SetPresence(new RichPresence
                {
                    Details = "made by tragedy",
                    Timestamps = Timestamps.Now,
                    Assets = new Assets
                    {
                        LargeImageKey = "1",
                        LargeImageText = "Made By tragedy",

                    }
                }); ;
            }
            catch
            {
            }
        }
        public static void StopDiscordRPC()
        {
            try
            {
                DiscordRPC.client.Dispose();
            }
            catch
            {
            }
        }
        public static DiscordRpcClient client;
    }
}
