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
        private LevelFruit levelFruit;
        private float scaleFruit = 0.3f;
        private Action<PropertiesFruits, PropertiesFruits, int> levelUp;
        private bool isColide = false;
        private bool isEnd = false;

        public bool IsColide { get => isColide; set => isColide = value; }
        public bool IsEnd { get => isEnd; }

        public void Initialized(Sprite sprite, int sizeScale, Action<PropertiesFruits, PropertiesFruits, int> levelUp, Action endGame, bool isFall = false)
        {
            IsColide = false;
            levelFruit = (LevelFruit)sizeScale;
            spriteRendererFruit.sprite = sprite;
            rb.bodyType = isFall ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            float scale = scaleFruit + sizeScale * 0.2f;
            transformFruit.localScale = new Vector2(scale, scale);
            this.levelUp = levelUp;
        }

        public void OnClick()
        {
            if (rb.bodyType != RigidbodyType2D.Dynamic)
                rb.bodyType = RigidbodyType2D.Dynamic;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PropertiesFruits prop = collision.gameObject.GetComponent<PropertiesFruits>();
            if (prop != null)
            {
                if (this.levelFruit == prop.levelFruit)
                {
                    levelUp?.Invoke(this, prop, (int)this.levelFruit);
                    IsColide = true;
                    prop.IsColide = true;
                }
            }
            if(collision.gameObject.name.Contains("Line"))
                isEnd = false;
        }
    }
}
