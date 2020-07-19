using UnityEngine;

[CreateAssetMenu(menuName = "Create Power-up")]
public class PowerUpInfo : ScriptableObject
{
    public Constant.Powerup type;
    [SerializeField] private Sprite boundingCircle;
    [SerializeField] private Sprite icon;

    public Sprite BoundingCircle => boundingCircle;
    
    public Sprite Icon => icon;
    public float timeActive;
}
