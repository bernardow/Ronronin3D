using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class Funghy : MonoBehaviour, IBoss
    {
        private FungiStates _currentState;
        private List<FungiAttacks> _currentAtacks = new List<FungiAttacks>();
        
        public BaseUnit FunghyHealth { get; private set; }
        private Spores _spores;
        private SporeCloud _sporeCloud;
        private FungiDash _fungiDash;
        private AcidRain _acidRain;
        private FungiMinions _fungiMinions;

        private ObservableObject _observableObject;
        
        private void Start()
        {
            FunghyHealth = GetComponent<BaseUnit>();
            _observableObject = new ObservableObject();
            _spores = GetComponent<Spores>();
            _fungiDash = GetComponent<FungiDash>();
            _sporeCloud = GetComponent<SporeCloud>();
            _acidRain = GetComponent<AcidRain>();
            _fungiMinions = GetComponent<FungiMinions>();
        
            _currentState = FungiStates.PhaseOne;
            CheckForAttacksUpdates();
            
            FungiStateMachine();
        }

        private void FungiStateMachine()
        {
            int attackIndex = _currentAtacks.GetRandomValueInList();
            _observableObject.NotifySingleObserver(attackIndex);
        }

        private void CheckForAttacksUpdates()
        {
            switch (_currentState)
            {
                case FungiStates.PhaseOne: 
                    _currentAtacks.Add(FungiAttacks.CrossShot);
                    _currentAtacks.Add(FungiAttacks.Dash);
                    
                    _observableObject.AddObserver(_spores);
                    _observableObject.AddObserver(_fungiDash);
                    break;
                case FungiStates.PhaseTwo: 
                    _currentAtacks.Add(FungiAttacks.AcidRain);
                    _currentAtacks.Add(FungiAttacks.SporeCloud);
                    
                    _observableObject.AddObserver(_acidRain);
                    _observableObject.AddObserver(_sporeCloud);
                    break;
                case FungiStates.PhaseThree: 
                    _currentAtacks.Add(FungiAttacks.Minions);
                    
                    _observableObject.AddObserver(_fungiMinions);
                    break;
                default: 
                    Debug.LogError("Phase not found");
                    break;
            }
        }

        private enum FungiStates
        {
            PhaseOne,
            PhaseTwo,
            PhaseThree
        }
    
        private enum FungiAttacks
        {
            CrossShot,
            SporeCloud,
            AcidRain,
            Dash,
            Minions
        }

        public void RunStateMachine() => FungiStateMachine();
        public void PhaseChecker()
        {
            if (FunghyHealth.Life > FunghyHealth.InitialLife * 0.66f)
                return;

            if (FunghyHealth.Life > FunghyHealth.InitialLife * 0.33f  && _currentState != FungiStates.PhaseTwo)
            {
                _currentState = FungiStates.PhaseTwo;
                CheckForAttacksUpdates();
                return;
            }

            if (FunghyHealth.Life < FunghyHealth.InitialLife * 0.33f && _currentState != FungiStates.PhaseThree)
            {
                _currentState = FungiStates.PhaseThree;
                CheckForAttacksUpdates();
            }
        }
    }
}