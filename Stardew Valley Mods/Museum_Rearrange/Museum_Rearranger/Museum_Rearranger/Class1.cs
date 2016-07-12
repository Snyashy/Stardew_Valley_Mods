﻿using System;
using StardewValley;
using StardewModdingAPI;
using System.IO;
namespace Museum_Rearranger
{
    public class Class1 : Mod
    {
        string key_binding = "R";
        bool game_loaded = false;

        public override void Entry(params object[] objects)
        {
            //set up all of my events here
            StardewModdingAPI.Events.PlayerEvents.LoadedGame += PlayerEvents_LoadedGame;
            StardewModdingAPI.Events.ControlEvents.KeyPressed += ControlEvents_KeyPressed;
        }

        public void ControlEvents_KeyPressed(object sender, StardewModdingAPI.Events.EventArgsKeyPressed e)
        {
            if (Game1.player == null) return;
            if (Game1.player.currentLocation == null) return;
            if (game_loaded == false) return;

            if (e.KeyPressed.ToString() == key_binding) //if the key is pressed, load my cusom save function
            {
                if (Game1.activeClickableMenu != null) return;
                if (StardewValley.Game1.player.currentLocation.name == "ArchaeologyHouse") Game1.activeClickableMenu = new StardewValley.Menus.MuseumMenu();
                else Log.Info("You can't rearrange the museum here!");
           }
        }

        public void PlayerEvents_LoadedGame(object sender, StardewModdingAPI.Events.EventArgsLoadedGameChanged e)
        {
            game_loaded = true;
            DataLoader_Settings();
            MyWritter_Settings();
        }

        void DataLoader_Settings()
        {
            //loads the data to the variables upon loading the game.
            string myname = StardewValley.Game1.player.name;
            string mylocation = Path.Combine(PathOnDisk, "Museum_Rearrange_Config");
            string mylocation2 = mylocation;
            string mylocation3 = mylocation2 + ".txt";
            if (!File.Exists(mylocation3)) //if not data.json exists, initialize the data variables to the ModConfig data. I.E. starting out.
            {
                key_binding = "R";
            }

            else
            {
                string[] readtext = File.ReadAllLines(mylocation3);
                key_binding = Convert.ToString(readtext[3]);
            }
        }

        void MyWritter_Settings()
        {
            //write all of my info to a text file.
            string myname = StardewValley.Game1.player.name;
            string mylocation = Path.Combine(PathOnDisk, "Museum_Rearrange_Config");
            string mylocation2 = mylocation;
            string mylocation3 = mylocation2 + ".txt";
            string[] mystring3 = new string[20];
            if (!File.Exists(mylocation3))
            {
                Log.Info("Museum Rearranger: Config not found. Creating it now.");

                mystring3[0] = "Config: Museum_Rearranger. Feel free to mess with these settings.";
                mystring3[1] = "====================================================================================";
                mystring3[2] = "Key binding for rearranging the museum.";
                mystring3[3] = key_binding.ToString();
                File.WriteAllLines(mylocation3, mystring3);
            }
            else
            {
                //write out the info to a text file at the end of a day. This will run if it doesnt exist.
                mystring3[0] = "Config: Save_Anywhere Info. Feel free to mess with these settings.";
                mystring3[1] = "====================================================================================";
                mystring3[2] = "Key binding for rearranging the museum.";
                mystring3[3] = key_binding.ToString();
                File.WriteAllLines(mylocation3, mystring3);
            }
        }
    }
}
//end class