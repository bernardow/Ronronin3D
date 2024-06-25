using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class Spores : MonoBehaviour, IObserver
    {
        [SerializeField] private Transform _fungiCenter;
        [SerializeField] private List<GameObject> _sporePool = new List<GameObject>();
        [SerializeField] private float _shotForce;
        [SerializeField] private float _duration = 12;

        private bool _isShooting;

        private void Start()
        {
            foreach (GameObject spore in GameObject.FindGameObjectsWithTag("Projectiles"))
            {
                _sporePool.Add(spore);
                spore.SetActive(false);
            }
                
        }

        private void Update()
        {
            if (_isShooting)
                RotateCenter();
        }

        public void ActivateSpore(GameObject spore)
        {
            spore.SetActive(true);
        }

        private void GetSpores(Vector3 direction)
        {
            foreach (GameObject spore in _sporePool)
            {
                if (!spore.activeSelf)
                {
                    ActivateSpore(spore);
                    spore.transform.position = _fungiCenter.position;
                    Rigidbody newSporeRb = spore.GetComponent<Rigidbody>();
                    newSporeRb.AddForce(direction * _shotForce, ForceMode.Impulse);
                    break;
                }
            }
        }

        private void RotateCenter() => _fungiCenter.Rotate(-Vector3.up * 10 * Time.deltaTime);

        private IEnumerator Shooter()
        {
            for (int i = 0; i < 4; i++)
            {
                GetSpores(_fungiCenter.forward);
                GetSpores(-_fungiCenter.forward);
                GetSpores(_fungiCenter.right);
                GetSpores(-_fungiCenter.right);
                yield return new WaitForSeconds(3f);
            }
        }
        
        public IEnumerator Run()
        {
            _isShooting = true;
            StartCoroutine(Shooter());
            yield return new WaitForSeconds(_duration);
            _isShooting = false;
        }
        
        public void OnNotify()
        {
            StartCoroutine(Run());
        }

        public void Disable()
        {
            enabled = false;
            StopAllCoroutines();
        }

        public void Enable()
        {
            enabled = true;
        }
    }
}
