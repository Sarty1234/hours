using System;
using CommandSystem;
using Exiled.API.Features;
using System.Linq;

namespace hours
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Commands : ICommand
    {
        public string Command { get; set; } = "getplayerhours";

        public string[] Aliases { get; set; } = { "hours" };

        public string Description { get; set; } = "return played hours";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerData[] data = file_manager.Instance.get_all_data();
            response = "Made by Verm465\n";
            foreach (PlayerData playerData in data)
            {
                response += $"{playerData.name}: {(int)(playerData.PlayTime / 3600)}\n";
            }


            return true;
        }
    }


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Command_2 : ICommand
    {
        public string Command { get; set; } = "getplayerhourstop20";

        public string[] Aliases { get; set; } = { "hourstop20" };

        public string Description { get; set; } = "return played hours for top 20 players";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerData[] data = file_manager.Instance.get_all_data();
            response = "Made by Verm465\n";
            int x = 0;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                PlayerData playerData = data[i];
                if (x >= 20)
                {
                    break;
                }
                response += $"{playerData.name}: {(int)(playerData.PlayTime / 3600)}\n";
                x++;
            }


            return true;
        }
    }


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Command_3 : ICommand
    {
        public string Command { get; set; } = "getplayernamebysteamid";

        public string[] Aliases { get; set; } = { "namebyid" };

        public string Description { get; set; } = "return player name by his steam id";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            if (arguments.Count != 1)
            {
                response = "use it like this: namebyid (steam id)";
                return false;
            } else
            {
                foreach (Player ply in Player.List)
                {
                    if (ply.UserId == arguments.ToArray()[0])
                    {
                        response = ply.CustomName;
                        break;
                    }
                }
            }


            return true;
        }
    }


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Command_4 : ICommand
    {
        public string Command { get; set; } = "getplayerhoursbysteamid";

        public string[] Aliases { get; set; } = { "hoursbyid" };

        public string Description { get; set; } = "return player played hours by his steam id";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            if (arguments.Count != 1)
            {
                response = "use it like this: hoursbyid (steam id)";
                return false;
            }
            else
            {
                response = $"{(int)(file_manager.Instance.get_one_player_data(arguments.ToArray()[0]).PlayTime / 3600)}";
            }


            return true;
        }
    }


    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Command_5 : ICommand
    {
        public string Command { get; set; } = "deleteplayerhours";

        public string[] Aliases { get; set; } = { "del_hours" };

        public string Description { get; set; } = "delete played hours";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            file_manager.Instance.delete_file();
            response = "Made by Verm465\n";


            return true;
        }
    }
}
