using UnityEngine;

namespace Systems.Dialogs
{
    public class DialogPoint : MonoBehaviour
    {
        [SerializeField] private DialogManager _dialogManager;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _dialogManager.InsideDialogRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _dialogManager.InsideDialogRange = false;
        }
    }
}
