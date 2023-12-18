using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private Action<PropertiesFruits, int> levelUp;
        private LevelFruit levelFruit;
        private float scaleFruit = 0.3f;

        public void Initialized(Sprite sprite, int sizeScale, Action<PropertiesFruits, int> levelUp, bool isFall = false)
        {
            levelFruit = (LevelFruit)sizeScale;
            spriteRendererFruit.sprite = sprite;
            rb.bodyType = isFall? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            float scale = scaleFruit + sizeScale * 0.2f;
            transformFruit.localScale = new Vector2(scale, scale);
            this.levelUp = levelUp;
        }

        public void OnClick()
        {
            if(rb.bodyType != RigidbodyType2D.Dynamic)
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PropertiesFruits prop = collision.gameObject.GetComponent<PropertiesFruits>();
            if (prop != null)
            {
                if(this.levelFruit == prop.levelFruit)
                {
                    Debug.Log($"Check level fruit: {this.gameObject.name} == {collide.gameObject.name}");
                    SimplePool.Despawn(prop.gameObject);
                    levelUp?.Invoke(this, (int)this.levelFruit);
                }    
            }    
        }
    }
}
