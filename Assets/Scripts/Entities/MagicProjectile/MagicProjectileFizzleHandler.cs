using UnityEngine;

public class MagicProjectileFizzleHandler : MonoBehaviour
{
    [NotNull]
    public MagicProjectile mainObject;

    public void DestroySelf()
    {
        Destroy(this.mainObject.gameObject);
    }
}
