using RougeFW;
using UnityEngine;
using UnityEngine.Events;


public class VRUIButton : MonoBehaviour
{
    public Animator btn_animator;
    BulletController weapon_control;

    //public CurvedUI.CurvedUISettings ui_settings;
    public CameraSystem camera_system;
    public RectTransform rect_transform;
    public Camera check_camera;


    public LayerMask ui_mask;
    //public Collider ui_collider;


    public UnityEvent OnClick;

    private void Start()
    {
        OnStart();
    }


    public virtual void OnStart()
    {
        
        weapon_control = BulletController.bulletInstance;

        camera_system = CameraSystem.instance;
        if (camera_system != null)
        {
            check_camera = camera_system.main_camera;
        }

        if (MsgSystem.instance != null)
        {
            MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_left, OnVRTriggerDownLeft);
            MsgSystem.instance.RegistMsgAction(MsgSystem.vr_trigger_down_right, OnVRTriggerDownRight);
        }

        //ui_settings = GetComponentInParent<CurvedUI.CurvedUISettings>();

        /*if(ui_settings != null )
            check_camera = ui_settings.GetComponentInChildren<Camera>(true);*/
    }

    private void OnDestroy()
    {
        if (MsgSystem.instance != null)
        {
            MsgSystem.instance.RemoveMsgAction(MsgSystem.vr_trigger_down_left, OnVRTriggerDownLeft);
            MsgSystem.instance.RemoveMsgAction(MsgSystem.vr_trigger_down_right, OnVRTriggerDownRight);
        }
    }


    private void OnDisable()
    {
        is_on = false;

        btn_animator.SetTrigger("Normal");
    }


    public virtual void OnVRTriggerDownLeft(System.Object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (left_on == true) OnClick.Invoke();
    }

    public virtual void OnVRTriggerDownRight(System.Object[] objs)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        if (right_on == true) OnClick.Invoke();
    }

    public bool left_on = false;
    public bool right_on = false;

    void Update()
    {
        if (gameObject.activeInHierarchy == false)
            return;

        CheckHandsInteraction();
    }


    public virtual void CheckHandsInteraction()
    {
        if (weapon_control == null)
            return;

        left_on = CheckInteraction(weapon_control.currentLeftPoint.transform);
        right_on = CheckInteraction(weapon_control.currentRightPoint.transform);

        if (left_on == true || right_on == true)
            UpdateButtonOn();
        else
            UpdateButtonOff();
    }




    public virtual bool CheckInteraction(Transform fire_point)
    {
        Ray ray = new Ray(fire_point.position, fire_point.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100000, ui_mask) == true)
        {

            if (hit.collider.transform.parent.parent != UISystem.instance.transform)
                return false;
            /*if (hit.collider.gameObject.layer != LayerMask.NameToLayer("VRButton"))
                return false;*/

            Vector3[] corners = new Vector3[4];
            rect_transform.GetWorldCorners(corners);

            Vector3 screen_0 = check_camera.WorldToScreenPoint(corners[0]);
            Vector3 screen_2 = check_camera.WorldToScreenPoint(corners[2]);
            Vector3 screen_hit = check_camera.WorldToScreenPoint(hit.point);

            Vector3 center = (screen_0 + screen_2) * 0.5f;
            Vector3 deta = screen_0 - center;

            if (Mathf.Abs(screen_hit.x - center.x) <= Mathf.Abs(deta.x) && Mathf.Abs(screen_hit.y - center.y) <= Mathf.Abs(deta.y))
                return true;
        }


        return false;
    }


    public bool is_on = false;
    public AudioClip on_clip;

    public void UpdateButtonOn()
    {
        if (is_on == true)
            return;

        /*AudioSystem.instance.PlayEffect(on_clip, false, false);*/
        AudioSystem.instance.PlayerEffect(on_clip, Vector3.zero) ;
        is_on = true;


        btn_animator.SetTrigger("Highlighted");

    }

    public void UpdateButtonOff()
    {
        if (is_on == false)
            return;

        is_on = false;

        btn_animator.SetTrigger("Normal");
    }

}



