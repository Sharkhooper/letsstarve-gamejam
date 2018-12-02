using System.Linq;
using DefaultNamespace;
using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;

namespace _Game.Scripts.Controlls {
    public class AttackController : MonoBehaviour{
        private Animator animator;
        
        public GameObjectList targets; // PlayerParty for enemies, Enemies for the player characters

        public bool useConstantStats;
        public bool useConstantStatsNot() => !useConstantStats;
        [ShowIf(nameof(useConstantStatsNot))][SerializeField] private GearItem item;
        [ShowIf(nameof(useConstantStats))][SerializeField] private Gear constantStats;

        public Gear gear() => useConstantStats ? constantStats : item.Gear;

        private GameObject preferedTarget = null;
        
        private void Start() {
            animator = GetComponent<Animator>();
        }

        private void Update() {
            preferedTarget = targets.List.FirstOrDefault(IsInRange);
            
            if (preferedTarget == null) {
                // nothing in range. don't do anything.
                return;
            }

            animator.SetTrigger("attack");
            this.enabled = false;
        }
        
        #region AnimationTriggerCallbackFunctions
        // this method is called if the attack animation has finished
        public void OnAttackAnimationTriggerFinished() {
            this.enabled = true;
            
            if (gear().isRangedValue) {
                // TODO: spawn projectile on ranged attack instead of instant damage... 
                
                return;
            }
            
            if (preferedTarget == null) {
                return; // should not be here ... "false" attack animation
            }
            
            // EITHER: apply damage to prefered target -> it is still in range
            // OR: check if another possible target is there and use it instead.
            // "uhm ... i actually aimed for your friend, but okay ..."

            var other = IsInRange(preferedTarget) ? preferedTarget : targets.List.FirstOrDefault(IsInRange);
            if(other == null) return;

            // todo: calculate damage correctly with attributes and buffs and stuff that might later come into the game
            other.GetComponent<HealthComponent>().Damage(gear().damageValue);
        }

        // same as above, but for exploding stuff
        public void OnSelfDestructAnimationTriggerFinshed() {
            //todo: fill me with content
        }
        #endregion

        
        private bool IsInRange(GameObject go) => go.transform.inRange(this.transform, gear().rangeValue);
    }
}