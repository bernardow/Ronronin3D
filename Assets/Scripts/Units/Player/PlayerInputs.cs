using System;
using UnityEngine;

namespace Units.Player
{
    public class PlayerInputs : MonoBehaviour
    {
        public bool FireKeyboard { get; private set; }
        public bool FireJoystick { get; private set; }
        public bool FireKunaiKeyboard { get; private set; }
        public bool FireKunaiJoystick { get; private set; }
        public bool DashKeyboard { get; private set; }
        public bool DashJoystick { get; private set; }
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float HorizontalRaw { get; private set; }
        public float VerticalRaw { get; private set; }
        public Vector3 MovementDirection { get; private set; }
        public Vector3 MovementDirectionRaw { get; private set; }
        
        
        public event Action OnFireSpecialAttack = delegate {  }; 
        public event Action OnFireKunai = delegate {  };
        public event Action OnDashActivate = delegate {  };

        private void Update()
        {
            FireJoystick = Input.GetKeyDown(KeyCode.Joystick1Button0);
            FireKeyboard = Input.GetKeyDown(KeyCode.Space);
            FireKunaiJoystick = Input.GetKey(KeyCode.Joystick1Button3);
            FireKunaiKeyboard = Input.GetKey(KeyCode.K);
            DashKeyboard = Input.GetKeyDown(KeyCode.L);
            DashJoystick = Input.GetKeyDown(KeyCode.Joystick1Button1);
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            HorizontalRaw = Input.GetAxisRaw("Horizontal");
            VerticalRaw = Input.GetAxisRaw("Vertical");

            MovementDirection = new Vector3(Horizontal, 0, Vertical);
            MovementDirectionRaw = new Vector3(HorizontalRaw, 0, VerticalRaw);
            
            if (FireJoystick || FireKeyboard)
                OnFireSpecialAttack.Invoke(); 
            
            if(FireKunaiJoystick || FireKunaiKeyboard)
                OnFireKunai.Invoke();
            
            if(DashJoystick || DashKeyboard)
                OnDashActivate.Invoke();
        }
    }
}
