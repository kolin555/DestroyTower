using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    public class PlayerAimControlVR : MonoBehaviour
    {
        //public GunController gun_controller;

        private void Update()
        {
            CheckMouseKey();

            if ( Input.GetMouseButtonDown(0) == true )
                MsgSystem.instance.SendMsg(MsgSystem.vr_trigger_down_left, null);

            if (Input.GetMouseButtonUp(0) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_trigger_up_left, null);

            if (Input.GetMouseButtonDown(1) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_trigger_down_right, null);
            if (Input.GetMouseButtonUp(1) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_trigger_up_right, null);




            if (Input.GetKeyDown(KeyCode.A) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_button_a_down, null);
            if (Input.GetKeyDown(KeyCode.B) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_button_b_down, null);

            if (Input.GetKeyDown(KeyCode.X) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_button_x_down, null);
            if (Input.GetKeyDown(KeyCode.Y) == true)
                MsgSystem.instance.SendMsg(MsgSystem.vr_button_y_down, null);

        }




        public virtual void CheckMouseKey( )
        {
            if (Input.GetMouseButton(0) == true|| Input.GetMouseButton(1) == true)
            {
                Ray aim_ray = CameraSystem.instance.main_camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Vector3 aim_point = Vector3.zero;

                if (Physics.Raycast(aim_ray, out hit, 1000) == true)
                    aim_point = hit.point;
                else
                    aim_point = aim_ray.GetPoint(200);

                /*if (weaopn_control.player_handle_left != null) weaopn_control.player_handle_left.transform.LookAt(aim_point);
                if (weaopn_control.player_handle_right != null) weaopn_control.player_handle_right.transform.LookAt(aim_point);*/
                if(GunController.gunInstance.currentLeftGun!=null) GunController.gunInstance.currentLeftGun.transform.LookAt(aim_point);
                if(GunController.gunInstance.currentRightGun!=null) GunController.gunInstance.currentRightGun.transform.LookAt(aim_point);
                
                if(BulletController.bulletInstance.currentLeftPoint!=null) BulletController.bulletInstance.currentLeftPoint.transform.LookAt(aim_point);
                if(BulletController.bulletInstance.currentRightPoint!=null) BulletController.bulletInstance.currentRightPoint.transform.LookAt(aim_point);
            }
        }





    }

