using HumanAPI;
using UnityEngine;
using UnityEngine.Events;

namespace fall {

    public class VoidTrigger : LevelPassTrigger {

        public new void OnTriggerEnter(Collider other) {
            if (!active) return;
            HumanBase componentInParent = other.GetComponentInParent<HumanBase>();
            if (!componentInParent) return;
            Dependencies.Get<IGame>().EnterPassZone();
            Dependencies.Get<IGame>().Fall(componentInParent, false, fallAchievement);
        }

        public bool fallAchievement = true;

        public UnityEvent OnFall;
    }
}