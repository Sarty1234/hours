using System.Linq;
using System.IO;
using System;

namespace hours
{
    public class file_manager
    {
        public static file_manager Instance = new file_manager();
        ///home/container/.config/SCP Secret Laboratory/info/
        public static string path = "/home/container/.config/SCP Secret Laboratory/info/data.json";

        public PlayerData[] get_all_data()
        {

            create_if_not_exist_file();
            //"\\home\\container.config\\info\\data.json"
            //"D:\\data.json"
            string json = File.ReadAllText(path);
            PlayerData[] data = string_to_class(json);
            data = data.OrderBy(o => o.PlayTime).ToArray();

            return data;
        }


        public PlayerData get_one_player_data(string _name)
        {
            PlayerData[] all_data = get_all_data();
            PlayerData data = null;
            foreach (PlayerData p in all_data)
            {
                if (p.name == _name)
                {
                    data = p;
                    break;
                }
            }

            return data;
        }


        public void write_all_data(PlayerData[] data)
        {
            create_if_not_exist_file();
            data = data.OrderBy(o => o.PlayTime).ToArray();
            File.WriteAllText(path, class_to_string(data));
        }


        public void delete_file()
        {
            File.Delete(path);
        }


        public void create_if_not_exist_file()
        {
            if (!File.Exists(path))
            {
                PlayerData[] data = new PlayerData[1] { new PlayerData() { name = "Verm465", PlayTime = 99999999999 } };
                File.WriteAllText(path, class_to_string(data));
            }
        }


        public string class_to_string(PlayerData[] data)
        {
            string output = "";

            foreach (PlayerData player in data)
            {
                output += $"{player.name} {player.PlayTime};";
            }
            output = output.Remove(output.Length - 1);

            return output;
        }


        public PlayerData[] string_to_class(string data)
        {
            PlayerData[] output = new PlayerData[0];
            PlayerData playerData = new PlayerData();
            string[] part_1 = data.Split(';');

            foreach (string part in part_1)
            {
                string[] text = part.Split(' ');
                playerData = new PlayerData()
                {
                    name = text[0],
                    PlayTime = Convert.ToDouble(text[1])
                };
                output = output.Append(playerData).ToArray();
            }

            return output;
        }
    }
}