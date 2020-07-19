using Photon;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour
{
    private Vector3 heightOffSet = ((Vector3) (Vector3.up * 0.48f));
    private bool isdestroying;
    private float killTime;
    private float killTime2;
    private Vector3 launchOffSet = Vector3.zero;
    private bool left = true;
    public bool leviMode;
    public float leviShootTime;
    private LineRenderer lineRenderer;
    private GameObject master;
    private GameObject myRef;
    public TITAN myTitan;
    private ArrayList nodes = new ArrayList();
    private int phase;
    private GameObject rope;
    private int spiralcount;
    private ArrayList spiralNodes;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;

    HERO masterT;
    Transform baseT;
    GameObject baseG;

    void Awake()
    {
        baseT = base.transform;
        baseG = base.gameObject;

    }
    public void checkTitan()
    {
        GameObject obj2 = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().main_object;
        if (((obj2 != null) && (masterT != null)) && (masterT == obj2))
        {
            RaycastHit hit;
            LayerMask mask = ((int) 1) << LayerMask.NameToLayer("PlayerAttackBox");
            if (Physics.Raycast(baseT.position, this.velocity, out hit, 10f, mask.value))
            {
                Collider collider = hit.collider;
                if (collider.name.Contains("PlayerDetectorRC"))
                {
                    TITAN component = collider.transform.root.gameObject.GetComponent<TITAN>();
                    if (component != null)
                    {
                        if (this.myTitan == null)
                        {
                            this.myTitan = component;
                            this.myTitan.isHooked = true;
                        }
                        else if (this.myTitan != component)
                        {
                            this.myTitan.isHooked = false;
                            this.myTitan = component;
                            this.myTitan.isHooked = true;
                        }
                    }
                }
            }
        }
    }

    public void disable()
    {
        this.phase = 2;
        this.killTime = 0f;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            object[] parameters = new object[] { 2 };
            base.photonView.RPC("setPhase", PhotonTargets.Others, parameters);
        }
    }

    private void FixedUpdate()
    {
        if (!(((this.phase == 2) || (this.phase == 1)) ? !this.leviMode : true))
        {
            this.spiralcount++;
            if (this.spiralcount >= 60)
            {
                this.isdestroying = true;
                this.removeMe();
                return;
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !base.photonView.isMine)
        {
            if (this.phase == 0)
            {

                baseT.position += (Vector3)(((this.velocity * Time.deltaTime) * 50f) + (this.velocity2 * Time.deltaTime));
                this.nodes.Add(new Vector3(baseT.position.x, baseT.position.y, baseT.position.z));
            }
        }
        else if (this.phase == 0)
        {
            RaycastHit hit;
            this.checkTitan();
            baseT.position += (Vector3)(((this.velocity * Time.deltaTime) * 50f) + (this.velocity2 * Time.deltaTime));
            LayerMask mask = ((int) 1) << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask2 = ((int) 1) << LayerMask.NameToLayer("Ground");
            LayerMask mask3 = ((int) 1) << LayerMask.NameToLayer("NetworkObject");
            LayerMask mask4 = (mask | mask2) | mask3;
            bool flag = false;
            bool flag2 = false;
            if (this.nodes.Count > 1)
            {
                flag2 = Physics.Linecast((Vector3) this.nodes[this.nodes.Count - 2], baseT.position, out hit, mask4.value);
            }
            else
            {
                flag2 = Physics.Linecast((Vector3) this.nodes[this.nodes.Count - 1], baseT.position, out hit, mask4.value);
            }
            if (flag2)
            {
                bool flag4 = true;
                Transform hit_collider = hit.collider.transform;
                if (hit_collider.gameObject.layer == LayerMask.NameToLayer("EnemyBox"))
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        object[] parameters = new object[] { hit_collider.root.gameObject.GetPhotonView().viewID };
                        base.photonView.RPC("tieMeToOBJ", PhotonTargets.Others, parameters);
                    }
                    masterT.lastHook = hit_collider.root;
                    baseT.parent = hit_collider;
                }
                else if (hit_collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    masterT.lastHook = null;
                }
                else if (((hit_collider.gameObject.layer == LayerMask.NameToLayer("NetworkObject")) && (hit_collider.gameObject.tag == "Player")) && !this.leviMode)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        object[] objArray3 = new object[] { hit_collider.root.gameObject.GetPhotonView().viewID };
                        base.photonView.RPC("tieMeToOBJ", PhotonTargets.Others, objArray3);
                    }
                    masterT.hookToHuman(hit_collider.root.gameObject, baseT.position);
                    baseT.parent = hit.collider.transform;
                    masterT.lastHook = null;
                }
                else
                {
                    flag4 = false;
                }
                if (this.phase == 2)
                {
                    flag4 = false;
                }
                if (flag4)
                {
                    masterT.launch(hit.point, this.left, this.leviMode);
                    baseT.position = hit.point;
                    if (this.phase != 2)
                    {
                        this.phase = 1;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            object[] objArray4 = new object[] { 1 };
                            base.photonView.RPC("setPhase", PhotonTargets.Others, objArray4);
                            object[] objArray5 = new object[] { baseT.position };
                            base.photonView.RPC("tieMeTo", PhotonTargets.Others, objArray5);
                        }
                        if (this.leviMode)
                        {
                            this.getSpiral(this.masterT.transform.position, this.masterT.transform.rotation.eulerAngles);
                        }
                        flag2 = true;
                    }
                }
            }
            this.nodes.Add(new Vector3(baseT.position.x, baseT.position.y, baseT.position.z));
            if (!flag)
            {
                this.killTime2 += Time.deltaTime;
                if (this.killTime2 > 0.8f)
                {
                    this.phase = 4;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        object[] objArray5 = new object[] { 4 };
                        base.photonView.RPC("setPhase", PhotonTargets.Others, objArray5);
                    }
                }
            }
        }
    }

    private void getSpiral(Vector3 masterposition, Vector3 masterrotation)
    {
        float num = 1.2f;
        float num2 = 30f;
        float num3 = 2f;
        float num4 = 0.5f;
        num = 30f;
        num3 = 0.05f + (this.spiralcount * 0.03f);
        if (this.spiralcount < 5)
        {
            num = Vector2.Distance(new Vector2(masterposition.x, masterposition.z), new Vector2(baseT.position.x, baseT.position.z));
        }
        else
        {
            num = 1.2f + ((60 - this.spiralcount) * 0.1f);
        }
        num4 -= this.spiralcount * 0.06f;
        float num5 = num / num2;
        float num6 = num3 / num2;
        float num7 = (num6 * 2f) * 3.141593f;
        num4 *= 6.283185f;
        this.spiralNodes = new ArrayList();
        for (int i = 1; i <= num2; i++)
        {
            float num9 = (i * num5) * (1f + (0.05f * i));
            float f = (((i * num7) + num4) + 1.256637f) + (masterrotation.y * 0.0173f);
            float x = Mathf.Cos(f) * num9;
            float z = -Mathf.Sin(f) * num9;
            this.spiralNodes.Add(new Vector3(x, 0f, z));
        }
    }

    public bool isHooked()
    {
        return (this.phase == 1);
    }

    [RPC]
    private void killObject()
    {
        UnityEngine.Object.Destroy(this.rope);
        UnityEngine.Object.Destroy(baseG);
    }

    public void launch(Vector3 v, Vector3 v2, string launcher_ref, bool isLeft, HERO hero, bool leviMode = false)
    {
        if (this.phase != 2)
        {
            masterT = hero;
            this.velocity = v;
            float f = Mathf.Acos(Vector3.Dot(v.normalized, v2.normalized)) * 57.29578f;
            if (Mathf.Abs(f) > 90f)
            {
                this.velocity2 = Vector3.zero;
            }
            else
            {
                this.velocity2 = Vector3.Project(v2, v);
            }
            if (launcher_ref == "hookRefL1")
            {
                this.myRef = hero.hookRefL1;
            }
            if (launcher_ref == "hookRefL2")
            {
                this.myRef = hero.hookRefL2;
            }
            if (launcher_ref == "hookRefR1")
            {
                this.myRef = hero.hookRefR1;
            }
            if (launcher_ref == "hookRefR2")
            {
                this.myRef = hero.hookRefR2;
            }
            this.nodes = new ArrayList();
            this.nodes.Add(this.myRef.transform.position);
            this.phase = 0;
            this.leviMode = leviMode;
            this.left = isLeft;
            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && base.photonView.isMine)
            {
                object[] parameters = new object[] { hero.photonView.viewID, launcher_ref };
                base.photonView.RPC("myMasterIs", PhotonTargets.Others, parameters);
                object[] objArray2 = new object[] { v, this.velocity2, this.left };
                base.photonView.RPC("setVelocityAndLeft", PhotonTargets.Others, objArray2);
            }
            baseT.position = this.myRef.transform.position;
            baseT.rotation = Quaternion.LookRotation(v.normalized);
        }
    }

    [RPC]
    private void myMasterIs(int id, string launcherRef)
    {
        masterT = PhotonView.Find(id).gameObject.GetComponent<HERO>();
        if (launcherRef == "hookRefL1")
        {
            this.myRef = masterT.hookRefL1;
        }
        if (launcherRef == "hookRefL2")
        {
            this.myRef = masterT.hookRefL2;
        }
        if (launcherRef == "hookRefR1")
        {
            this.myRef = masterT.hookRefR1;
        }
        if (launcherRef == "hookRefR2")
        {
            this.myRef = masterT.hookRefR2;
        }
    }

    [RPC]
    private void netLaunch(Vector3 newPosition)
    {
        this.nodes = new ArrayList();
        this.nodes.Add(newPosition);
    }

    [RPC]
    private void netUpdateLeviSpiral(Vector3 newPosition, Vector3 masterPosition, Vector3 masterrotation)
    {
        this.phase = 2;
        this.leviMode = true;
        this.getSpiral(masterPosition, masterrotation);
        Vector3 vector = masterPosition - ((Vector3) this.spiralNodes[0]);
        this.lineRenderer.SetVertexCount(this.spiralNodes.Count - ((int) (this.spiralcount * 0.5f)));
        for (int i = 0; i <= ((this.spiralNodes.Count - 1) - (this.spiralcount * 0.5f)); i++)
        {
            if (this.spiralcount < 5)
            {
                Vector3 position = ((Vector3) this.spiralNodes[i]) + vector;
                float num2 = (this.spiralNodes.Count - 1) - (this.spiralcount * 0.5f);
                position = new Vector3(position.x, (position.y * ((num2 - i) / num2)) + (newPosition.y * (((float) i) / num2)), position.z);
                this.lineRenderer.SetPosition(i, position);
            }
            else
            {
                this.lineRenderer.SetPosition(i, ((Vector3) this.spiralNodes[i]) + vector);
            }
        }
    }

    [RPC]
    private void netUpdatePhase1(Vector3 newPosition, Vector3 masterPosition)
    {
        this.lineRenderer.SetVertexCount(2);
        this.lineRenderer.SetPosition(0, newPosition);
        this.lineRenderer.SetPosition(1, masterPosition);
        baseT.position = newPosition;
    }

    private void OnDestroy()
    {
        if (FengGameManagerMKII.instance != null)
        {
            FengGameManagerMKII.instance.removeHook(this);
        }
        if (this.myTitan != null)
        {
            this.myTitan.isHooked = false;
        }
        UnityEngine.Object.Destroy(this.rope);
    }

    public void removeMe()
    {
        this.isdestroying = true;
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && base.photonView.isMine)
        {
            PhotonNetwork.Destroy(base.photonView);
            PhotonNetwork.RemoveRPCs(base.photonView);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            UnityEngine.Object.Destroy(this.rope);
            UnityEngine.Object.Destroy(baseG);
        }
    }

    private void setLinePhase0()
    {
        if (masterT == null)
        {
            UnityEngine.Object.Destroy(this.rope);
            UnityEngine.Object.Destroy(baseG);
        }
        else if (this.nodes.Count > 0)
        {
            Vector3 vector = this.myRef.transform.position - ((Vector3) this.nodes[0]);
            this.lineRenderer.SetVertexCount(this.nodes.Count);
            for (int i = 0; i <= (this.nodes.Count - 1); i++)
            {
                this.lineRenderer.SetPosition(i, ((Vector3) this.nodes[i]) + ((Vector3) (vector * Mathf.Pow(0.75f, (float) i))));
            }
            if (this.nodes.Count > 1)
            {
                this.lineRenderer.SetPosition(1, this.myRef.transform.position);
            }
        }
    }

    [RPC]
    private void setPhase(int value)
    {
        this.phase = value;
    }

    [RPC]
    private void setVelocityAndLeft(Vector3 value, Vector3 v2, bool l)
    {
        this.velocity = value;
        this.velocity2 = v2;
        this.left = l;
        baseT.rotation = Quaternion.LookRotation(value.normalized);
    }

    private void Start()
    {
        this.rope = (GameObject)UnityEngine.Object.Instantiate(Cach.rope != null ? Cach.rope : Cach.rope = (GameObject)Resources.Load("rope"));
        this.lineRenderer = this.rope.GetComponent<LineRenderer>();
        FengGameManagerMKII.instance.addHook(this);
    }

    [RPC]
    private void tieMeTo(Vector3 p)
    {
        baseT.position = p;
    }

    [RPC]
    private void tieMeToOBJ(int id)
    {
        baseT.parent = PhotonView.Find(id).gameObject.transform;
    }

    public void update()
    {
        if (masterT == null)
        {
            this.removeMe();
        }
        else if (!this.isdestroying)
        {
            if (this.leviMode)
            {
                this.leviShootTime += Time.deltaTime;
                if (this.leviShootTime > 0.4f)
                {
                    this.phase = 2;
                    baseG.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (this.phase == 0)
            {
                this.setLinePhase0();
            }
            else if (this.phase == 1)
            {
                Vector3 vector = baseT.position - this.myRef.transform.position;
                Vector3 vector1 = baseT.position + this.myRef.transform.position;
                Vector3 velocity = masterT.rigidbody.velocity;
                float magnitude = velocity.magnitude;
                float f = vector.magnitude;
                int num3 = (int) ((f + magnitude) / 5f);
                num3 = Mathf.Clamp(num3, 2, 6);
                this.lineRenderer.SetVertexCount(num3);
                this.lineRenderer.SetPosition(0, this.myRef.transform.position);
                int index = 1;
                float num5 = Mathf.Pow(f, 0.3f);
                while (index < num3)
                {
                    int num6 = num3 / 2;
                    float num7 = Mathf.Abs((int) (index - num6));
                    float num8 = (num6 - num7) / ((float) num6);
                    num8 = Mathf.Pow(num8, 0.5f);
                    float max = ((num5 + magnitude) * 0.0015f) * num8;
                    this.lineRenderer.SetPosition(index, (Vector3) ((((new Vector3(UnityEngine.Random.Range(-max, max), UnityEngine.Random.Range(-max, max), UnityEngine.Random.Range(-max, max)) + this.myRef.transform.position) + (vector * (((float) index) / ((float) num3)))) - (((Vector3.up * num5) * 0.05f) * num8)) - (((velocity * 0.001f) * num8) * num5)));
                    index++;
                }
                this.lineRenderer.SetPosition(num3 - 1, baseT.position);
            }
            else if (this.phase == 2)
            {
                if (this.leviMode)
                {
                    this.getSpiral(masterT.transform.position, masterT.transform.rotation.eulerAngles);
                    Vector3 vector3 = this.myRef.transform.position - ((Vector3) this.spiralNodes[0]);
                    this.lineRenderer.SetVertexCount(this.spiralNodes.Count - ((int) (this.spiralcount * 0.5f)));
                    for (int i = 0; i <= ((this.spiralNodes.Count - 1) - (this.spiralcount * 0.5f)); i++)
                    {
                        if (this.spiralcount < 5)
                        {
                            Vector3 position = ((Vector3) this.spiralNodes[i]) + vector3;
                            float num11 = (this.spiralNodes.Count - 1) - (this.spiralcount * 0.5f);
                            position = new Vector3(position.x, (position.y * ((num11 - i) / num11)) + (baseT.position.y * (((float) i) / num11)), position.z);
                            this.lineRenderer.SetPosition(i, position);
                        }
                        else
                        {
                            this.lineRenderer.SetPosition(i, ((Vector3) this.spiralNodes[i]) + vector3);
                        }
                    }
                }
                else
                {
                    this.lineRenderer.SetVertexCount(2);
                    this.lineRenderer.SetPosition(0, baseT.position);
                    this.lineRenderer.SetPosition(1, this.myRef.transform.position);
                    this.killTime += Time.deltaTime * 0.2f;
                    this.lineRenderer.SetWidth(0.1f - this.killTime, 0.1f - this.killTime);
                    if (this.killTime > 0.1f)
                    {
                        this.removeMe();
                    }
                }
            }
            else if (this.phase == 4)
            {
                baseT.position += this.velocity + ((Vector3)(this.velocity2 * Time.deltaTime));
                this.nodes.Add(new Vector3(baseT.position.x, baseT.position.y, baseT.position.z));
                Vector3 vector5 = this.myRef.transform.position - ((Vector3) this.nodes[0]);
                for (int j = 0; j <= (this.nodes.Count - 1); j++)
                {
                    this.lineRenderer.SetVertexCount(this.nodes.Count);
                    this.lineRenderer.SetPosition(j, ((Vector3) this.nodes[j]) + ((Vector3) (vector5 * Mathf.Pow(0.5f, (float) j))));
                }
                this.killTime2 += Time.deltaTime;
                if (this.killTime2 > 0.8f)
                {
                    this.killTime += Time.deltaTime * 0.2f;
                    this.lineRenderer.SetWidth(0.1f - this.killTime, 0.1f - this.killTime);
                    if (this.killTime > 0.1f)
                    {
                        this.removeMe();
                    }
                }
            }
        }
    }
}

