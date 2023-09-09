using System;
using System.Collections.Generic;
using DG.Tweening;
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
            selectorDictionary.Add(0, _pointer.anchoredPosition);
            selectorDictionary.Add(1, Vector3.zero);
            selectorDictionary.Add(2, new Vector3(0, -205, 0));
        }

        private void Update()
        {
            CheckForArrowUpdate();
        }

        private void CheckForArrowUpdate()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _indexer = _indexer == 2 ? 0 : _indexer + 1;
                _pointer.DOAnchorPos(selectorDictionary[_indexer], 1).SetEase(Ease.OutSine);
            }else if (Input.GetKeyDown(KeyCode.W))
            {
                _indexer = _indexer == 0 ? 2 : _indexer - 1;
                _pointer.DOAnchorPos(selectorDictionary[_indexer], 1).SetEase(Ease.OutSine);
            }
            else if(Input.GetKeyDown(KeyCode.KeypadEnter)){
                switch (_indexer)
                {
                    case 0: ChangeScene(1);
                        break;
                    case 1: DisplayOptions();
                        break;
                    case 2: Application.Quit();
                        break;
                    default: Debug.LogError("Index out of scope");
                        break;
                }
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
