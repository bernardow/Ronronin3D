using System.Collections;

namespace Units
{
    public interface IBoss
    {
        public void RunStateMachine();

        public IEnumerator StopStateMachine();

        public void PhaseChecker();
    }
}
