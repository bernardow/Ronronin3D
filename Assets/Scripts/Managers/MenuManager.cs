using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _optionsScreen;
        [SerializeField] private GameObject _menuScreen;
        [SerializeField] private RectTransform _pointer;
        
        private Dictionary<int, Vector3> selectorDictionary = new Dictionary<int, Vector3>();
        private int _indexer;
        
        private void Start()
        {
            selectorDictionary.Add(0, new Vector3(0, 205, 0));
            selectorDictionary.Add(1, Vector3.zero);
            selectorDictionary.Add(2, new Vector3(0, -205, 0));
        }

        private void Update()
        {
            _pointer.position = selectorDictionary[_indexer];
            CheckForArrowUpdate();
        }

        private void CheckForArrowUpdate()
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                _indexer++;
            }
        }

        public void ChangeScene(int index) => SceneManager.LoadScene(index);

        public void DisplayOptions()
        {
            _optionsScreen.SetActive(!_optionsScreen.activeSelf);
            _menuScreen.SetActive(!_menuScreen.activeSelf);
        }
    }
}
