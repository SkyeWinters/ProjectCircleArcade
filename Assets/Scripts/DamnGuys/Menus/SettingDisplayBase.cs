using UnityEngine;

namespace DamnGuys.Menus
{
    public abstract class SettingDisplayBase : MonoBehaviour
    {
        public abstract void Show(GameSetting setting);
        public abstract void Hide();
        public virtual void OnLeft() {}
        public virtual void OnRight() {}
        public virtual void OnInput() {}
    }
}