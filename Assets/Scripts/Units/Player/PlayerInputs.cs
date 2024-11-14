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
        public bool MeleeAttackKeyboard { get; private set; }
        public bool MeleeAttackJoystick { get; private set; }
        public bool InteractKeyboard { get; private set; }
        public bool InteractJoystick { get; private set; }
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float HorizontalRaw { get; private set; }
        public float VerticalRaw { get; private set; }
        public Vector3 MovementDirection { get; private set; }
        public Vector3 MovementDirectionRaw { get; private set; }
        public Vector3 SecondaryJoystickDirection { get; private set; }
        public Vector3 SecondatyJoystickDirectionAligned { get; private set; }

        public event Action OnFireSpecialAttack = delegate {  }; 
        public event Action OnFireKunai = delegate {  };
        public event Action OnDashActivate = delegate {  };
        public event Action OnBasicAttack = delegate {  };
        public event Action OnInteractPressed = delegate {  };

        private Camera camera => Camera.main;
        
        private void Update()
        {
            FireJoystick = Input.GetKeyDown(KeyCode.Joystick1Button7);
            FireKeyboard = Input.GetKeyDown(KeyCode.Space);
            FireKunaiJoystick = Input.GetKey(KeyCode.Joystick1Button5);
            FireKunaiKeyboard = Input.GetKey(KeyCode.Mouse0);
            DashKeyboard = Input.GetKeyDown(KeyCode.LeftShift);
            DashJoystick = Input.GetKeyDown(KeyCode.Joystick1Button4);
            MeleeAttackKeyboard = Input.GetKeyDown(KeyCode.Mouse1);
            MeleeAttackJoystick = Input.GetKeyDown(KeyCode.Joystick1Button6);
            InteractKeyboard = Input.GetKeyDown(KeyCode.E);
            InteractJoystick = Input.GetKeyDown(KeyCode.Joystick1Button3);
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            HorizontalRaw = Input.GetAxisRaw("Horizontal");
            VerticalRaw = Input.GetAxisRaw("Vertical");

            MovementDirection = new Vector3(Horizontal, 0, Vertical);
            MovementDirectionRaw = new Vector3(HorizontalRaw, 0, VerticalRaw);

            float secondaryJoystickHorizontal = Input.GetAxis("SecondaryJoystickHorizontal");
            float secondaryJoystickVertical = Input.GetAxis("SecondaryJoystickVertical");
            SecondaryJoystickDirection = new Vector3(secondaryJoystickHorizontal, 0, -secondaryJoystickVertical);

            Vector3 cameraRight = camera.transform.right * SecondaryJoystickDirection.x;
            Vector3 cameraUp = camera.transform.up * SecondaryJoystickDirection.z;
            SecondatyJoystickDirectionAligned = cameraRight + cameraUp;
            SecondatyJoystickDirectionAligned = new Vector3(SecondatyJoystickDirectionAligned.x, 0, SecondatyJoystickDirectionAligned.z);
            
            if (FireJoystick || FireKeyboard)
                OnFireSpecialAttack.Invoke(); 
            
            if(FireKunaiJoystick || FireKunaiKeyboard)
                OnFireKunai.Invoke();
            
            if(DashJoystick || DashKeyboard)
                OnDashActivate.Invoke();
            
            if(MeleeAttackJoystick || MeleeAttackKeyboard)
                OnBasicAttack.Invoke();

            if (InteractJoystick || InteractKeyboard)
                OnInteractPressed.Invoke();
        }
    }
}
