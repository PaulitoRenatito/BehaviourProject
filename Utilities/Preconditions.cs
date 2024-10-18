using System;

namespace Utilities
{
    public class Preconditions
    {
        Preconditions() { }

        public static T CheckNotNull<T>(T reference, string message = null)
        {
            if (reference is UnityEngine.Object obj && obj.OrNull() == null)
            {
                throw new ArgumentException(message);
            }
            if (reference is null) {
                throw new ArgumentNullException(message);
            }
            return reference;
        }
    }
}
