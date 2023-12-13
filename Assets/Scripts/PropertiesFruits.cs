using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class PropertiesFruits : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRendererFruit;
        [SerializeField] private Transform transformFruit;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D collide;
        private LevelFruit levelFruit;
        private float scaleFruit = 0.3f;

        public void Initialized(Sprite sprite, int sizeScale)
        {
            levelFruit = (LevelFruit)sizeScale;
            spriteRendererFruit.sprite = sprite;
            rb.bodyType = RigidbodyType2D.Kinematic;
            float scale = scaleFruit + sizeScale * 0.2f;
            transformFruit.localScale = new Vector2(scale, scale);
        }

        public void OnClick()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }    

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
