using UnityEngine;
using System.Collections;

public class SimpleAnimation : MonoBehaviour {
    [System.Serializable]
    public class Anim {
        public float frameDuration;
        public Sprite[] frames;
        public bool loop;
    }

    public Anim [] animations;

    SpriteRenderer m_renderer;
    Anim m_anim;
    bool m_animationRunning;
    float m_currentFrameTime;
    int m_currentFrameIndex;

    void Start() {
        m_renderer = GetComponent<SpriteRenderer>();
        m_currentFrameTime = 0f;
        m_currentFrameIndex = 0;
        m_animationRunning = false;
        if (animations.Length > 0) {
            m_anim = animations[0];
        }
        else {
            m_anim = null;
        }
    }

    void Update() {
        if (!m_animationRunning || m_anim == null) return;

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

        m_renderer.sprite = m_anim.frames[m_currentFrameIndex];
    }

    public void Play() {
        m_currentFrameTime = 0f;
        m_currentFrameIndex = 0;
        m_animationRunning = true;
    }

    public void StopPlayback() {
        m_animationRunning = false;
    }
}
