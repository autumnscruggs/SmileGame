using UnityEngine;
using System.Collections;

public interface ICollectible
{
    void PickUp(ICollector collector);
    void Use();
}
