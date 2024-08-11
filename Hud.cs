using System;
using System.Net.Mail;
using BepInEx;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
namespace InfoGUI
{
    // Token: 0x02000002 RID: 2
    [BepInPlugin("CCHUD", "CCHUD", "1.0.0")]
    public class NotifiLib : BaseUnityPlugin
    {

        // Token: 0x06000001 RID: 1 RVA: 0x00002080 File Offset: 0x00000280
        private void Init()
        {
            
            
                this.MainCamera = GameObject.Find("Main Camera");
                this.HUDObj = new GameObject();
                this.HUDObj2 = new GameObject();
                
                this.HUDObj.AddComponent<Canvas>();
                this.HUDObj.AddComponent<CanvasScaler>();
                this.HUDObj.AddComponent<GraphicRaycaster>();
                this.HUDObj.GetComponent<Canvas>().enabled = true;
                this.HUDObj.GetComponent<Canvas>().worldCamera = this.MainCamera.GetComponent<Camera>();
                this.HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
                this.HUDObj.GetComponent<RectTransform>().position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);
                this.HUDObj2.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z - 4.6f);
                this.HUDObj.transform.parent = this.HUDObj2.transform;
                this.HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
                Vector3 eulerAngles = this.HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
                eulerAngles.y = -270f;
                this.HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
                this.HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
                this.Testtext = new GameObject
                
                
                {
                    transform =
                {
                    parent = this.HUDObj.transform
                }
                }.AddComponent<Text>();
                this.Testtext.text = "";
                this.Testtext.color = Color.yellow;
                this.Testtext.fontSize = 25;
                this.Testtext.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
                this.Testtext.rectTransform.sizeDelta = new Vector2(450f, 210f);
                this.Testtext.alignment = TextAnchor.UpperLeft;
                this.Testtext.rectTransform.localScale = new Vector3(0.0033333334f, 0.0033333334f, 0.33333334f);
                this.Testtext.rectTransform.localPosition = new Vector3(-1f, -0.05f, -0.5f);
                this.Testtext.material = this.AlertText;
                NotifiLib.NotifiText = this.Testtext;

             
            
        }
        // Token: 0x06000002 RID: 2 RVA: 0x00002374 File Offset: 0x00000574
        private void FixedUpdate()
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                /*if (player.NickName == "CC")
                {
                    this.Ranked = false;
                }*/
                if (player.NickName == "CCSCRIM")

                {
                    this.Ranked = true;
                }
            }

            if (this.timer)
            {
                this.elapsedTime += Time.deltaTime;
            }
            this.num1 = Mathf.FloorToInt(this.elapsedTime / 60f);
            this.num2 = Mathf.FloorToInt(this.elapsedTime % 60f);
            float num = this.elapsedTime * 1000f;
            num %= 1000f;
            string text = string.Format("{0:00}:{1:00}:{2:000}", this.num1, this.num2, num);
            if (!PhotonNetwork.InRoom)
            {
                this.elapsedTime = 0f;
            }
            if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
            {
                this.elapsedTime = 0f;
                this.timer = false;
            }
            if (!GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
            {
                this.timer = true;
            }
            int num2 = 0;
            GorillaTagManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Tag Manager").GetComponent<GorillaTagManager>();
            foreach (Player item in PhotonNetwork.PlayerList)
            {
                if (!component.currentInfected.Contains(item))
                {
                    num2++;
                }
            }
            this.deltaTime2 += (Time.unscaledDeltaTime - this.deltaTime2) * 0.1f;
            float f = 1f / this.deltaTime2;
            if (!this.HasInit && GameObject.Find("Main Camera") != null)
            {
                this.Init();
                this.HasInit = true;
            }
            this.HUDObj2.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y, this.MainCamera.transform.position.z);
            this.HUDObj2.transform.rotation = this.MainCamera.transform.rotation;

            
             //if ( PhotonNetwork.InRoom)
             //{
                if (hudEnable)
                {
                    this.Testtext.text = string.Concat(new string[]

                    {
                    "\n<color=#FF0000>Ba</color><color=#FF8700>n</color><color=#FFFB00>a</color><color=#0FFF00>na</color><color=#0036FF>OS</color><color=#B600FF>u</color><color=#FF00B6>i</color>",
                    "\nFPS: ", Mathf.Ceil(f).ToString(),
                    "\nPeople Tagged: ", component.currentInfected.ToArray().Length.ToString(),
                    "\nPeople Alive: ", num2.ToString(),
                    "\nRun Time: ",text,

                     });

                }
                else
                {
                    this.Testtext.text = null;

                    {
                     };
                }
                
             //}
            
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000207B File Offset: 0x0000027B
        private void awake()
        {
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000266C File Offset: 0x0000086C
        public static void SendNotification(string NotificationText)
        {
            if (NotifiLib.IsEnabled)
            {
                if (!NotificationText.Contains(Environment.NewLine))
                {
                    NotificationText += Environment.NewLine;
                }
                NotifiLib.NotifiText.text = NotifiLib.NotifiText.text + NotificationText;
                NotifiLib.PreviousNotifi = NotificationText;
            }
        }

        // Token: 0x04000001 RID: 1
        
        private GameObject HUDObj;

        // Token: 0x04000002 RID: 2
        private GameObject HUDObj2;

        private GameObject box;

        // Token: 0x04000003 RID: 3
        private GameObject MainCamera;

        // Token: 0x04000004 RID: 4
        private Text Testtext;

        // Token: 0x04000005 RID: 5
        private Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

        // Token: 0x04000006 RID: 6
        private string[] Notifilines;

        // Token: 0x04000007 RID: 7
        private string newtext;

        // Token: 0x04000008 RID: 8
        public static string PreviousNotifi;

        // Token: 0x04000009 RID: 9
        private bool HasInit;

        // Token: 0x0400000A RID: 10
        private static Text NotifiText;

        // Token: 0x0400000B RID: 11
        public static bool IsEnabled = true;

        // Token: 0x0400000C RID: 12
        private float elapsedTime;

        // Token: 0x0400000D RID: 13
        private float deltaTime2;

        // Token: 0x0400000E RID: 14
        public static bool doonce = true;

        // Token: 0x0400000F RID: 15
        public int num1;

        // Token: 0x04000010 RID: 16
        public int num2;

        // Token: 0x04000011 RID: 17
        public bool timer;

        // Token: 0x04000012 RID: 18
        public int num3;

        // Token: 0x04000013 RID: 19
        public bool Ranked;

        public static bool hudEnable;
    }
}