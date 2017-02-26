using System;
using System.Collections;
using UnityEngine;

namespace Assets.scenes.template_combat
{
    public class DamageTextBehaviour : MonoBehaviour
    {

        void Awake()
        {
            StartCoroutine(DestroyCor());
        }

        private IEnumerator DestroyCor()
        {
            yield return new WaitForSeconds(3f);

            Destroy(gameObject);
        }

        void Update()
        {
            if (moveDelegate != null)
            {
                moveDelegate();
            }
        }

        public void SetMoveParameters(Vector3 instPos, Vector2 moveSpeedVector, float fadeTime)
        {
            this.fadeTime = fadeTime;

            moveSpeed = moveSpeedVector * Time.deltaTime;

            transform.position = instPos;

            moveDelegate = Move;
        }

        private void Move()
        {
            transform.Translate(moveSpeed);

            var color = textMesh.color;

            color.a -= Time.deltaTime/fadeTime;

            textMesh.color = color;
        }

        private float fadeTime;
        private Vector2 moveSpeed;
        private Action moveDelegate;

        private TextMesh textMesh
        {
            get { return GetComponent<TextMesh>(); }
        }

        public void SetText(string damage)
        {
            textMesh.text = damage.ToString();
        }
    }
}
