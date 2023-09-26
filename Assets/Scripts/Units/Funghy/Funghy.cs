using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using FungiAttacksSet = Units.Bosses.Base.FungiAttacksSet;

namespace Units.Funghy
{
    public class Funghy : MonoBehaviour, IBoss
    {
        [SerializeField] private List<FungiAttacksSet> _attacksSets = new List<FungiAttacksSet>();
        private Utilities.Queue<IObserver> _attackQueue;
        private int _minAttackSetRange;
        private int _maxAttackRange = 2;
        
        private FungiStates _currentState;
        //private List<FungiUtilities.FungiAttacks> _currentAtacks = new List<FungiUtilities.FungiAttacks>();

        public Transform FungiTransform { get; private set; }
        public Transform FungiCenter { get; private set; }
        public Rigidbody FungyRigidbody { get; private set; }
        public BaseUnit FunghyHealth { get; private set; }
        public VulnerableState VulnerableState { get; private set; }
        private Spores _spores;
        private SporeCloud _sporeCloud;
        private FungiDash _fungiDash;
        private AcidRain _acidRain;
        private FungiMinions _fungiMinions;
        private FungiIdle _fungiIdle;
        public FungiUltimate FungiUltimate;

        private List<IObserver> _attacksComponents = new List<IObserver>();

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
            FungiCenter = transform.GetChild(1);
            VulnerableState = GetComponent<VulnerableState>();

            _attackQueue = new Utilities.Queue<IObserver>();
            
            _attacksComponents.Add(_spores);
            _attacksComponents.Add(_fungiDash);
            _attacksComponents.Add(FungiUltimate);
            _attacksComponents.Add(_sporeCloud);
            _attacksComponents.Add(_acidRain);
            _attacksComponents.Add(_fungiMinions);
            
            _currentState = FungiStates.PhaseOne;
            //CheckForAttacksUpdates();
            RunStateMachine();
        }

        private IEnumerator FungiStateMachine()
        {
            while (FunghyHealth.Life > 0 && !VulnerableState.InVulnerableState)
            {
                int attackIndex = _attacksSets.GetRandomValueInList(_minAttackSetRange, _maxAttackRange);
                RunAttackSet(attackIndex);
                for (int i = 0; i < _attackQueue.Length; i++)
                {
                    yield return StartCoroutine(_attackQueue.GetNextInQueue().Run());
                }
                _attackQueue.ClearQueue();
            }
        }

        /*
        private void CheckForAttacksUpdates()
        {
            switch (_currentState)
            {
                case FungiStates.PhaseOne: 
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.CrossShot);
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.Dash);
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.Minions);
                    
                    _observableObject.AddObserver(_spores);
                    _observableObject.AddObserver(_fungiDash);
                    _observableObject.AddObserver(_fungiMinions);
                    break;
                case FungiStates.PhaseTwo: 
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.AcidRain);
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.SporeCloud);
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.Ultimate);
                    
                    _observableObject.AddObserver(_acidRain);
                    _observableObject.AddObserver(_sporeCloud);
                    _observableObject.AddObserver(FungiUltimate);
                    break;
                case FungiStates.PhaseThree: 
                    _currentAtacks.Add(FungiUtilities.FungiAttacks.Minions);
                    
                    _observableObject.AddObserver(_fungiMinions);
                    break;
                default: 
                    Debug.LogError("Phase not found");
                    break;
            }
        }
        */
        private enum FungiStates
        {
            PhaseOne,
            PhaseTwo,
            PhaseThree
        }

        #region IBoss
        public void RunStateMachine()
        {
            ManageIdleMovement();
            StartCoroutine(FungiStateMachine());
        }

        public IEnumerator StopStateMachine()
        {
            foreach (IObserver attack in _attacksComponents)
                attack.Disable();
            yield return new WaitForSeconds(0.2f);
            foreach (IObserver attack in _attacksComponents)
                attack.Enable();
            StopAllCoroutines();
        }

        public void PhaseChecker()
        {
            if (FunghyHealth.Life > FunghyHealth.InitialLife * 0.66f)
                return;

            if (FunghyHealth.Life > FunghyHealth.InitialLife * 0.33f  && _currentState != FungiStates.PhaseTwo)
            {
                _currentState = FungiStates.PhaseTwo;
                StartCoroutine(StopAttackSet());
                _minAttackSetRange = 2;
                _maxAttackRange = 4;
                //CheckForAttacksUpdates();
                return;
            }

            if (FunghyHealth.Life < FunghyHealth.InitialLife * 0.33f && _currentState != FungiStates.PhaseThree)
            {
                _currentState = FungiStates.PhaseThree;
                StartCoroutine(StopAttackSet());
                _minAttackSetRange = 5;
                _maxAttackRange = 7;
                //CheckForAttacksUpdates();
            }
        }
        #endregion

        #region AttackSets
        private void RunAttackSet(int index)
        {
            if (_attackQueue.Length == 0)
            {
                foreach (FungiUtilities.FungiAttacks fungiAttack in _attacksSets[index].AttacksList)
                {
                    IObserver currentAttack = GetRespectiveIObserver(fungiAttack);
                    _attackQueue.AddToQueue(currentAttack);
                }
            }
        }

        private IEnumerator StopAttackSet(bool stopStateMachine = false)
        {
            _attackQueue.ClearQueue();
            if(stopStateMachine)
                yield return StartCoroutine(StopStateMachine());
        }

        private IObserver GetRespectiveIObserver(FungiUtilities.FungiAttacks attack)
        {
            switch (attack)
            {
                case FungiUtilities.FungiAttacks.AcidRain:
                    return _acidRain;
                case FungiUtilities.FungiAttacks.SporeCloud:
                    return _sporeCloud;
                case FungiUtilities.FungiAttacks.CrossShot:
                    return _spores;
                case FungiUtilities.FungiAttacks.Minions:
                    return _fungiMinions;
                case FungiUtilities.FungiAttacks.Ultimate:
                    return FungiUltimate;
                case FungiUtilities.FungiAttacks.Dash:
                    return _fungiDash;
                default:
                    Debug.LogError("Attack not found");
                    return null;
            }
        }
        #endregion

        public void ManageIdleMovement(bool active = true) => _fungiIdle.Deactivate = active;
    }
}