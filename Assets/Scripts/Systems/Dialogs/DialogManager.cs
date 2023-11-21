using System;
using System.Collections.Generic;
using Systems.Player_Death_Data;
using Units.Player;
using UnityEngine;

namespace Systems.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private List<Dialog> _dialogs = new List<Dialog>();
        [SerializeField] private List<Sprite> _charactersSprites = new List<Sprite>();
        private DialogBox _dialogBox;
        private PlayerInputs _playerInputs;
        public bool RunningDialog { get; set; }
        public bool FinishedSceneDialog { get; set; }
        public bool InsideDialogRange { get; set; }
    
        public enum GameCharacters
        {
            Ryujin,
            OldMan
        }

        private void Awake()
        {
            _dialogBox = GetComponent<DialogBox>();
            _playerInputs = GameObject.FindWithTag("Player").GetComponent<PlayerInputs>();
            _playerInputs.OnInteractPressed += RunDialog;
            FinishedSceneDialog = false;
        }

        private void OnDestroy() => _playerInputs.OnInteractPressed -= RunDialog;

        private void RunDialog()
        {
            if(RunningDialog || FinishedSceneDialog || !InsideDialogRange) return;
            
            _dialogBox.SetDialog(_dialogs[PlayerDeathManager.GetData()], _dialogs[0].dialogContent[0].Character);
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
