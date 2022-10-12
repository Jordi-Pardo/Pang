using System;
using UnityEngine;

public interface IPowerUp
{
    public void OnSpawn();
    public void Activate(GameObject gameObject,Action callback);
}