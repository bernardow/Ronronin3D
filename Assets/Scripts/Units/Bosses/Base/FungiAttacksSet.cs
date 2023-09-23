using System.Collections.Generic;
using UnityEngine;

namespace Units.Bosses.Base
{
    [CreateAssetMenu(menuName = "Bosses/AttackSetFungi", fileName = "NewFungiAttackSet")]
    public class FungiAttacksSet : ScriptableObject, IAttackSet
    {
        [SerializeField] private int iD;
        [SerializeField] private string setName;

        public List<FungiUtilities.FungiAttacks> AttacksList = new List<FungiUtilities.FungiAttacks>();

        public int ID
        {
            get => iD;
            set => value = iD;
        }

        public string SetName { get; set; }
    }

    public interface IAttackSet
    {
        public int ID { get; set; }
        public string SetName { get; set; }
    }
}
