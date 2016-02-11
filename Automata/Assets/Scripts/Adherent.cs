using UnityEngine;
using System.Collections;

public class Adherent : MonoBehaviour {

    public enum adTypes {
        Rectangle = 0,
        Circle = 1,
        Diamond = 2,
        Triangle = 3
    }

    private GameObject host;
    private Follower follower;
    public PathObject pathObject;

    public bool isCarrying = false;
    public int level;
    private int type;
    private float m_pathTparam = 0;

    // Use this for initialization
    void Awake() {
        host = gameObject;
        follower = transform.GetChild(0).GetComponent<Follower>();
        pathObject = transform.GetChild(1).GetComponent<PathObject>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        float oldX = m_pathTparam;
        m_pathTparam += 4f;
        m_pathTparam %= pathObject.path.Length;
        Vector2 point = pathObject.path.GetPointAt(m_pathTparam);
        float angle = pathObject.path.GetAngleAt(m_pathTparam);
        IntersectionData inter = pathObject.path.GetIntersectionBetween(oldX, m_pathTparam);
        if (!inter.isEmpty()) {
            Resource levelRes = ResourceManager.levelResource(level);

            if (isCarrying && levelRes.canCollide) {
                this.detach(levelRes, true);
            }
        }
        follower.place(point + new Vector2(host.transform.position.x, host.transform.position.y), angle);
    }

    public void setPath(Path p) {
        pathObject.setShape(p);
        p.parentAdherent = this;
    }

    public void setPos(float t) {
        Vector2 point = pathObject.path.GetPointAt(t);
        Vector2 position = point + new Vector2(host.transform.position.x, host.transform.position.y);
        Rigidbody2D body = follower.GetComponent<Rigidbody2D>();

        m_pathTparam = t;
        body.position = position;
        follower.transform.position = position;
    }

    public Adherent attach(Resource res) {
        follower.swapSprite();
        isCarrying = true;

        res.gameObject.transform.SetParent(follower.host.transform);
        res.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        res.gameObject.transform.localEulerAngles = new Vector3(1, 0, 0);
        res.pauseCollision();
        return this;
    }

    public void detach(Resource res, bool andPauseCollision) {
        follower.swapSprite();
        isCarrying = false;

        res.gameObject.transform.SetParent(null);
        res.gameObject.transform.localPosition = follower.gameObject.transform.position;
        res.gameObject.transform.localEulerAngles = follower.gameObject.transform.localEulerAngles;
        res.parent = null;

        if (andPauseCollision) {
            res.pauseCollision();
        }
    }

    public int getType() {
        return type;
    }

    public void setType(int t) {
        type = t;
        follower.setSprite(type);

        Renderer rend = pathObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Unlit/Color");
        if (type == 0) {
            rend.material.SetColor("_Color", new Color(2f / 255f, 184f / 255f, 162f / 255f));
        } if (type == 1) {
            rend.material.SetColor("_Color", new Color(234f / 255f, 108f / 255f, 94f / 255f));
        } if (type == 2) {
            rend.material.SetColor("_Color", new Color(146f / 255f, 113f / 255f, 159f / 255f));
        } if (type == 3) {
            rend.material.SetColor("_Color", new Color(221f / 255f, 181f / 255f, 16f / 255f));
        }
    }
}
