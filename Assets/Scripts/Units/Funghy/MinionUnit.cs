using System.Collections;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Units.Funghy
{
    public class MinionUnit : MonoBehaviour
    {
        [SerializeField] private float _minionSpeed;
        [SerializeField] private float _lifeBonus = 5;
        [SerializeField] private float _lifeTime = 20f;
    
        private Transform _minionTransform;
        private BaseUnit _baseUnit;
        private Player.Player _player;

        private void OnDestroy()
        {
            int randomNum = Random.Range(0, 5);
            if(randomNum == 1)
                _player.PlayerHealth.AddLife(_lifeBonus);
        }

        private  void  Start()
        {
            _minionTransform = transform;
            _baseUnit = GetComponent<BaseUnit>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            StartCoroutine(SetLifeTime(_lifeTime));
        }

        private void Update()
        {
            Vector3 position = _minionTransform.position;
            position += (Helpers.GetPlayerPosition()! - position).normalized * _minionSpeed * Time.deltaTime; 
            _minionTransform.position = position;
        }

        private IEnumerator SetLifeTime(float timer)
        {
            yield return new WaitForSeconds(timer);
            _baseUnit.Kill();
        }
    }
}
