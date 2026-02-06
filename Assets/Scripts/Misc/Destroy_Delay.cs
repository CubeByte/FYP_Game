using UnityEngine;

public class Destroy_Delay : MonoBehaviour
{
  public float lifetime;

  void Start()
  {
    Destroy(gameObject, lifetime);
  }
}
