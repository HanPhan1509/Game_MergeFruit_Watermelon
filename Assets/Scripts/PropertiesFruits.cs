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
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private CircleCollider2D collide;
        private float scaleFruit = 0.3f;

        public void Initialized(Sprite sprite, int sizeScale)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            spriteRendererFruit.sprite = sprite;
            scaleFruit += sizeScale * 0.2f;
            transformFruit.localScale = new Vector2(scaleFruit, scaleFruit);
            Debug.Log($"level fruit = {sizeScale} - {(LevelFruit)sizeScale} - scale = {scaleFruit} - radius = {collide.radius}");
        }

        public void OnClick()
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
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
