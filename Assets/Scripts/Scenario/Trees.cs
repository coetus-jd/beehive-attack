using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Scenario
{
    public class Trees : MonoBehaviour
    {
        [Tooltip("Time for the animation")]
        [SerializeField] private float AnimTime = 4f;
        [Tooltip("Size transform")]
        [SerializeField] private float SizeTransform = 0.3f;
        [Tooltip("Animation Curve")]
        [SerializeField] private AnimationCurve AnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        private Vector2 TreePosition, TreeScale;

        private float StartTime;

        private void Start()
        {
            TreePosition = transform.position;
            TreeScale = transform.localScale;
        }

        private void Update()
        {
            AnimationIdle();
        }
        
        void AnimationIdle()
        {
            var time = (Time.time - StartTime) / AnimTime;
            transform.localScale = TreeScale - new Vector2(0.4f, 0.3f) * SizeTransform * AnimCurve.Evaluate(time);
            transform.position = TreePosition - new Vector2(0, 0.05f) * SizeTransform * AnimCurve.Evaluate(time);

            if(time <= 1)
                return;
            StartTime = Time.time;
        }

    }

}
