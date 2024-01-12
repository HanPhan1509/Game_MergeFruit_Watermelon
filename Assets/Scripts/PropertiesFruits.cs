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
        private float oriSize = 2.04f;

        public bool IsColide { get => isColide; set => isColide = value; }
        public bool IsEnd { get => isEnd; }

        public void Initialized(Sprite sprite, int sizeScale, Action<PropertiesFruits, PropertiesFruits, int> levelUp, Action endGame, bool isFall = false)
        {
            this.levelUp = levelUp;
            IsColide = false;
            levelFruit = (LevelFruit)sizeScale;
            spriteRendererFruit.sprite = sprite;
            rb.bodyType = isFall ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            ResizeFruit(sprite, sizeScale);
        }

        private void ResizeFruit(Sprite sprite, int sizeScale)
        {
            // Get size sprite
            float originalSize = sprite.bounds.size.x;
            float x = 1;
            Debug.Log("before : " + x);
            if (originalSize > oriSize)
            {
                x /= 2;
                Debug.Log("1 : " + x);
            }
            else if (originalSize < oriSize)
            {
                x *= 2;
                Debug.Log("2 : " + x);
            }
            Debug.Log("after : " + x);
            float scale = (scaleFruit + sizeScale * 0.2f) * x;
            collide.radius = 1.04f * (originalSize > oriSize? 2 : 1);
            Debug.Log($"check size {originalSize} > {oriSize} => scale = {scale} , radius collide = {collide.radius}");
            transformFruit.localScale = new Vector2(scale, scale);
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
            if (collision.gameObject.name.Contains("Line"))
                isEnd = false;
        }
    }
}
