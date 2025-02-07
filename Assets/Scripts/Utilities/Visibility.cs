using UnityEngine;

namespace Utilities
{
    public static class Visibility
    {
        public static void Show(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        public static void Hide(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
