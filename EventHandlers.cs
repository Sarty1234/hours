using System;
using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace hours
{

    public class EventHandlers
    {
        public bool run = true;
        public void onStart()
        {
            if (run)
            {
                Timing.RunCoroutine(MyCoroutine());
                run = false;
            }
        }


        public void onSpawned(SpawningEventArgs eventArgs)
        {
            if (eventArgs == null) return;
            if (eventArgs.Player != null) return;
            PlayerData player = file_manager.Instance.get_one_player_data(eventArgs.Player.UserId);
            if (player == null) return;
            if (player.PlayTime / 3600 > hours.Instance.Config.hours_for_prefix)
            {
                eventArgs.Player.CustomName = hours.Instance.Config.custom_prefix + eventArgs.Player.Nickname;
            }
        }


        public IEnumerator<float> MyCoroutine()
        {
            for (; ; ) //repeat the following infinitely
            {
                action();
                yield return Timing.WaitForSeconds(hours.Instance.Config.seconds_between_scan); //Tells the coroutine to wait 5 seconds before continuing, since this is at the end of the loop, it effectively stalls the loop from repeating for 5 seconds.
            }
        }


        public void action()
        {
            PlayerData[] playerDatas = file_manager.Instance.get_all_data();
            Console.WriteLine("scan started");

            foreach (Player play in Player.List)
            {
                int id = -1;
                int i = 0;
                foreach (PlayerData data in playerDatas)
                {
                    if (data.name == play.UserId)
                    {
                        id = i;
                        break;
                    }
                    i++;
                }


                if (id == -1)
                {
                    playerDatas = playerDatas.Append(new PlayerData() { name = play.UserId, PlayTime = 0 }).ToArray();
                }
                else
                {
                    playerDatas[id].PlayTime += hours.Instance.Config.seconds_between_scan;
                }
            }
            

            file_manager.Instance.write_all_data(playerDatas);
        }
    }
}

