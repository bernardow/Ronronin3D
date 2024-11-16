using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Bosses.Base
{
    [Serializable]
    public class FungiAttacksSet : IAttackSet
    {
        [SerializeField] private int iD;
        [SerializeField] private string setName;

        public List<FungiUtilities.FungiAttacks> AttacksList = new List<FungiUtilities.FungiAttacks>();

        public int ID
        {
            get => iD;
            set => iD = value;
        }

        public string SetName { get; set; }
    }
}
