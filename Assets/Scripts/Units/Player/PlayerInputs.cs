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
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public Vector3 MovementDirection { get; private set; }
        
        
        
        public event Action OnFireSpecialAttack = delegate {  }; 
        public event Action OnFireKunai = delegate {  };

        private void Update()
        {
            FireJoystick = Input.GetKeyDown(KeyCode.Joystick1Button0);
            FireKeyboard = Input.GetKeyDown(KeyCode.Space);
            FireKunaiJoystick = Input.GetKeyDown(KeyCode.Joystick1Button3);
            FireKunaiKeyboard = Input.GetKeyDown(KeyCode.K);
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            MovementDirection = new Vector3(Horizontal, 0, Vertical);
            
            if (FireJoystick || FireKeyboard)
                OnFireSpecialAttack.Invoke(); 
            
            if(FireKunaiJoystick || FireKunaiKeyboard)
                OnFireKunai.Invoke();
                
        }
    }
}
