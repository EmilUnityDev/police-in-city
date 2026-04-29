using UnityEngine;

public abstract class AbstractPersonFactory : MonoBehaviour
{
    public abstract Driver GetDriver();
}