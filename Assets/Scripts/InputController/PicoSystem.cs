using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.PXR;
using UnityEngine.XR.Interaction.Toolkit;
using RougeFW;

#if PICO
using UnityEngine.XR;
using  Unity.XR.PXR;
#endif

    public class PicoSystem : MonoBehaviour
    {

        public static PicoSystem instance;
        
        void Awake()
        {
            instance = this;
        }

        void OnDestroy()
        {
            instance = null;
        }

      

       public List<bool> is_button_down = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };


        void Update()
        {
            if (MsgSystem.instance == null)
                return;
            UpdateButton();
         
        }

       

        bool[] check_btn = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, };

        public void UpdateButton()
        {

      

        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.triggerButton, out check_btn[0]);
        CheckButton(0, check_btn[0], MsgSystem.vr_trigger_down_left, MsgSystem.vr_trigger_up_left);

        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out check_btn[1]);
        CheckButton(1, check_btn[1], MsgSystem.vr_trigger_down_right, MsgSystem.vr_trigger_up_right);
       
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.gripButton, out check_btn[2]);
        CheckButton(2, check_btn[2], MsgSystem.vr_hold_down_left, MsgSystem.vr_hold_up_left);

        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out check_btn[3]);
        CheckButton(3, check_btn[3], MsgSystem.vr_hold_down_right, MsgSystem.vr_hold_up_right);

        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out check_btn[4]);
        CheckButton(4, check_btn[4], MsgSystem.vr_button_a_down,MsgSystem.vr_button_a_up);

        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.secondaryButton, out check_btn[5]);
        CheckButton(5, check_btn[5], MsgSystem.vr_button_b_down,MsgSystem.vr_button_b_up);

        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out check_btn[6]);
        CheckButton(6, check_btn[6], MsgSystem.vr_button_x_down,MsgSystem.vr_button_x_up);

        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.secondaryButton, out check_btn[7]);
        CheckButton(7, check_btn[7], MsgSystem.vr_button_y_down,MsgSystem.vr_button_y_up);
#if PICO
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.triggerButton, out check_btn[0]);
            CheckButton(0, check_btn[0], MsgSystem.vr_trigger_down_left, MsgSystem.vr_trigger_up_left);
            
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out check_btn[1]);
            CheckButton(1, check_btn[1], MsgSystem.vr_trigger_down_right, MsgSystem.vr_trigger_up_right);

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.gripButton, out check_btn[2]);
            CheckButton(2, check_btn[2], MsgSystem.vr_hold_down_left, MsgSystem.vr_hold_up_left);

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out check_btn[3]);
            CheckButton(3, check_btn[3], MsgSystem.vr_hold_down_right, MsgSystem.vr_hold_up_right);

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out check_btn[4]);
            CheckButton(4, check_btn[4], MsgSystem.vr_button_a_down,MsgSystem.vr_button_a_up);

            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.secondaryButton, out check_btn[5]);
            CheckButton(5, check_btn[5], MsgSystem.vr_button_b_down,MsgSystem.vr_button_b_up);

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out check_btn[6]);
            CheckButton(6, check_btn[6], MsgSystem.vr_button_x_down,MsgSystem.vr_button_x_up);

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.secondaryButton, out check_btn[7]);
            CheckButton(7, check_btn[7], MsgSystem.vr_button_y_down,MsgSystem.vr_button_y_up);

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.menuButton, out check_btn[8]);
            CheckButton(8, check_btn[8], MsgSystem.vr_button_start_down,MsgSystem.vr_button_start_up);
#endif
    }
        public void CheckButton(int button_index, bool button_value, string down_event, string up_event)
        {
           
            if (button_value && !is_button_down[button_index])
            {
                is_button_down[button_index] = true;
                Debug.Log(button_value);
                MsgSystem.instance.SendMsg(down_event, null);
            }

            else if (!button_value&& is_button_down[button_index])
            {
                is_button_down[button_index] = false;
                MsgSystem.instance.SendMsg(up_event, null);
            }

        }


      
      




    }
