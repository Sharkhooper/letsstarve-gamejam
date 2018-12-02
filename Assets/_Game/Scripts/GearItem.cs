using UnityAtoms;
using UnityEngine;

[CreateAssetMenu]
public class GearItem : ScriptableObject {
    [SerializeField] private Gear gear;

    public Gear Gear => gear; 

    public int DamageValue => gear.damageValue;

    public float RangeValue => gear.rangeValue;

    public bool IsRangedValue => gear.isRangedValue;

    public Sprite graphic;
}