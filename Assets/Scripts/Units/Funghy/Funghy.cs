using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Utilities;

namespace Units.Funghy
{
    public class Funghy : MonoBehaviour, IBoss
    {
        private FungiStates _currentState;
        private List<FungiAttacks> _currentAtacks = new List<FungiAttacks>();

        public Transform FungiTransform { get; private set; }
        public Rigidbody FungyRigidbody { get; private set; }
        public BaseUnit FunghyHealth { get; private set; }
        private Spores _spores;
        private SporeCloud _sporeCloud;
        private FungiDash _fungiDash;
        private AcidRain _acidRain;
        private FungiMinions _fungiMinions;
        private FungiIdle _fungiIdle;
        public FungiUltimate FungiUltimate;

        private ObservableObject _observableObject;
        
        private void Awake()
        {
            FungiTransform = transform;
            FungyRigidbody = GetComponent<Rigidbody>();
            FunghyHealth = GetComponent<BaseUnit>();
            _observableObject = new ObservableObject();
            _spores = GetComponent<Spores>();
            _fungiDash = GetComponent<FungiDash>();
            _sporeCloud = GetComponent<SporeCloud>();
            _acidRain = GetComponent<AcidRain>();
            _fungiMinions = GetComponent<FungiMinions>();
            _fungiIdle = GetComponent<FungiIdle>();
            _currentState = FungiStates.PhaseOne;
            CheckForAttacksUpdates();
        }

        private void Start() => RunStateMachine();

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
                    _currentAtacks.Add(FungiAttacks.Ultimate);
                    
                    _observableObject.AddObserver(_acidRain);
                    _observableObject.AddObserver(_sporeCloud);
                    _observableObject.AddObserver(FungiUltimate);
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
            Minions,
            Ultimate
        }

        public void RunStateMachine()
        {
            ManageIdleMovement();
            FungiStateMachine();
        }

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

        public void ManageIdleMovement() => _fungiIdle!.enabled = !_fungiIdle!.enabled;
    }
}