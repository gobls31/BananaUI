using System.Text;
using UnityEngine;
using BananaOS;
using Photon.Pun;
using GorillaNetworking;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Contracts;
using BepInEx.Bootstrap;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using GorillaComputer.Model;
using HarmonyLib;
using InfoGUI;
using GorillaLocomotion;
using Utilla;
using Pluginthings;
using static  Pluginthings.Plugin;




namespace BananaOS.Pages
{
    public class ManagerPage : WatchPage
    {
        void Awake()
        {
            MonkeWatch.RegisterPage(typeof(ManagerPage));
        }


        //What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => "Bam";

        //Enabling will display your page on the main menu if you're nesting pages you should set this to false
        public override bool DisplayOnMainMenu => true;
        //important
        string index = "selectionHandler.currentIndex";




        public void Update()
        {


        }

        public static bool Punchy;
        public static bool STOPP;






        //This method will be ran after the watch is completely setup
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 2;


        }

        //What you return is what is drawn to the watch screen the screen will be updated everytime you press a button

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            bool flag3 = !Pluginthings.Plugin.Modded;


            if (Modded)
            { stringBuilder.AppendLine("Integrations"); 

            }
            else
            {
                stringBuilder.AppendLine("MODDED ONLY");
            }
            

            bool flag = InfoGUI.NotifiLib.hudEnable;
            if (flag)
            {
                stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(0, "Disable Hud"));
            }
            else
            {
                stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(0, "Enable Hud"));
            }
            if (Modded)
            {
                bool flag1 = Punchy;

                if (flag1)
                {
                    stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(1, "Disable Punchy"));
                }
                else
                {

                    stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(1, "Enable Punchy"));
                }

                bool flag2 = STOPP;

                if (flag1)
                {
                    stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(2, "Disable Freeze"));
                }
                else
                {

                    stringBuilder.AppendLine(this.selectionHandler.GetOriginalBananaOSSelectionText(2, "Enable Freeze"));
                }
            }
           
            return stringBuilder.ToString();

        }








        public override void OnButtonPressed(WatchButtonType buttonType)
        {

            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;



                case WatchButtonType.Enter:
                    bool flag = !Pluginthings.Plugin.Modded;


                    if (selectionHandler.currentIndex == 0)
                    {
                        if (InfoGUI.NotifiLib.hudEnable)
                        {
                            InfoGUI.NotifiLib.hudEnable = false;
                        }
                        else
                        {
                            InfoGUI.NotifiLib.hudEnable = true;

                        }
                    }

                    if (Modded)
                    {
                        if (selectionHandler.currentIndex == 1)
                        {
                            if (Punchy)
                            {

                                Punchy = false;
                            }
                            else
                            {
                                BetterPunchMod();
                                Punchy = true;

                            }
                        }
                        if (selectionHandler.currentIndex == 2)
                        {
                            if (STOPP)
                            {

                                STOPP = false;
                            }
                            else
                            {
                                STOP();
                                STOPP = true;

                            }
                        }
                    }
                    break;





                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;



            }
        }




        /*public static bool IsModded()
        {
            return ;
        }*/



        public static Vector3[] lastLeft = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        public static Vector3[] lastRight = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        public static void BetterPunchMod()
        {

            if (Punchy)
            {
                int index = -1;
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        index++;

                        Vector3 they = vrrig.rightHandTransform.position;
                        Vector3 notthem = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
                        float distance = Vector3.Distance(they, notthem);

                        if (distance < 0.25)
                        {
                            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.Normalize(vrrig.rightHandTransform.position - lastRight[index]) * 10f;
                        }
                        lastRight[index] = vrrig.rightHandTransform.position;

                        they = vrrig.leftHandTransform.position;
                        distance = Vector3.Distance(they, notthem);

                        if (distance < 0.25)
                        {
                            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.Normalize(vrrig.leftHandTransform.position - lastLeft[index]) * 10f;
                        }
                        lastLeft[index] = vrrig.leftHandTransform.position;
                    }
                }
            }
        }

        public static void STOP()
        {

            if (STOPP)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    int index = -1;
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        index++;
                        Vector3 vrrighand = vrrig.rightHandTransform.position;
                        Vector3 me = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
                        float distance = Vector3.Distance(vrrighand, me);

                        if (distance < 0.25)
                        {
                            float d = 0.09f;
                            Vector3 position = GorillaTagger.Instance.offlineVRRig.transform.position;
                            Vector3[] array = new Vector3[]
                            {
                position + GorillaTagger.Instance.offlineVRRig.transform.forward * d,
                position - GorillaTagger.Instance.offlineVRRig.transform.forward * d,
                position + GorillaTagger.Instance.offlineVRRig.transform.right * d,
                position - GorillaTagger.Instance.offlineVRRig.transform.right * d,
                position + GorillaTagger.Instance.offlineVRRig.transform.forward * d
                            };
                            int num = 0;
                            foreach (MonkeyeAI monkeyeAI in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
                            {
                                num++;
                                monkeyeAI.gameObject.transform.position = array[num];
                            }
                        }
                        lastRight[index] = vrrig.rightHandTransform.position;

                        vrrighand = vrrig.leftHandTransform.position;
                        distance = Vector3.Distance(vrrighand, me);

                        if (distance < 0.25)
                        {
                            float d = 0.09f;
                            Vector3 position = GorillaTagger.Instance.offlineVRRig.transform.position;
                            Vector3[] array = new Vector3[]
                            {
                position + GorillaTagger.Instance.offlineVRRig.transform.forward * d,
                position - GorillaTagger.Instance.offlineVRRig.transform.forward * d,
                position + GorillaTagger.Instance.offlineVRRig.transform.right * d,
                position - GorillaTagger.Instance.offlineVRRig.transform.right * d,
                position + GorillaTagger.Instance.offlineVRRig.transform.forward * d
                            };
                            int num = 0;
                            foreach (MonkeyeAI monkeyeAI in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
                            {
                                num++;
                                monkeyeAI.gameObject.transform.position = array[num];
                            }
                        }
                        lastLeft[index] = vrrig.leftHandTransform.position;

                    }
                }

            }
        }

    }
}
