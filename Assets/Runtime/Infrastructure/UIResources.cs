using UnityEngine;

namespace Runtime.Infrastructure
{
    public static class UIResources
    {
        public static UIIcon Grab {get; private set;}
        public static UIIcon Interact {get; private set;}
        public static UIIcon DefaultCursor {get; private set;}

        public static void Initialize()
        {
            Grab = UIIcon.Grab();
            Interact = UIIcon.Interact();
            DefaultCursor = UIIcon.Default();
        }
    }
    
    internal static class UIIconPaths
    {
        public const string GrabIcon = "Icons/grab";
        public const string InteractIcon = "Icons/interact";
        public const string DefaultCursor = "Icons/default";
    }

    public class UIIcon
    {
        public Sprite Icon
        {
            get
            {
                if(!_cacheIcon)
                    _cacheIcon = Resources.Load<Sprite>(_cachePath);
                return _cacheIcon;
            }
        }
        private readonly string _cachePath;
        private Sprite _cacheIcon;
        
        public UIIcon(string path)
        {
            _cachePath = path;
        }
        
        public static UIIcon Grab() => new(UIIconPaths.GrabIcon);
        public static UIIcon Interact() => new(UIIconPaths.InteractIcon);
        public static UIIcon Default() => new(UIIconPaths.DefaultCursor);
    }
}