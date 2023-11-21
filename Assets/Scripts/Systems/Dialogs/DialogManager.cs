using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private List<Dialog> _dialogs = new List<Dialog>();
        [SerializeField] private List<Sprite> _charactersSprites = new List<Sprite>();
        private DialogBox _dialogBox;
    
        public enum GameCharacters
        {
            Ryujin,
            OldMan
        }

        private void Awake()
        {
            _dialogBox = GetComponent<DialogBox>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _dialogBox.SetDialog(_dialogs[0], _dialogs[0].dialogContent[0].Character);
            }
        }

        public Sprite GetSprite(GameCharacters character)
        {
            switch (character)
            {
                case GameCharacters.Ryujin: return _charactersSprites[0];
                case GameCharacters.OldMan: return _charactersSprites[1];
                default:
                    throw new ArgumentOutOfRangeException(nameof(character), character, "Character not found");
            }
        }
    }
}
