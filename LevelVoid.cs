using System;
using System.Collections.Generic;
using System.Linq;
using HumanAPI;
using UnityEngine;

namespace fall {

    public class LevelVoid {

        public static readonly float bottomY = -10000;

        private HashSet<GameObject> voidObjts;

        public HashSet<GameObject> VoidObjts => voidObjts ?? FindVoidObjts();

        private HashSet<GameObject> FindVoidObjts() {
            voidObjts = new();
            foreach (var coord in GetCPsCoords()) {
                if (!Physics.Raycast(coord.ToLvlBottom(), Vector3.up, out RaycastHit hit)) continue;
                if (!hit.collider.GetComponent<FallTrigger>()) continue;
                voidObjts.Add(hit.collider.gameObject);
            }
            return voidObjts;
        }

        private Vector3[] GetCPsCoords() =>
            Game.currentLevel?.checkpoints
                .Select(cp => cp.position)
                .ToArray() ?? Array.Empty<Vector3>();

        private LevelPassTrigger[] voidTrigs;

        public LevelPassTrigger[] VoidTrigs =>
            voidTrigs ??= VoidObjts
                .Select(o => o.GetComponent<VoidTrigger>())
                .ToArray();
        
        private LevelPassTrigger[] passTrigs;

        public LevelPassTrigger[] PassTrigs =>
            passTrigs ??= UnityEngine.Object.FindObjectsOfType<LevelPassTrigger>()
                .Where(p => !VoidTrigs.Contains(p))
                .ToArray();

        private FallTrigger[] fallTrigs;

        public FallTrigger[] FallTrigs => 
            fallTrigs ??= VoidObjts
                .Select(v => v.GetComponent<FallTrigger>())
                .ToArray()
        ;
    }

    public static class Vec3Extensions {
        public static Vector3 ToLvlBottom(this Vector3 v) => new(v.x, LevelVoid.bottomY, v.z);
    }
}