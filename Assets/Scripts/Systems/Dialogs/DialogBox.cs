using System;
using TMPro;
using Units.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Dialogs
{
    public class DialogBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogText;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private GameObject _dialogBox;
        private DialogManager _dialogManager;
        private PlayerInputs _playerInputs;

        private Dialog _currentDialog;
        private int _dialogIndex;

        private void Awake()
        {
            _dialogManager = GetComponent<DialogManager>();
            _playerInputs = GameObject.FindWithTag("Player").GetComponent<PlayerInputs>();
            _playerInputs.OnInteractPressed += RunDialog;
        }

        private void OnDestroy() => _playerInputs.OnInteractPressed -= RunDialog;

        public bool SetDialog(Dialog dialog, DialogManager.GameCharacters characterName)
        {
            if (dialog.dialogContent[0] == null) return false;
        
            _dialogBox.SetActive(true);
            _dialogManager.RunningDialog = true;
            _currentDialog = dialog;
            _dialogText.text = dialog.dialogContent[0].DialogLineText;
            _dialogIndex = 0;
            _characterIcon.sprite = _dialogManager.GetSprite(characterName);
            _characterName.text = characterName.ToString();
            return true;
        }

        private void RunDialog()
        {
            if(_currentDialog == null || !_dialogManager.RunningDialog) return;
            if (_dialogIndex + 1 == _currentDialog.dialogContent.Length)
            {
                EndDialog();
                return;
            }
            
            _dialogIndex++;
            _dialogText.text = _currentDialog.dialogContent[_dialogIndex].DialogLineText;
            _characterName.text = _currentDialog.dialogContent[_dialogIndex].Character.ToString();

            _characterIcon.sprite = _dialogManager.GetSprite(_currentDialog.dialogContent[_dialogIndex].Character);
        }

        private void EndDialog()
        {
            _dialogBox.SetActive(false);
            _dialogManager.RunningDialog = false;
            _dialogManager.FinishedSceneDialog = true;
        }
    }
}
