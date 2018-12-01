using UnityEngine;

[CreateAssetMenu]
public class GearItem : ScriptableObject
{
    [SerializeField] private int damageValue;

    [SerializeField] private int rangeValue;

    [SerializeField] private bool isRangedValue;

    public int DamageValue => damageValue;

    public int RangeValue => rangeValue;

    public bool IsRangedValue => isRangedValue;
}
