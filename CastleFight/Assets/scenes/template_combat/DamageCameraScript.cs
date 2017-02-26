using UnityEngine;

namespace Assets.scenes.template_combat
{
    public class DamageCameraScript : MonoBehaviour
    {
        public GameObject Text3DPrefab;

        public float FadeTime = 1f;
        public float MoveSpeed = 1f;

        private void Awake()
        {
            this.MoveSpeedVector = new Vector2(0f, MoveSpeed);
        }


        public void CreateDamageText(GameObject damagedUnit, int damage)
        {
            CreateDamageText(damagedUnit.transform, damage);
        }

        public void CreateDamageText(Transform damagedUnit, int damage)
        {
            Create3DTextInstance(damagedUnit.transform.position, damage);
        }

        private void Create3DTextInstance(Vector3 position, int damage)
        {
            var instPos = new Vector3(position.x, position.y, -1f);

            var textInstance = (GameObject) GameObject.Instantiate(Text3DPrefab);

            var damageTextScript = textInstance.GetComponent<DamageTextBehaviour>();

            damageTextScript.SetMoveParameters(instPos, MoveSpeedVector, FadeTime);

            damageTextScript.SetText(damage.ToString());
        }

        private Vector2 MoveSpeedVector;


    }
}
