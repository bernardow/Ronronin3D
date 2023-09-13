using System;

namespace Units.Player
{
    public class OnCollisionArgs : EventArgs
    {
        public BaseUnit Collider { get; set; }
    }
}