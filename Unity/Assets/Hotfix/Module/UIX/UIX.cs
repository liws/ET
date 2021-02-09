using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIXAwakeSystem : AwakeSystem<UIX, string>
    {
        public override void Awake(UIX self, string name)
        {
            self.Awake(name);
        }
    }
    public class UIX:Entity
    {
		public string Name { get; private set; }

        ResourcesComponent resourcesComponent;
        public void Awake(string name)
        {
            resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            this.Name = name;
        }

        public async ETTask LoadAsset()
        {
            await resourcesComponent.LoadBundleAsync("bundleName");
            var prefab = (GameObject)resourcesComponent.GetAsset("bundleName", Name);
            var gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            gameObject.AddComponent<ComponentView>().Component = this;
            gameObject.layer = LayerMask.NameToLayer(LayerNames.UI);
            gameObject.GetComponent<Canvas>().worldCamera = Game.Scene.GetComponent<UIXComponent>().Camera;
            this.GameObject = gameObject;
        }

        public void OnOpen()
        {
            this.GetComponent<UIXFormBase>().OnOpen();
        }

        public void OnClose()
        {
            this.GetComponent<UIXFormBase>().OnClose();
        }
    }
}
