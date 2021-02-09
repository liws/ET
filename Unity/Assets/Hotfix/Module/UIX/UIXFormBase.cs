using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    public class UIXFormBase : Component
    {
        public virtual void OnOpen()
        {
            SetVisible(true);
        }

        public virtual void OnClose()
        {
            SetVisible(false);
        }

        public virtual void SetVisible(bool bVisible)
        {
            
        }
    }
}

