using UnityEngine;

namespace Runtime.Infrastructure
{
    public static class UIResources
    {
        public static UIIcon Grab => new UIIcon(UIIconPaths.GrabIcon);
        public static UIIcon Interact => new UIIcon(UIIconPaths.InteractIcon);
    }
    
    internal static class UIIconPaths
    {
        public const string GrabIcon = "Icons/GrabIcon";
        public const string InteractIcon = "Icons/InteractIcon";
    }

    public class UIIcon
    {
        public Sprite Icon
        {
            get
            {
                if(cache_Icon == null)
                    Resources.Load<Sprite>("GrabIcon");
                return cache_Icon;
            }
        }
        private string cache_Path;
        private static Sprite cache_Icon;
        
        public UIIcon(string path)
        {
            cache_Path = path;
        }
    }
}