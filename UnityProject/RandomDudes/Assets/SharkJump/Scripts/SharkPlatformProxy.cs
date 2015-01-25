using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace SharkJumper
{


    [RequireComponent(typeof(Sprite))]
    [RequireComponent(typeof(Collider2D))]
    public class SharkPlatformProxy : MonoBehaviour
    {

        public const float FallDownSpeed = 9;
        public const float FadeSpeed = 2;

        private List<PlayerProxy> playerContained = new List<PlayerProxy>();

        [HideInInspector()] public SpriteRenderer _sprite;
        private Collider2D _collider;
        private Transform _myTransform;

        private Color col;

        private bool Dying = false;

        public Color Color
        {
            get { return col; }
            set { if (value != col) { col = value; _sprite.color = col; } }
        }

        void Awake()
        {
            _myTransform = GetComponent<Transform>();
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            col = _sprite.color;
        }

        public void AddRemovePlayer(PlayerProxy player)
        {
            if (playerContained.Contains(player))
            {
                playerContained.Remove(player);
                if (playerContained.Count <= 0) Kill();
            }
            else
            {
                playerContained.Add(player);
            }
        }

        public void Kill()
        {
            if (Dying) return;
            Dying = true;
            StartCoroutine(DestroyFader());
        }

        IEnumerator DestroyFader()
        {
            _collider.enabled = false;

            while (col.a > 0)
            {
                col.a -= Time.deltaTime * FadeSpeed;
                _sprite.color = col;
                _myTransform.Translate(Vector3.down * Time.deltaTime * FallDownSpeed);
                yield return new WaitForEndOfFrame();
            }
            GameObject.Destroy(gameObject);
            yield return null;
        }
    }

}