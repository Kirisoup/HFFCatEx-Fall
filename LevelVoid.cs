using System;
using System.Linq;
using HumanAPI;
using UnityEngine;

namespace fall {

    public class LevelVoid {

        public Lazy<GameObject[]> VoidObjs => new(FindVoidObjs);
        public Lazy<FallTrigger[]> FallTrigs => new(GetFallTrigs);
        public Lazy<VoidTrigger[]> VoidTrigs => new(GetVoidTrigs);
        public Lazy<LevelPassTrigger[]> PassTrigs => new(GetPassTrigs);

        public static void Add(GameObject obj) => obj.AddComponent<VoidTrigger>();
        public static void Destroy(UnityEngine.Object obj) => UnityEngine.Object.Destroy(obj);
        public static void Enable(Behaviour beh) => beh.enabled = true;
        public static void Disable(Behaviour beh) => beh.enabled = false;

        private GameObject[] FindVoidObjs() =>
            GetCPsCoords()
                .Select(v => Physics.Raycast(v.ToBottom(), Vector3.up, out RaycastHit hit) ? hit.collider : null)
                .Where(v => v is not null)
                .Distinct()
                .Where(c => c.GetComponent<FallTrigger>())
                .AnyOr(GetCPsCoords()
                    .SelectMany(v => Physics.RaycastAll(v.ToBottom(), Vector3.up))
                    .Select(h => h.collider)
                    .Distinct()
                    .Where(c => c.GetComponent<FallTrigger>()))
                .Select(c => c.gameObject)
                .ToArray();

        private FallTrigger[] GetFallTrigs() =>
            VoidObjs.Value
                .SelectMany(v => v.GetComponents<FallTrigger>())
                .ToArray();


        private VoidTrigger[] GetVoidTrigs() =>
            VoidObjs.Value
                .Select(o => o.GetComponent<VoidTrigger>())
                .ToArray();

        private LevelPassTrigger[] GetPassTrigs() =>
            UnityEngine.Object.FindObjectsOfType<LevelPassTrigger>()
                .Where(p => !VoidTrigs.Value.Contains(p))
                .ToArray();

        private static Vector3[] GetCPsCoords() =>
            Game.currentLevel?.checkpoints
                .Select(cp => cp.position)
                .ToArray() ?? Array.Empty<Vector3>();

        public static readonly float bottomY = -10000;
    }

    public static class Vec3Extension {
        public static Vector3 ToBottom(this Vector3 v) =>
            new(v.x, LevelVoid.bottomY, v.z);
    }
}