using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_GetTypeTileLocked", menuName = "Data/RSE/RSE_GetTypeTileLocked")]
public class RSE_GetTypeTileLocked : BT.ScriptablesObject.RuntimeScriptableEvent<Vector3,Action<TileType>>{}