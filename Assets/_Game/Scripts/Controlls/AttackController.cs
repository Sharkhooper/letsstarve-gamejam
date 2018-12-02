using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using NaughtyAttributes;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Controlls {
    public class AttackController : MonoBehaviour{
        private Animator animator;
        
        public GameObjectList targets; // PlayerParty for enemies, Enemies for the player characters

        public Inventory inventory;
        [SerializeField] private GearItem item;
        [FormerlySerializedAs("constantStats")] [SerializeField] private Gear baseStats;

        public Gear gear() => item == null ? baseStats : item.Gear;

        private GameObject preferedTarget = null;
        private CharacterActor characterActor;

        public SpriteRenderer attackProjectile;
        
        private void Start() {
            characterActor = GetComponentInParent<CharacterActor>();
            animator = GetComponent<Animator>();
        }

        public GearItem GetGear(GearItem item) {
            if (! inventory.EquippedItems.TryGetValue(characterActor, out GearItem current)) {
                current = null;
            }

            var stats = this.item != null ? current.Gear : baseStats;

            if (stats.damageValue < item.DamageValue) { // fixme: do better...
                inventory.EquipGear(characterActor, item);
                this.item = item;
                return current;
            }
            
            // todo: animator, set attack animation speed based on weapon speed
            return item; // not interested ... 
        }
        
        private void Update() {
            preferedTarget = targets.List.FirstOrDefault(IsInRange);
            
            if (preferedTarget == null) {
                // nothing in range. don't do anything.
                return;
            }

            characterActor.Forward = preferedTarget.transform.position - this.transform.position;
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
            if(this.item != null){
            attackProjectile.gameObject.SetActive(true);
            attackProjectile.sprite = this.item.graphic;
            attackProjectile.transform.position = this.transform.position + characterActor.Forward;
            DOTween.To(() => 0f, f => {
                    attackProjectile.transform.localRotation = Quaternion.Euler(Vector3.forward * f);
                }, 360 * 3, 0.8f).OnComplete(() => attackProjectile.gameObject.SetActive(false));
            
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