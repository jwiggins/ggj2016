using UnityEngine;
using System.Collections;

public class SimpleAnimation : MonoBehaviour {
    [System.Serializable]
    public class _Animation {
        public float frameDuration;
        public Sprite[] frames;
        public bool loop;
    }

    _Animation m_anim;
    bool m_animationRunning;
    float m_currentFrameTime;
    int m_currentFrameIndex;

    void Start() {
        m_currentFrameTime = 0f;
        m_currentFrameIndex = 0;
        m_animationRunning = false;
    }

    void Update() {
        if (!m_animationRunning) return;

        m_currentFrameTime += Time.deltaTime;

        if (m_currentFrameTime >= m_anim.frameDuration) {
            m_currentFrameIndex += 1;
            m_currentFrameTime %= m_anim.frameDuration;
        }

        if (m_currentFrameIndex >= m_anim.frames.Length) {
            if (m_anim.loop) {
                m_currentFrameIndex = 0;
            }
            else {
                m_animationRunning = false;
            }
        }
    }
}
