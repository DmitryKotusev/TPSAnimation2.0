using UnityEngine;

[CreateAssetMenu(menuName = "Character/Status")]
public class CharacterStatus : ScriptableObject
{
    public bool isAiming;
    public bool isSprinting;
    public bool isGround;
}
