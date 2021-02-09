using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;


namespace ETHotfix
{
    [ObjectSystem]
    public class UIXComponentAwakeSystem : AwakeSystem<UIXComponent>
    {
        public override void Awake(UIXComponent self)
        {
            self.CameraGO = Component.Global.transform.Find("UICamera").gameObject;
            self.Camera = self.CameraGO.GetComponent<Camera>();
        }
    }
    
    public class UIXComponent:Component
    {
		private readonly Dictionary<string, UIX> activityUIs = new Dictionary<string, UIX>();
        private readonly Dictionary<string, UIX> closedUIs = new Dictionary<string, UIX>();//todo:pool manager
        //todo:uigroup

        public GameObject CameraGO;
        public Camera Camera;
        
        public async ETTask OpenUI<T>(string name) where T : UIXFormBase
        {
            if (this.activityUIs.ContainsKey(name))
                return;

            if (this.closedUIs.TryGetValue(name , out var ui))
            {
                activityUIs[name] = ui;
                this.closedUIs.Remove(name);
                ui.OnClose();
                return;
            }

            ui = ComponentFactory.Create<UIX, string>(name, false);
            this.activityUIs.Add(name, ui);
            
            await ui.LoadAsset();
            ui.AddComponent(typeof(T));
            ui.OnOpen();
        }

        public void CloseUI(string name)
        {
            if (!this.activityUIs.TryGetValue(name, out UIX ui))
            {
                return;
            }
            this.closedUIs[name] = ui;
            ui.OnClose();
        }
    }
}
