using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RougeFW
{


    public class UIPanel : MonoBehaviour
    {
        
        public int priority = 0;
        public bool is_show_when_start = true;
        
        
    
        public virtual void StartPanel()
        {
            //HideUIPanel();
            
            gameObject.SetActive(is_show_when_start);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {

        }

        public virtual void ShowUIPanel( )
        {

            var group = this.transform.GetComponent<CanvasGroup>();
            group.alpha = 0;
            gameObject.SetActive(true);
            DOTween.To(() => group.alpha, a => group.alpha = a, 1f, 1f);
            
            
        }

        public virtual void HideUIPanel( )
        {

            gameObject.SetActive(false);
            /*var group = this.transform.GetComponent<CanvasGroup>();
            group.alpha = 1;
            DOTween.To(() => group.alpha, a => group.alpha = a, 0f, 1f).OnComplete(() => {

                gameObject.SetActive(false);

            });  */
        }


    }
}