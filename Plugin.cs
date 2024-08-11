using BananaOS;
using BananaOS.Pages;
using BepInEx;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manager;
using BananaOS;
using BepInEx.Bootstrap;
using HarmonyLib;
using Utilla;


namespace Pluginthings

{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(Manager.PluginInfo.GUID, Manager.PluginInfo.Name, Manager.PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002679 File Offset: 0x00000879
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            Plugin.Modded = true;
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002682 File Offset: 0x00000882
        public void OnDisable()
        {
            
            Plugin.Modded = false;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x000026BB File Offset: 0x000008BB
        public void OnGameInitialized()
        {
            
            Plugin.Modded = false;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x000026F4 File Offset: 0x000008F4
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            
            Plugin.Modded = false;
        }

        // Token: 0x0400000A RID: 10
        public static bool Modded;
    }
}

   
