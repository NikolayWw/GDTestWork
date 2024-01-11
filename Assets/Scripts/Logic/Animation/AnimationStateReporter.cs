using System;
using UnityEngine;

namespace Logic.Animation
{
    public class AnimationStateReporter : MonoBehaviour
    {
        public Action<AnimationStateId> OnReport;

        private void Report(AnimationStateId id) =>
            OnReport?.Invoke(id);
    }
}